using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Commands
{
	public class MigrateOrderCommand
	{
		private readonly Order _order;

		public MigrateOrderCommand(Order order)
		{
			_order = order;
		}

		public OrderMigratedResult Execute()
		{
			//Fetch transaction made on the same day as order was created
			var matchingTransaction = new FetchTransactionMatchingOrderQuery(_order).Execute();
			var matchingSubscriptionItem = new FetchSubscriptionItemMatchingOrderQuery(_order).Execute();
			MigrateOrder(matchingTransaction, matchingSubscriptionItem);
			return new OrderMigratedResult(_order, matchingTransaction, matchingSubscriptionItem);
		}

		protected void MigrateOrder(OrderTransaction transaction, SubscriptionItem subscriptionItem)
		{
			//TODO
		}
	}
}