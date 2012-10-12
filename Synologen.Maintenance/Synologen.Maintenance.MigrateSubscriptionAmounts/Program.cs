using System.Collections.Generic;
using System.Linq;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model.Enums;
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

			
            var OrderTransactions = new FetchOrderTransactions().Execute().ToList();
            var OrderSubscriptionItems = new FetchOrderSubscriptionItems().Execute().ToList();
		    var PendingPayments = new FetchPendingPayments().Execute().ToList();
		    var UpdatedTransactions = new List<Transaction>();

		    foreach (var orderTransaction in OrderTransactions)
		    {
                if (orderTransaction.PendingPaymentId != null) 
                	UpdatedTransactions.Add(HandlePendingPayment(orderTransaction, PendingPayments)); 
				//else if (orderTransaction.Reason == TransactionReason.Correction) 
				//    UpdatedTransactions.Add(HandleCorrection(orderTransaction));
				//else
				//{
				//    var trans = orderTransaction;
				//}
				//else if (orderTransaction.Reason == TransactionReason.Withdrawal) 
				//    UpdatedTransactions.Add(HandleWithdrawal(orderTransaction, OrderSubscriptionItems)); 
		    }
			
		}

        static Transaction HandlePendingPayment(Transaction transaction, IEnumerable<PendingPayment> pendingPayments)
        {
            var RelevantPendingPayment = pendingPayments.First(x => x.Id == transaction.PendingPaymentId);
            transaction.NewAmount = RelevantPendingPayment.Amount;
            return transaction;
        }

        static Transaction HandleCorrection(Transaction transaction)
        {
            transaction.NewAmount.Taxed = transaction.OldAmount;
            transaction.NewAmount.TaxFree = 0; // unneccessary?
            return transaction;
        }

        static Transaction HandleWithdrawal(Transaction transaction, IEnumerable<SubscriptionItem> orderSubscriptionItems)
        {
            foreach (var OrderSubscriptionItem in orderSubscriptionItems)
            {
                if(
                    (OrderSubscriptionItem.SubscriptionId == transaction.SubscriptionId)
                    && (OrderSubscriptionItem.Amount.TaxFree + OrderSubscriptionItem.Amount.Taxed == transaction.OldAmount))
                {
                    transaction.NewAmount.TaxFree += OrderSubscriptionItem.Amount.TaxFree;
                    transaction.NewAmount.Taxed += OrderSubscriptionItem.Amount.Taxed;
                    break;
                }
            } // this can all be done much more prettily with Linq but it's probably not worth the time
            return transaction;
        }
	}
}
