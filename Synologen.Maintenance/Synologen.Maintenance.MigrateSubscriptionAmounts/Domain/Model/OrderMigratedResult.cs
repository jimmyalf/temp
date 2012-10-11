namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class OrderMigratedResult
	{
		private readonly Order _order;

		public OrderMigratedResult(Order order)
		{
			_order = order;
		}

		public override string ToString()
		{
			return string.Format("Order[{0}] was migrated", _order.Id);
		}
	}
}