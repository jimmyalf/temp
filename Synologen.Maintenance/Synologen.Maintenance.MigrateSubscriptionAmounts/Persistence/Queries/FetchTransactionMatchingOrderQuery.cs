using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model.Enums;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
	public class FetchTransactionMatchingOrderQuery : PersistenceBase
	{
		private readonly Order _order;

		public FetchTransactionMatchingOrderQuery(Order order)
		{
			_order = order;
		}

		public Transaction Execute()
		{
			var query = QueryBuilder
                .Build("SELECT TOP 1 * FROM SynologenOrderTransaction")
				.Where("DATEDIFF(day, @CreatedDate, CreatedDate) >= 0")
				.Where("SubscriptionId = @SubscriptionId")
				.Where("Amount = @Amount")
				.Where("Type = @TransactionType")
				.Where("Reason = @TransactionReason")
				.AddParameters(new
				{
					CreatedDate = _order.Created, 
					SubscriptionId = _order.SubscripitonId,
					Amount = _order.OldAmount,
					TransactionType = TransactionType.Withdrawal,
					TransactionReason = TransactionReason.Withdrawal,
				});
            return Query(query, Transaction.Parse).Single();
		}
	}
}