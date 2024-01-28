using LS.CarbonAccountingModule.DAC;
using PX.Data;
using PX.Objects.IN;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Data.BQL.Fluent;
using PX.Common;

namespace LS.CarbonAccountingModule
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class INTransferEntryExt : PXGraphExtension<INTransferEntry>
    {
        #region Views
        public SelectFrom<SNZCTransferShippingDetail>
            .Where<SNZCTransferShippingDetail.refNbr.IsEqual<INRegister.refNbr.FromCurrent>
                .And<SNZCTransferShippingDetail.docType.IsEqual<INRegister.docType.FromCurrent>>>.View CarbonDetail;
        #endregion

        #region Event Handlers
        protected void _(Events.FieldUpdated<INRegister, INRegister.transferType> e)
        {
            INRegister row = e.Row;
            if (row == null)
                return;

            if (row.TransferType == "2")
            {
                //show tab
            }
            else
            {
                //hide tab  
            }
        }

        protected void _(Events.FieldUpdated<SNZCTransferShippingDetail, SNZCTransferShippingDetail.transportType> e)
        {
            SNZCTransferShippingDetail row = e.Row;
            if (row == null || string.IsNullOrWhiteSpace(row.TransportType))
                return;

            RecalculateTotalCarbonCost(row);

            //new string[] { "F", "S", "T", "A", "B" },
            //new string[] { "Fleet Truck", "Fleet Semi", "Train", "Air Freight", "Boat Freight" })]
            row.CarbonWeight = row.TransportType == "F" ? 3.5m :
                row.TransportType == "S" ? 1 :
                row.TransportType == "T" ? 3 :
                row.TransportType == "A" ? 5 :
                row.TransportType == "B" ? 2 :
                0;
        }

        protected void _(Events.FieldUpdated<SNZCTransferShippingDetail, SNZCTransferShippingDetail.distance> e)
        {
            SNZCTransferShippingDetail row = e.Row;
            if (row == null || string.IsNullOrWhiteSpace(row.TransportType))
                return;

            RecalculateTotalCarbonCost(row);
        }

        protected void RecalculateTotalCarbonCost(SNZCTransferShippingDetail row)
        {
            switch (row.TransportType)
            {
                case "F":
                    row.TotalCarbonCost = row.Distance * 3.5m;
                    break;
                case "S":
                    row.TotalCarbonCost = row.Distance * 1;
                    break;
                case "T":
                    row.TotalCarbonCost = row.Distance * 3;
                    break;
                case "A":
                    row.TotalCarbonCost = row.Distance * 5;
                    break;
                case "B":
                    row.TotalCarbonCost = row.Distance * 2;
                    break;
                default:
                    row.TotalCarbonCost = 0;
                    break;
            }
        }
        #endregion

        #region Actions
        [PXProcessButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
        protected virtual IEnumerable Release(PXAdapter adapter)
        {
            List<INRegister> list = new List<INRegister>();
            foreach (INRegister item in adapter.Get<INRegister>())
            {
                if (item.Hold == false && item.Released == false)
                {
                    list.Add(Base.INRegisterDataMember.Update(item));
                }
            }

            if (list.Count == 0)
            {
                throw new PXException("Document Status is invalid for processing.");
            }

            Base.Save.Press();
            PXQuickProcess.ActionFlow quickProcessFlow = adapter.QuickProcessFlow;
            PXLongOperation.StartOperation(this, delegate
            {
                INDocumentRelease.ReleaseDoc(list, isMassProcess: false, releaseFromHold: false, quickProcessFlow);
            });

            INRegister targetRecord = list.FirstOrDefault();
            SNZCTransferShippingDetail targetDetail = CarbonDetail.Current;

            LSCATransactionDetail carbonDetail = new()
            {
                TransactionType   = targetRecord.DocType,
                ReferenceNumber   = targetRecord.RefNbr,
                ExtCarbonEquivQty = targetDetail.TotalCarbonCost,
                TranDescr = targetDetail.Distance + " miles using " +
                            (targetDetail.TransportType    == "F" ? "Fleet Truck" :
                                targetDetail.TransportType == "S" ? "Fleet Semi" :
                                targetDetail.TransportType == "T" ? "Train" :
                                targetDetail.TransportType == "A" ? "Air Freight" :
                                targetDetail.TransportType == "B" ? "Boat Freight" : string.Empty) +
                            " transportation.",
                ReasonCode = "TRANSPORT"
            };

            LSCATransactionEntry.CreateCarbonTransaction(targetRecord.NoteID, targetRecord.TranDate,
                carbonDetail.AsSingleEnumerable());

            return list;
        }
        #endregion
    }
}
