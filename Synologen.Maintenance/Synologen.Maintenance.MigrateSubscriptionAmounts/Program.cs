using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts
{
	class Program
	{
		static void Main(string[] args)
		{
		    var orderTransactions = new FetchOrderTransactions().Execute();
		    var orderSubscriptionItems = new FetchOrderSubscriptionItems().Execute();
		    foreach (var orderTransaction in orderTransactions)
		    {
                if (orderTransaction.PendingPaymentId != null) /* do something with amount */ ;
                else if (orderTransaction.Reason == 3) HandleCorrection(orderTransaction);
                else if (orderTransaction.Reason == 2) HandleWithdrawal(orderTransaction);
		    }
		}

        static void HandleCorrection(OrderTransaction orderTransaction)
        {
            // all taxed
        }

        static void HandleWithdrawal(OrderTransaction orderTransaction)
        {
            
        }
	}
}
