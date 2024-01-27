using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LS.CarbonAccountingModule.DAC;
using PX.Data;
using PX.Data.BQL.Fluent;


namespace LS.CarbonAccountingModule
{
    public class LSCASetupMaint : PXGraph<LSCASetupMaint>
    {
        public PXCancel<LSCASetup>        Cancel;
        public PXSave<LSCASetup>          Save;
        public SelectFrom<LSCASetup>.View Setup;
    }
}