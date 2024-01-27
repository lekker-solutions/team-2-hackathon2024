using System;
using PX.Data;

namespace LS.CarbonAccountingModule.DAC
{
    [Serializable]
    [PXCacheName("LSCASetup")]
    public class LSCASetup : IBqlTable
    {
        #region TransactionNumberingID

        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Transaction Numbering ID")]
        public virtual string TransactionNumberingID { get; set; }

        public abstract class transactionNumberingID : PX.Data.BQL.BqlString.Field<transactionNumberingID>
        {
        }

        #endregion

        #region CarbonInventoryID

        [PXDBInt()]
        [PXUIField(DisplayName = "Carbon Inventory ID")]
        public virtual int? CarbonInventoryID { get; set; }

        public abstract class carbonInventoryID : PX.Data.BQL.BqlInt.Field<carbonInventoryID>
        {
        }

        #endregion
    }
}