using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
	public class FetchSubscriptionItemMatchingOrderQuery : PersistenceBase
	{
		private readonly Order _order;

		public FetchSubscriptionItemMatchingOrderQuery(Order order)
		{
			_order = order;
		}

		public SubscriptionItem Execute()
		{            
			var query = QueryBuilder
				.Build("SELECT * FROM SynologenOrderSubscriptionItem")
				.Where("Id = @Id")
				.AddParameters(new {Id = _order.SubscriptionItemId});
            return Query(query, SubscriptionItem.Parse).Single();
		}
	}
}