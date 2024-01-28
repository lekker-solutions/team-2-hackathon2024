
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

namespace LS.CarbonAccountingModule.DAC.Extension
{
    public sealed class LSCAReasonCodeExt : PXCacheExtension<ReasonCode>
    {
        #region UsrLSCAForCarbonAccounting

        [PXDBBool]
        [PXUIField(DisplayName = "For Carbon Accounting")]
        public bool? UsrLSCAForCarbonAccounting { get; set; }

        public abstract class usrLSCAForCarbonAccounting : BqlBool.Field<usrLSCAForCarbonAccounting>
        {
        }

        #endregion
    }
}