using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts
{
	class Program
	{
		static void Main(string[] args)
		{
		    var OrderTransactions = new FetchOrderTransactions().Execute();
		    var OrderSubscriptionItems = new FetchOrderSubscriptionItems().Execute();
		    List<PendingPayment> PendingPayments = new FetchPendingPayments().Execute().ToList();
		    foreach (var orderTransaction in OrderTransactions)
		    {
                if (orderTransaction.PendingPaymentId != null) orderTransaction = HandlePendingPayment(orderTransaction, PendingPayments);
                else if (orderTransaction.Reason == 3) HandleCorrection(orderTransaction);
                else if (orderTransaction.Reason == 2) HandleWithdrawal(orderTransaction);
		    }
			var migrator = new Migrator();
			migrator.MigrateOrders();
		}

        static OrderTransaction HandlePendingPayment(OrderTransaction orderTransaction, IEnumerable<PendingPayment> pendingPayments)
        {
            var RelevantPendingPayment = pendingPayments.First(x => x.Id == orderTransaction.PendingPaymentId);
            orderTransaction.TaxedAmount = RelevantPendingPayment.TaxedAmount;
            orderTransaction.UntaxedAmount = RelevantPendingPayment.UntaxedAmount;
            return orderTransaction;
        }

        static OrderTransaction HandleCorrection(OrderTransaction orderTransaction)
        {
            orderTransaction.TaxedAmount = orderTransaction.Amount;
            return orderTransaction;
        }

        static void HandleWithdrawal(OrderTransaction orderTransaction)
        {
            
        }
	}
}
