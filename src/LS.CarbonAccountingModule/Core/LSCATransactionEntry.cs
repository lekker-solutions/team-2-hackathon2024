using System;
using System.Collections.Generic;
using LS.CarbonAccountingModule.DAC;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace LS.CarbonAccountingModule
{
    public class LSCATransactionEntry : PXGraph<LSCATransactionEntry, LSCATransaction>
    {
        public SelectFrom<LSCATransaction>.View Document;

        public SelectFrom<LSCATransactionDetail>
            .Where<LSCATransactionDetail.transactionType
                .IsEqual<LSCATransaction.transactionType.FromCurrent>
                .And<LSCATransactionDetail.referenceNumber.IsEqual<LSCATransaction.referenceNumber.FromCurrent>>>.View
            Transactions;

        public static void CreateCarbonTransaction<TSource>(TSource                            source,
                                                            DateTime?                          transactionDate,
                                                            IEnumerable<LSCATransactionDetail> transactions)
            where TSource : INotable
        {
        }
    }
}