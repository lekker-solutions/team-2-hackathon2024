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

namespace LS.CarbonAccountingModule.Descriptor
{
    public class CarbonTranStatus : PXStringListAttribute
    {
        public const string Open = "O";
        public const string Hold = "Hold";

        public const string Released = "R";

        public CarbonTranStatus() : base(Pair(CarbonTranStatus.Hold, nameof(CarbonTranStatus.Hold)),
            Pair(CarbonTranStatus.Open, nameof(CarbonTranStatus.Open)),
            Pair(CarbonTranStatus.Released, nameof(CarbonTranStatus.Released)))
        {
        }

        public class hold : BqlString.Constant<hold>
        {
            public hold() : base(Hold)
            {
            }
        }


        public class open : BqlString.Constant<open>
        {
            public open() : base(Open)
            {
            }
        }

        public class released : BqlString.Constant<released>
        {
            public released() : base(Released)
            {
            }
        }
    }
}