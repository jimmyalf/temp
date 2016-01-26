namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class OrderMigratedSuccessfullyResult : IMigratedResult
	{
		private readonly Order _order;
		private readonly Transaction _transaction;

		public OrderMigratedSuccessfullyResult(Order order, Transaction transaction)
		{
			_order = order;
			_transaction = transaction;
		}

		public override string ToString()
		{
			return string.Format("Order[{0}] was migrated with amounts from Transaction[{1}]" 
				,_order.Id, _transaction.Id);
		}

		public bool Success
		{
			get { return true; }
		}
	}
}