using System;
using LS.CarbonAccountingModule.DAC.Extension;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.DAC
{
    [Serializable]
    [PXCacheName("LSCATransactionDetail")]
    public class LSCATransactionDetail : IBqlTable
    {
        public class PK : PrimaryKeyOf<LSCATransactionDetail>.By<transactionType, referenceNumber, lineNbr>
        {
            public static LSCATransactionDetail Find(
                PXGraph graph, string transactionType, string referenceNbr, int? lineNbr)
                => FindBy(graph, transactionType, referenceNbr, lineNbr);
        }

        public class FK
        {
            public class LSCATransactionFK : LSCATransaction.PK.ForeignKeyOf<LSCATransactionDetail>.By<transactionType,
                referenceNumber>
            {
            }
        }

        #region TransactionType

        [PXDBString(1, IsKey = true, IsFixed = true, InputMask = "")]
        [PXDefault(typeof(LSCATransaction.transactionType))]
        [PXUIField(DisplayName = "Transaction Type", Visible = false, Visibility = PXUIVisibility.Invisible)]
        public virtual string TransactionType { get; set; }

        public abstract class transactionType : BqlString.Field<transactionType>
        {
        }

        #endregion

        #region ReferenceNumber

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXDBDefault(typeof(LSCATransaction.referenceNumber))]
        [PXParent(typeof(FK.LSCATransactionFK))]
        [PXUIField(DisplayName = "Reference Number", Visible = false, Visibility = PXUIVisibility.Invisible)]
        public virtual string ReferenceNumber { get; set; }

        public abstract class referenceNumber : BqlString.Field<referenceNumber>
        {
        }

        #endregion

        #region LineNbr

        [PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(LSCATransaction.lastLineNbr))]
        [PXUIField(DisplayName = "Line Nbr", Visible = false, Visibility = PXUIVisibility.Invisible)]
        public virtual int? LineNbr { get; set; }

        public abstract class lineNbr : BqlInt.Field<lineNbr>
        {
        }

        #endregion

        #region InventoryID

        [PXSelector(typeof(InventoryItem.inventoryID),
            typeof(InventoryItem.descr),
            typeof(InventoryItem.stkItem),
            SubstituteKey = typeof(InventoryItem.inventoryCD))]
        [PXUIField(DisplayName = "Inventory ID")]
        public virtual int? InventoryID { get; set; }

        public abstract class inventoryID : BqlInt.Field<inventoryID>
        {
        }

        #endregion

        #region UOM

        [PXDefault(typeof(Selector<inventoryID, InventoryItem.baseUnit>), PersistingCheck = PXPersistingCheck.Nothing)]
        [INUnit(typeof(inventoryID))]
        [PXUIField(DisplayName = "UOM")]
        public virtual string UOM { get; set; }

        public abstract class uOM : BqlString.Field<uOM>
        {
        }

        #endregion

        #region Qty

        [PXDBQuantity(typeof(uOM), typeof(baseQty), HandleEmptyKey = true)]
        [PXUIField(DisplayName = "Qty")]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? Qty { get; set; }

        public abstract class qty : BqlDecimal.Field<qty>
        {
        }

        #endregion

        #region BaseQty

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Base Qty")]
        public virtual decimal? BaseQty { get; set; }

        public abstract class baseQty : BqlDecimal.Field<baseQty>
        {
        }

        #endregion

        #region Rate

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Rate")]
        public virtual decimal? Rate { get; set; }

        public abstract class rate : BqlDecimal.Field<rate>
        {
        }

        #endregion

        #region ExtCarbonEquivQty

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Total t C02-eq")]
        public virtual decimal? ExtCarbonEquivQty { get; set; }

        public abstract class extCarbonEquivQty : BqlDecimal.Field<extCarbonEquivQty>
        {
        }

        #endregion


        #region ReasonCode

        [PXDBString(20, IsUnicode = true)]
        [PXSelector(
            typeof(Search<ReasonCode.reasonCodeID, Where<LSCAReasonCodeExt.usrLSCAForCarbonAccounting, Equal<True>>>),
            typeof(ReasonCode.reasonCodeID),
            typeof(ReasonCode.usage))]
        [PXUIField(DisplayName = "Reason Code")]
        public virtual string ReasonCode { get; set; }

        public abstract class reasonCode : BqlString.Field<reasonCode>
        {
        }

        #endregion

        #region TranDescr

        [PXDBString(125, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Description")]
        public virtual string TranDescr { get; set; }

        public abstract class tranDescr : BqlString.Field<tranDescr>
        {
        }

        #endregion
    }
}