namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class OrderMigratedResult
	{
		private readonly Order _order;

		public OrderMigratedResult(Order order)
		{
			_order = order;
		}
	}
}