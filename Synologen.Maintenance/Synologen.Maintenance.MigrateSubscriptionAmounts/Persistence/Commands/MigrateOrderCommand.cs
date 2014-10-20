using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Commands
{
	public class MigrateOrderCommand : PersistenceBase
	{
		private readonly Order _order;

		public MigrateOrderCommand(Order order)
		{
			_order = order;
		}

		public IMigratedResult Execute()
		{
			var matchingTransaction = new FetchTransactionMatchingOrderQuery(_order).Execute();
			return (matchingTransaction.NewAmount.Total != _order.OldAmount)
				? new OrderMigrationFailedResult(_order, "Transaction amount did not match order amount") 
				: MigrateOrder(matchingTransaction);
		}

		protected IMigratedResult MigrateOrder(Transaction transaction)
		{
			var command = CommandBuilder
				.Build(@"UPDATE SynologenOrder 
					SET TaxedWithdrawalAmount = @TaxedWithdrawalAmount, 
					TaxFreeWithdrawalAmount = @TaxFreeWithdrawalAmount
					WHERE Id = @Id")
				.AddParameters(new
				{
					TaxedWithdrawalAmount = transaction.NewAmount.Taxed,
					TaxFreeWithdrawalAmount = transaction.NewAmount.TaxFree, 
					_order.Id
				});
			Execute(command);
			return new OrderMigratedSuccessfullyResult(_order, transaction);
		}
	}
}