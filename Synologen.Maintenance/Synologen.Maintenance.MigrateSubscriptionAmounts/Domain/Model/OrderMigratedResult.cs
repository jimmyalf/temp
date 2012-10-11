namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class OrderMigratedResult
	{
		private readonly Order _order;
		private readonly OrderTransaction _transaction;
		private readonly SubscriptionItem _subscriptionItem;

		public OrderMigratedResult(Order order, OrderTransaction transaction, SubscriptionItem subscriptionItem)
		{
			_order = order;
			_transaction = transaction;
			_subscriptionItem = subscriptionItem;
		}

		public override string ToString()
		{
			return string.Format(
				"Order[{0}] was migrated with amounts from Transaction[{1}] and SubscriptionItem[{2}]", 
				_order.Id, 
				_transaction.Id,
				_subscriptionItem.Id);
		}
	}
}