#region #Copyright

// ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2024 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2024/1/27
// ----------------------------------------------------------------------------------

#endregion

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.DAC.Extension
{
    public class LSACInventoryItemExt : PXCacheExtension<InventoryItem>
    {
        #region UsrLSACCarbonTonEquivalent

        [PXDBDecimal(6)]
        [PXUIField(DisplayName = "t C02-eq Per Base Unit")]
        public virtual decimal? UsrLSACCarbonTonEquivalent { get; set; }

        public abstract class usrLSACCarbonTonEquivalent : BqlDecimal.Field<usrLSACCarbonTonEquivalent>
        {
        }

        #endregion
    }
}