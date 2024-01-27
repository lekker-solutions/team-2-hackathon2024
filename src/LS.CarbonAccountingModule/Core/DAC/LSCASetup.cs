using System;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.DAC
{
    [Serializable]
    [PXCacheName("LSCASetup")]
    public class LSCASetup : IBqlTable
    {
        #region TransactionNumberingID

        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXSelector(typeof(Numbering.numberingID))]
        [PXUIField(DisplayName = "Transaction Numbering ID")]
        public virtual string TransactionNumberingID { get; set; }

        public abstract class transactionNumberingID : PX.Data.BQL.BqlString.Field<transactionNumberingID>
        {
        }

        #endregion

        #region CarbonInventoryID

        [StockItem]
        [PXUIField(DisplayName = "Carbon Inventory ID")]
        public virtual int? CarbonInventoryID { get; set; }

        public abstract class carbonInventoryID : PX.Data.BQL.BqlInt.Field<carbonInventoryID>
        {
        }

        #endregion

        #region CarbonSiteID

        [PXDBInt]
        [PXDimensionSelector(SiteAttribute.DimensionName, typeof(INSite.siteID), typeof(INSite.siteCD))]
        [PXUIField(DisplayName = "Carbon Warehouse")]
        public virtual int? CarbonSiteID { get; set; }

        public abstract class carbonSiteID : BqlInt.Field<carbonSiteID>
        {
        }

        #endregion
    }
}