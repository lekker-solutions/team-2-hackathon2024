using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LS.CarbonAccountingModule.DAC;
using LS.CarbonAccountingModule.Descriptor;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.IN;
using PX.Objects.PO;

namespace LS.CarbonAccountingModule
{
    public class LSCATransactionEntry : PXGraph<LSCATransactionEntry, LSCATransaction>
    {
        public SelectFrom<LSCATransaction>.View Document;

        public SelectFrom<LSCATransactionDetail>
            .Where<LSCATransactionDetail.transactionType
                .IsEqual<LSCATransaction.transactionType.FromCurrent>
                .And<LSCATransactionDetail.referenceNumber.IsEqual<LSCATransaction.referenceNumber.FromCurrent>>>.View
            Transactions;

        public PXSetup<LSCASetup> Setup;

        public LSCATransactionEntry()
        {
            _issueEntry   = new Lazy<INIssueEntry>(PXGraph.CreateInstance<INIssueEntry>);
            _receiptEntry = new Lazy<INReceiptEntry>(PXGraph.CreateInstance<INReceiptEntry>);
        }

        protected void _(Events.RowSelected<LSCATransaction> e)
        {
            if (e.Row is null) return;
            switch (e.Row.Status)
            {
                case CarbonTranStatus.Hold:
                    Document.AllowUpdate     = true;
                    Transactions.AllowUpdate = true;
                    ActionPutOnHold.SetVisible(false);
                    ActionPutOnHold.SetEnabled(false);
                    ActionRelease.SetVisible(false);
                    ActionRelease.SetEnabled(false);
                    ActionReleaseFromHold.SetVisible(true);
                    ActionReleaseFromHold.SetEnabled(true);
                    break;
                case CarbonTranStatus.Open:
                    Document.AllowUpdate     = true;
                    Transactions.AllowUpdate = true;
                    ActionPutOnHold.SetVisible(true);
                    ActionPutOnHold.SetEnabled(true);
                    ActionRelease.SetVisible(true);
                    ActionRelease.SetEnabled(true);
                    ActionReleaseFromHold.SetVisible(false);
                    ActionReleaseFromHold.SetEnabled(false);
                    break;
                case CarbonTranStatus.Released:
                    Document.AllowUpdate     = false;
                    Transactions.AllowUpdate = false;
                    ActionPutOnHold.SetVisible(false);
                    ActionPutOnHold.SetEnabled(false);
                    ActionRelease.SetVisible(false);
                    ActionRelease.SetEnabled(false);
                    ActionReleaseFromHold.SetVisible(false);
                    ActionReleaseFromHold.SetEnabled(false);
                    break;
            }
        }

        public PXAction<LSCATransaction> ActionRelease;

        [PXUIField(DisplayName = "Release")]
        [PXButton(CommitChanges = true)]
        public IEnumerable actionRelease(PXAdapter adapter)
        {
            var list = Save.Press(adapter).Cast<LSCATransaction>().ToList();
            PXLongOperation.StartOperation(this, () =>
            {
                var graph = PXGraph.CreateInstance<LSCATransactionEntry>();
                foreach (LSCATransaction transaction in list)
                {
                    graph.Document.Current =
                        graph.Document.Search<LSCATransaction.transactionType, LSCATransaction.referenceNumber>(
                            transaction.TransactionType, transaction.ReferenceNumber);
                    switch (transaction.TransactionType)
                    {
                        case CarbonTranType.Emission:
                            graph.PostEmissionTransaction();
                            break;
                        case CarbonTranType.Capture:
                            graph.PostCaptureTransaction();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(transaction.TransactionType),
                                transaction.TransactionType, string.Empty);
                    }
                }
            });

            return adapter.Get();
        }

        public PXAction<LSCATransaction> ActionPutOnHold;

        [PXUIField(DisplayName = "Hold")]
        [PXButton(CommitChanges = true)]
        public void actionPutOnHold()
        {
            if (Document.Current is null) return;
            Document.Current.Status = CarbonTranStatus.Hold;
            Document.UpdateCurrent();
            Save.Press();
        }

        public PXAction<LSCATransaction> ActionReleaseFromHold;

        [PXUIField(DisplayName = "Open")]
        [PXButton(CommitChanges = true)]
        public void actionReleaseFromHold()
        {
            if (Document.Current is null) return;
            Document.Current.Status = CarbonTranStatus.Open;
            Document.UpdateCurrent();
            Save.Press();
        }

        public static void CreateCarbonTransaction<TSource>(Guid?                              noteId,
                                                            DateTime?                          transactionDate,
                                                            IEnumerable<LSCATransactionDetail> transactions)
        {
            var graph = PXGraph.CreateInstance<LSCATransactionEntry>();
            var document = graph.Document.Insert(new LSCATransaction()
            {
                TransactionType =
                    transactions.Sum(t =>
                        t.ExtCarbonEquivQty.GetValueOrDefault()) > 0
                        ? CarbonTranType.Emission
                        : CarbonTranType.Capture
            });
            document.TranDate  = transactionDate;
            document.RefNoteID = noteId;
            foreach (LSCATransactionDetail detail in transactions)
            {
                detail.ReferenceNumber = null;
                detail.TransactionType = null;
                detail.LineNbr         = null;
                graph.Transactions.Insert(detail);
            }

            graph.ActionRelease.Press();
        }


        public void PostEmissionTransaction()
        {
            _issueEntry.Value.Clear(PXClearOption.ClearAll);
            var issue = _issueEntry.Value.issue.Insert();
            issue.TranDate = this.Document.Current.TranDate;
            issue.TranDesc = this.Document.Current.Descr;

            foreach (LSCATransactionDetail detail in Transactions.SelectMain())
            {
                var tran = _issueEntry.Value.transactions.Insert();
                _issueEntry.Value.transactions.Cache.SetValueExt<INTran.inventoryID>(tran,
                    Setup.Current.CarbonInventoryID);
                _issueEntry.Value.transactions.Cache.SetValueExt<INTran.qty>(tran, detail.ExtCarbonEquivQty);
                tran.TranDesc = detail.TranDescr;
                _issueEntry.Value.transactions.Cache.SetValueExt<INTran.reasonCode>(tran, detail.ReasonCode);
                _issueEntry.Value.transactions.Update(tran);
            }

            _issueEntry.Value.releaseFromHold.Press();
            _issueEntry.Value.release.Press();

            Document.Current.InventoryTransactionType = _issueEntry.Value.issue.Current.DocType;
            Document.Current.InventoryTranRefNbr      = _issueEntry.Value.issue.Current.RefNbr;
            Document.Current.Status                   = CarbonTranStatus.Released;
            Document.UpdateCurrent();
            Save.Press();
        }

        public void PostCaptureTransaction()
        {
            _receiptEntry.Value.Clear(PXClearOption.ClearAll);
            var receipt = _receiptEntry.Value.receipt.Insert();
            receipt.TranDate = this.Document.Current.TranDate;
            receipt.TranDesc = this.Document.Current.Descr;
            foreach (LSCATransactionDetail detail in Transactions.SelectMain())
            {
                var tran = _receiptEntry.Value.transactions.Insert();
                _receiptEntry.Value.transactions.Cache.SetValueExt<INTran.inventoryID>(tran,
                    Setup.Current.CarbonInventoryID);
                _receiptEntry.Value.transactions.Cache.SetValueExt<INTran.qty>(tran, detail.ExtCarbonEquivQty);
                tran.TranDesc = detail.TranDescr;
                _receiptEntry.Value.transactions.Cache.SetValueExt<INTran.reasonCode>(tran, detail.ReasonCode);
                _receiptEntry.Value.transactions.Update(tran);
            }

            _receiptEntry.Value.releaseFromHold.Press();
            _receiptEntry.Value.release.Press();
            Document.Current.InventoryTransactionType = _receiptEntry.Value.receipt.Current.DocType;
            Document.Current.InventoryTranRefNbr      = _receiptEntry.Value.receipt.Current.RefNbr;
            Document.Current.Status                   = CarbonTranStatus.Released;
            Document.UpdateCurrent();
            Save.Press();
        }

        private readonly Lazy<INReceiptEntry> _receiptEntry;
        private readonly Lazy<INIssueEntry>   _issueEntry;
    }
}