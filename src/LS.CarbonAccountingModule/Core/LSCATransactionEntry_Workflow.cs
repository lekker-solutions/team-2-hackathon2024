
//using LS.CarbonAccountingModule.DAC;
//using LS.CarbonAccountingModule.Descriptor;
//using PX.Data;
//using PX.Data.WorkflowAPI;

//namespace LS.CarbonAccountingModule
//{
//    public class LSCATransactionEntry_Workflow : PXGraphExtension<LSCATransactionEntry>
//    {
//        public override void Configure(PXScreenConfiguration configuration)
//        {
//            var context = configuration.GetScreenConfigurationContext<LSCATransactionEntry, LSCATransaction>();
//            Configure(context);
//        }

//        public void Configure(WorkflowContext<LSCATransactionEntry, LSCATransaction> configuration)
//        {
//            const string Initial = "_";
//            configuration.AddScreenConfigurationFor(c =>
//                c.StateIdentifierIs<LSCATransaction.status>().AddDefaultFlow(fc =>
//                    fc.WithFlowStates(s =>
//                    {
//                        s.Add(Initial, o => o.IsInitial().WithActions(a => a.Add(a => a.InitializeState)));
//                        s.Add<CarbonTranStatus.open>(o =>
//                            o.WithActions(a => a.Add(a => a.ActionPutOnHold)));
//                        s.Add<CarbonTranStatus.hold>(o => o.IsInitial().WithActions(a =>
//                        {
//                            a.Add(a => a.ActionPutOnHold);
//                            a.Add(a => a.ActionRelease);
//                        }));
//                        s.Add<CarbonTranStatus.released>(r => r.WithFieldStates(fs =>
//                        {
//                            fs.AddAllFields<LSCATransaction>(fs => fs.IsDisabled());
//                            fs.AddAllFields<LSCATransactionDetail>(fs => fs.IsDisabled());
//                            fs.AddField<LSCATransaction.transactionType>();
//                            fs.AddField<LSCATransaction.referenceNumber>();
//                        }));
//                    }).WithTransitions(ts =>
//                    {
//                        ts.AddGroupFrom(Initial,
//                            i => i.Add(t => t.To<CarbonTranStatus.open>().IsTriggeredOn(a => a.InitializeState)));
//                        ts.AddGroupFrom<CarbonTranStatus.open>(o =>
//                        {
//                            o.Add(s => s.To<CarbonTranStatus.released>().IsTriggeredOn(a => a.ActionRelease));
//                            o.Add(s => s.To<CarbonTranStatus.hold>().IsTriggeredOn(a => a.ActionPutOnHold));
//                        });
//                        ts.AddGroupFrom<CarbonTranStatus.hold>(o =>
//                        {
//                            o.Add(s => s.To<CarbonTranStatus.open>().IsTriggeredOn(a => a.ActionReleaseFromHold));
//                        });
//                    })).WithActions(ac =>
//                {
//                    ac.Add(a => a.ActionPutOnHold);
//                    ac.Add(a => a.ActionRelease);
//                    ac.Add(a => a.ActionReleaseFromHold);
//                }));
//        }
//    }
//}