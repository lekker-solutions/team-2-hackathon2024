using LS.CarbonAccountingModule.AM.DAC.Extension;
using LS.CarbonAccountingModule.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.CarbonAccountingModule.AM.Graph.Extension
{

    public class LSCAProdMaintExt : PXGraphExtension<ProdMaint>
    {
        public static bool IsActive() => true;

        public SelectFrom<LSAMProdMatlExt>.
            Where<LSAMProdMatlExt.orderType.IsEqual<AMProdItem.orderType.FromCurrent>
                .And<LSAMProdMatlExt.prodOrdID.IsEqual<AMProdItem.prodOrdID.FromCurrent>>>.View AllProdMatlRecords;

        public SelectFrom<LSAMProdOperExt>.
            Where<LSAMProdOperExt.orderType.IsEqual<AMProdItem.orderType.FromCurrent>
                .And<LSAMProdOperExt.prodOrdID.IsEqual<AMProdItem.prodOrdID.FromCurrent>>>.View AllAMProdOperRecords;

        [PXOverride]
        public IEnumerable Release(PXAdapter adapter, Action<PXAdapter> baseMethod)
        {
            baseMethod(adapter);

            List<LSCATransactionDetail> details = CreateMaterialDetailLines();
            List<LSCATransactionDetail> opsDetails = CreateOperationDetailLines();

            LSCATransactionEntry.CreateCarbonTransaction<AMProdItem>(Base.ProdItemSelected.Current.NoteID, Base.ProdItemSelected.Current.ProdDate, details.AddRange(opsDetails));

            return adapter.Get();
        }

        private List<LSCATransactionDetail> CreateMaterialDetailLines()
        {
            List<LSCATransactionDetail> details = new List<LSCATransactionDetail>();
            foreach (var materialRecord in AllProdMatlRecords.View.SelectMulti().RowCast<LSAMProdMatlExt>())
            {
                var detailRecord = new LSCATransactionDetail
                {
                    InventoryID = materialRecord.InventoryID,
                    Qty = materialRecord.Qty,
                    BaseQty = materialRecord.BaseQty,
                    Rate = materialRecord.CarbonEmission
                };

                details.Add(detailRecord);
            }

            return details;
        }

        private List<LSCATransactionDetail> CreateOperationDetailLines()
        {
            List<LSCATransactionDetail> details = new List<LSCATransactionDetail>();
            foreach (var operationalRecord in AllProdMatlRecords.View.SelectMulti().RowCast<LSAMProdOperExt>())
            {
                var detailRecord = new LSCATransactionDetail
                {
                    InventoryID = operationalRecord.WcID,
                    Qty = operationalRecord.RunUnits,
                    BaseQty = operationalRecord.BaseTotalQty,
                    Rate = operationalRecord.CarbonEmission
                };

                details.Add(detailRecord);
            }

            return details;
        }
    }
}
