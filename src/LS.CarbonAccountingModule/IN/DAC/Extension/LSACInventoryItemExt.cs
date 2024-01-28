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
using System.Data.SqlTypes;
using System;

namespace LS.CarbonAccountingModule.IN.DAC.Extension
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class LSACInventoryItemExt : PXCacheExtension<InventoryItem>
    {

        #region CarbonEmission
        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Carbon Emission (kg)")]
        public virtual Decimal? CarbonEmission { get; set; }
        public abstract class carbonEmission : BqlDecimal.Field<carbonEmission> { }
        #endregion
    }
}