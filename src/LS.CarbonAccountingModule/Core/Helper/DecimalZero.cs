#region #Copyright

// ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2024 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2024/1/28
// ----------------------------------------------------------------------------------

#endregion

using PX.Data.BQL;

namespace LS.CarbonAccountingModule.Helper
{
    public class DecimalZero : BqlDecimal.Constant<DecimalZero>
    {
        public DecimalZero() : base(0M)
        {
        }
    }
}