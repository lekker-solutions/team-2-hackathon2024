using PX.Data;
using PX.Data.BQL;
using PX.Objects.FA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.CarbonAccountingModule
{
    public class SNZCTransferShippingDetail : IBqlTable
    {
        
        [PXDBString(15, IsKey = true)]
        [PXUIField(DisplayName = "Transfer Nbr")]
        public virtual string RefNbr { get; set; }
        public abstract class refNbr : BqlString.Field<refNbr> { }

        [PXDBString(1, IsKey = true)]
        [PXUIField(DisplayName = "Doc Type")]
        public virtual string DocType { get; set; }
        public abstract class docType : BqlString.Field<docType> { }

        [PXDBString()]
        [PXStringList(
            new string[] { "F", "S", "T", "A", "B" },
            new string[] { "Fleet Truck", "Fleet Semi", "Train", "Air Freight", "Boat Freight" })]
        [PXUIField(DisplayName = "Transportation")]
        public virtual string TransportType { get; set; }
        public abstract class transportType : BqlString.Field<transportType> { }

        [PXDecimal(2)]
        [PXUIField(DisplayName = "Distance (In Miles)" )]
        public virtual decimal? Distance { get; set; }
        public abstract class distance : BqlDecimal.Field<distance> { }

        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Carbon Weight", Enabled = false)]
        public virtual decimal? CarbonWeight { get; set; }
        public abstract class carbonWeight : BqlDecimal.Field<carbonWeight> { }

        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Total Carbon Cost")]
        public virtual decimal? TotalCarbonCost { get; set; }
        public abstract class totalCarbonCost : BqlDecimal.Field<totalCarbonCost> { }
        
        
    }
}
