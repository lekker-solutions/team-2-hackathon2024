#region #Copyright

// ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2024 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2024/1/27
// ----------------------------------------------------------------------------------

#endregion

using PX.Data;
using PX.Objects.AM;
using System;
using PX.Objects.IN;

namespace LS.CarbonAccountingModule.AM.DAC.Extension
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class LSAMProdOperExt : PXCacheExtension<AMProdOper>
    {
        #region CarbonEmission
        public abstract class carbonEmission : PX.Data.BQL.BqlDecimal.Field<carbonEmission> { }

        [PXDecimal(6)]
        [PXDefault(typeof(Search<LSAMWCExt.carbonEmission, Where<AMWC.wcID,
            Equal<Current<AMProdOper.wcID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUnboundDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Carbon Emission", Enabled = false)]
        public decimal? CarbonEmission { get; set; }

        #endregion

        #region TotalCarbonEmission
        public abstract class totalCarbonEmission : PX.Data.BQL.BqlDecimal.Field<totalCarbonEmission> { }

        [PXDecimal(6)]
        [PXFormula(typeof(Mult<carbonEmission, AMProdOper.runUnits>))]
        [PXUnboundDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Total Carbon Emission in kg", Enabled = false)]
        public decimal? TotalCarbonEmission { get; set; }

        #endregion
    }
}