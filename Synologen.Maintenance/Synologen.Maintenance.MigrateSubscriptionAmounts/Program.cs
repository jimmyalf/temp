using System.Collections.Generic;
using System.Linq;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts
{
	class Program
	{
		static void Main(string[] args)
		{
			var migrator = new Migrator();
			var results = migrator.MigrateOrders();

			
			var OrderTransactions = new FetchOrderTransactions().Execute();
		    var OrderSubscriptionItems = new FetchOrderSubscriptionItems().Execute();
		    List<PendingPayment> PendingPayments = new FetchPendingPayments().Execute().ToList();
		    foreach (var orderTransaction in OrderTransactions)
		    {
                if (orderTransaction.PendingPaymentId != null)
                {
                	//orderTransaction = HandlePendingPayment(orderTransaction, PendingPayments);
                }
                else if (orderTransaction.Reason == 3) HandleCorrection(orderTransaction);
                else if (orderTransaction.Reason == 2) HandleWithdrawal(orderTransaction);
		    }
			
		}

        static Transaction HandlePendingPayment(Transaction transaction, IEnumerable<PendingPayment> pendingPayments)
        {
            var RelevantPendingPayment = pendingPayments.First(x => x.Id == transaction.PendingPaymentId);
            transaction.NewAmount.Taxed = RelevantPendingPayment.TaxedAmount;
            transaction.NewAmount.TaxFree = RelevantPendingPayment.UntaxedAmount;
            return transaction;
        }

        static Transaction HandleCorrection(Transaction transaction)
        {
            transaction.NewAmount.Taxed = transaction.OldAmount;
            return transaction;
        }

        static void HandleWithdrawal(Transaction transaction)
        {
            
        }
	}
}
