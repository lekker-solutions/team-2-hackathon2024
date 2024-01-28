using LS.CarbonAccountingModule.AM.DAC.Extension;
using LS.CarbonAccountingModule.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LS.CarbonAccountingModule.Helper;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.AM;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.AM.Graph.Extension
{

    public class LSCAProdMaintExt : PXGraphExtension<ProdMaint>
    {
        public static bool IsActive() => true;

        public SelectFrom<AMProdMatl>.
            Where<AMProdMatl.orderType.IsEqual<AMProdItem.orderType.FromCurrent>
                .And<AMProdMatl.prodOrdID.IsEqual<AMProdItem.prodOrdID.FromCurrent>>>.View AllProdMatlRecords;

        public SelectFrom<AMProdOper>.
            Where<AMProdOper.orderType.IsEqual<AMProdItem.orderType.FromCurrent>
                .And<AMProdOper.prodOrdID.IsEqual<AMProdItem.prodOrdID.FromCurrent>>>.View AllAMProdOperRecords;

        public PXAction<AMProdItem> release;

        [PXUIField(DisplayName = "Release Order", MapEnableRights = PXCacheRights.Update,
            MapViewRights = PXCacheRights.Update)]
        [PXProcessButton]
        public virtual IEnumerable Release(PXAdapter adapter)
        {
            var items = Base.release.Press(adapter);

            List<LSCATransactionDetail> details = CreateMaterialDetailLines();
            List<LSCATransactionDetail> opsDetails = CreateOperationDetailLines();
            details.AddRange(opsDetails);
            LSCATransactionEntry.CreateCarbonTransaction(Base.ProdItemSelected.Current.NoteID,
                Base.ProdItemSelected.Current.ProdDate, details);

            return adapter.Get();
        }

        private List<LSCATransactionDetail> CreateMaterialDetailLines()
        {
            List<LSCATransactionDetail> details = new List<LSCATransactionDetail>();
            foreach (var materialRecord in AllProdMatlRecords.View.SelectMulti().RowCast<AMProdMatl>())
            {
                var detailRecord = new LSCATransactionDetail
                {
                    InventoryID       = materialRecord.InventoryID,
                    Qty               = materialRecord.Qty,
                    BaseQty           = materialRecord.BaseQty,
                    Rate              = materialRecord.GetExtension<LSAMProdMatlExt>().CarbonEmission,
                    ExtCarbonEquivQty = materialRecord.GetExtension<LSAMProdMatlExt>().TotalCarbonEmission,
                    TranDescr =
                        $"Production Order {materialRecord.ProdOrdID} - Consumption of {materialRecord.Qty} Item {InventoryItem.PK.Find(Base, materialRecord.InventoryID)?.InventoryCD}",
                    ReasonCode = "PRODUCTION"
                };

                details.Add(detailRecord);
            }

            return details;
        }

        private List<LSCATransactionDetail> CreateOperationDetailLines()
        {
            List<LSCATransactionDetail> details = new List<LSCATransactionDetail>();
            foreach (var operationalRecord in AllAMProdOperRecords.View.SelectMulti().RowCast<AMProdOper>())
            {
                var detailRecord = new LSCATransactionDetail
                {
                    Qty               = operationalRecord.RunUnits,
                    BaseQty           = operationalRecord.BaseTotalQty,
                    Rate              = operationalRecord.GetExtension<LSAMProdOperExt>().CarbonEmission,
                    ExtCarbonEquivQty = operationalRecord.GetExtension<LSAMProdOperExt>().TotalCarbonEmission,
                    TranDescr =
                        $"Production Order {operationalRecord.ProdOrdID}: Operation {operationalRecord.OperationCD} - Work Center {operationalRecord.WcID}",
                    ReasonCode = "PRODUCTION"
                };

                details.Add(detailRecord);
            }

            return details;
        }
    }
}
