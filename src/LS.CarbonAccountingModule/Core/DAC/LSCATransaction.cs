using System;
using LS.CarbonAccountingModule.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.DAC
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

        [PXDefault(CarbonTranType.Emission)]
        [PXDBString(1, IsKey = true, IsFixed = true, IsUnicode = true, InputMask = "")]
        [CarbonTranType]
        [PXUIField(DisplayName = "Transaction Type")]
        public virtual string TransactionType { get; set; }

        public abstract class transactionType : PX.Data.BQL.BqlString.Field<transactionType>
        {
        }

        #endregion

        #region ReferenceNumber

        [AutoNumber(typeof(LSCASetup.transactionNumberingID), typeof(tranDate))]
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXSelector(typeof(LSCATransaction.referenceNumber))]
        [PXDefault]
        [PXUIField(DisplayName = "Reference Number")]
        public virtual string ReferenceNumber { get; set; }

        public abstract class referenceNumber : PX.Data.BQL.BqlString.Field<referenceNumber>
        {
        }

        #endregion

        #region Status

        [PXDBString(1)]
        [CarbonTranStatus]
        [PXDefault(CarbonTranStatus.Open)]
        [PXUIField(DisplayName = "Status", Enabled = false)]
        public virtual string Status { get; set; }

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
        [PXUIField(DisplayName = "Ref Note ID", Enabled = false)]
        public virtual Guid? RefNoteID { get; set; }

        public abstract class refNoteID : PX.Data.BQL.BqlGuid.Field<refNoteID>
        {
        }

        #endregion

        #region InventoryTransactionType

        [PXDBString(1, IsFixed = true)]
        [INDocType.List()]
        [PXUIField(DisplayName = "IN Tran Type", Enabled = false)]
        public virtual string InventoryTransactionType { get; set; }

        public abstract class inventoryTransactionType : BqlString.Field<inventoryTransactionType>
        {
        }

        #endregion

        #region InventoryTranRefNbr

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "IN Tran Ref Nbr", Enabled = false)]
        public virtual string InventoryTranRefNbr { get; set; }

        public abstract class inventoryTranRefNbr : BqlString.Field<inventoryTranRefNbr>
        {
        }

        #endregion


        #region LastLineNbr

        [PXDBInt()]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Last Line Nbr", Enabled = false)]
        public virtual int? LastLineNbr { get; set; }

        public abstract class lastLineNbr : PX.Data.BQL.BqlInt.Field<lastLineNbr>
        {
        }

        #endregion
    }
}