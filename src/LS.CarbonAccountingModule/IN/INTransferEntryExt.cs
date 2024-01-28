using LS.CarbonAccountingModule.DAC;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PX.Objects.SO.SOPickingWorksheet.worksheetType;
using PX.Data.BQL.Fluent;
using PX.Common;

namespace LS.CarbonAccountingModule
{
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

            LSCATransactionDetail carbonDetail = new LSCATransactionDetail();
            carbonDetail.TransactionType = targetRecord.DocType;
            carbonDetail.ReferenceNumber = targetRecord.RefNbr;
            carbonDetail.ExtCarbonEquivQty = targetDetail.TotalCarbonCost;
            carbonDetail.TranDescr = targetDetail.Distance + " miles using " +
                (targetDetail.TransportType == "F" ? "Fleet Truck" :
                targetDetail.TransportType == "S" ? "Fleet Semi" :
                targetDetail.TransportType == "T" ? "Train" :
                targetDetail.TransportType == "A" ? "Air Freight" :
                targetDetail.TransportType == "B" ? "Boat Freight" : string.Empty) +
                " transportation.";

            LSCATransactionEntry.CreateCarbonTransaction<INRegister>(targetRecord, targetRecord.TranDate, carbonDetail.AsSingleEnumerable());

            return list;
        }
        #endregion
    }
}
