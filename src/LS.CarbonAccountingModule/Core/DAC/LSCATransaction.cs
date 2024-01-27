using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;

namespace LS.CarbonAccounting
{
    [Serializable]
    [PXCacheName("LSCATransaction")]
    public class LSCATransaction : IBqlTable
    {
        public class PK : PrimaryKeyOf<LSCATransaction>.By<transactionType, referenceNumber>
        {
            public static LSCATransaction Find(
                PXGraph graph, string transactionType, string referenceNbr)
                => FindBy(graph, transactionType, referenceNbr);
        }

        #region TransactionType

        [PXDBString(1, IsKey = true, IsFixed = true, InputMask = "")]
        [PXUIField(DisplayName = "Transaction Type")]
        public virtual string TransactionType { get; set; }

        public abstract class transactionType : PX.Data.BQL.BqlString.Field<transactionType>
        {
        }

        #endregion

        #region ReferenceNumber

        [AutoNumber(typeof(LSCASetup), typeof(tranDate))]
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Reference Number")]
        public virtual string ReferenceNumber { get; set; }

        public abstract class referenceNumber : PX.Data.BQL.BqlString.Field<referenceNumber>
        {
        }

        #endregion

        #region Status

        [PXDBString(1)]
        [PXUIField(DisplayName = "Status")]
        public virtual string? Status { get; set; }

        public abstract class status : BqlString.Field<status>
        {
        }

        #endregion


        #region TranDate

        [PXDBDate()]
        [PXUIField(DisplayName = "Transaction Date")]
        public virtual DateTime? TranDate { get; set; }

        public abstract class tranDate : PX.Data.BQL.BqlDateTime.Field<tranDate>
        {
        }

        #endregion

        #region Descr

        [PXDBString(125, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Description")]
        public virtual string Descr { get; set; }

        public abstract class descr : PX.Data.BQL.BqlString.Field<descr>
        {
        }

        #endregion

        #region RefNoteID

        [PXDBGuid()]
        [PXUIField(DisplayName = "Ref Note ID")]
        public virtual Guid? RefNoteID { get; set; }

        public abstract class refNoteID : PX.Data.BQL.BqlGuid.Field<refNoteID>
        {
        }

        #endregion

        #region LastLineNbr

        [PXDBInt()]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Last Line Nbr")]
        public virtual int? LastLineNbr { get; set; }

        public abstract class lastLineNbr : PX.Data.BQL.BqlInt.Field<lastLineNbr>
        {
        }

        #endregion
    }
}