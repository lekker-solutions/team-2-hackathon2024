using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;

namespace LS.CarbonAccounting
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
        [PXUIField(DisplayName = "Transaction Type")]
        public virtual string TransactionType { get; set; }

        public abstract class transactionType : PX.Data.BQL.BqlString.Field<transactionType>
        {
        }

        #endregion

        #region ReferenceNumber

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXDBDefault(typeof(LSCATransaction.referenceNumber))]
        [PXParent(typeof(FK.LSCATransactionFK))]
        [PXUIField(DisplayName = "Reference Number")]
        public virtual string ReferenceNumber { get; set; }

        public abstract class referenceNumber : PX.Data.BQL.BqlString.Field<referenceNumber>
        {
        }

        #endregion

        #region LineNbr

        [PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(LSCATransaction.lastLineNbr))]
        [PXUIField(DisplayName = "Line Nbr")]
        public virtual int? LineNbr { get; set; }

        public abstract class lineNbr : PX.Data.BQL.BqlInt.Field<lineNbr>
        {
        }

        #endregion

        #region Qty

        [PXDBDecimal(6)]
        [PXUIField(DisplayName = "Qty")]
        public virtual Decimal? Qty { get; set; }

        public abstract class qty : PX.Data.BQL.BqlDecimal.Field<qty>
        {
        }

        #endregion

        #region ReasonCode

        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Reason Code")]
        public virtual string ReasonCode { get; set; }

        public abstract class reasonCode : PX.Data.BQL.BqlString.Field<reasonCode>
        {
        }

        #endregion

        #region TranDescr

        [PXDBString(125, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Tran Descr")]
        public virtual string TranDescr { get; set; }

        public abstract class tranDescr : PX.Data.BQL.BqlString.Field<tranDescr>
        {
        }

        #endregion
    }
}