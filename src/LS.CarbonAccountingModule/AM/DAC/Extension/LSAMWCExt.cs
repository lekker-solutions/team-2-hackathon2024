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
using System;
using PX.Objects.AM;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.AM.DAC.Extension
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class LSAMWCExt : PXCacheExtension<AMWC>
    {
        #region CarbonEmission

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Carbon Emission (kg)")]
        public decimal? CarbonEmission { get; set; }

        public abstract class carbonEmission : BqlDecimal.Field<carbonEmission> { }
        #endregion
    }
}