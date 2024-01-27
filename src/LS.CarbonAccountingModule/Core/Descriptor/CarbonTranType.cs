#region #Copyright

// ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2024 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2024/1/27
// ----------------------------------------------------------------------------------

#endregion

using PX.Data;

namespace LS.CarbonAccountingModule.Descriptor
{
    public class CarbonTranType : PXStringListAttribute
    {
        public const string Emission = "E";
        public const string Capture  = "C";

        public CarbonTranType() : base(Pair(CarbonTranType.Emission, nameof(CarbonTranType.Emission)),
            Pair(CarbonTranType.Capture, nameof(CarbonTranType.Capture)))
        {
        }
    }
}