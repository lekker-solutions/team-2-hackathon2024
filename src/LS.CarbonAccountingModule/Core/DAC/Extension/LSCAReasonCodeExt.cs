
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

namespace LS.CarbonAccountingModule.DAC.Extension
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
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