using System.Collections.Generic;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
    class FetchOrderTransactions : PersistenceBase
    {
        public IEnumerable<OrderTransaction> Execute()
        {
            var query = QueryBuilder
                .Build("SELECT * FROM SynologenOrderTransaction");
            return Query(query, OrderTransaction.Parse);
        }
    }
}
