using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
	public class FetchActiveOrdersQuery : PersistenceBase
	{
		 public IEnumerable<Order> Execute()
		 {
			var query = QueryBuilder
				.Build("SELECT * FROM SynologenOrder")
				.Where("Status = 1");
			return Query(query, Order.Parse);
		 }
	}
}