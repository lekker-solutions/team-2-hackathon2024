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

namespace LS.CarbonAccountingModule.AM.DAC.Extension
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public sealed class LSAMProdItemExt : PXCacheExtension<AMProdItem>
    {
        #region TotalCarbonEmission
        public abstract class totalCarbonEmission : PX.Data.BQL.BqlDecimal.Field<totalCarbonEmission> { }

        //[PXDBScalar(typeof(Search4<
        //		AMProdMatl.totalCarbonEmission,
        //	Where<
        //		AMProdMatl.orderType, Equal<Current<AMProdOper.orderType>>
        //		, And<AMProdMatl.prodOrdID, Equal<Current<AMProdOper.prodOrdID>>,
        //	Aggregate<
        //		Sum<AMProdMatl.totalCarbonEmission>>>>>))]
        //[PXDBScalar(typeof<Search4<AMProdMatl.totalCarbonEmission, Where<AMProdMatl.prodOrdID, Equal<Current<AMProdItem.prodOrdID>>>>>)]

        //[PXDBScalar(typeof(Search4<AMProdMatl.totalCarbonEmission,
        //	Where<AMProdMatl.orderType, Equal<Current<AMProdItem.orderType>>,
        //	And<AMProdMatl.prodOrdID, Less<Current<AMProdItem.prodOrdID>>>>,
        //	Aggregate<Sum<AMProdMatl.totalCarbonEmission>>>))]


        [PXDefault(typeof(Search5<totalCarbonEmission,
            InnerJoin<AMProdMatl, On<AMProdMatl.orderType, Equal<Current<AMProdItem.orderType>>,
                And<AMProdMatl.prodOrdID, Equal<Current<AMProdItem.prodOrdID>>>>>,
            Aggregate<
                Sum<LSAMProdMatlExt.totalCarbonEmission>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDecimal(6)]
        [PXUIField(DisplayName = "Total Carbon Emission in kg", Enabled = false)]
        public decimal? TotalCarbonEmission { get; set; }

        #endregion
    }
}