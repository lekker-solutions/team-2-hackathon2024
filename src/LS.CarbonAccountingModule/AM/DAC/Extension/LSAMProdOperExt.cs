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
using PX.Objects.AM;
using System.Data.SqlTypes;
using System;

namespace LS.CarbonAccountingModule.AM.DAC.Extension
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class LSAMProdOperExt : PXCacheExtension<AMProdOper>
    {
        #region CarbonEmission
        public abstract class carbonEmission : PX.Data.BQL.BqlDecimal.Field<carbonEmission> { }

        [PXDefault(typeof(Search<LSAMWCExt.carbonEmission, Where<LSAMWCExt.wcID,
            Equal<Current<LSAMProdOperExt.wcID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUnboundDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Carbon Emission", Enabled = false)]
        public virtual Decimal? CarbonEmission
        {
            get;
            set;
        }
        #endregion

        #region TotalCarbonEmission
        public abstract class totalCarbonEmission : PX.Data.BQL.BqlDecimal.Field<totalCarbonEmission> { }

        [PXFormula(typeof(Mult<LSAMProdOperExt.carbonEmission, LSAMProdOperExt.runUnits>))]
        [PXUnboundDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Total Carbon Emission in kg", Enabled = false)]
        public virtual Decimal? TotalCarbonEmission
        {
            get;
            set;
        }
        #endregion
    }
}