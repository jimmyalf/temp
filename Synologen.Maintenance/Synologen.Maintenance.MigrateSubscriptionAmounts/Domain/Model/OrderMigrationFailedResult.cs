namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class OrderMigrationFailedResult : IMigratedResult
	{
		private readonly Order _order;
		private readonly string _cause;

		public OrderMigrationFailedResult(Order order, string cause)
		{
			_order = order;
			_cause = cause;
		}

		public override string ToString()
		{
			return string.Format("Order[{0}] could not be migrated because: {1}", _order.Id, _cause);
		}

		public bool Success
		{
			get { return false; }
		}
	}
}