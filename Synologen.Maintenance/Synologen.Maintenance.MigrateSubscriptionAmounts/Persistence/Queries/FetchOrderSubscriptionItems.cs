using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
    class FetchOrderSubscriptionItems : PersistenceBase
    {
        public IEnumerable<SubscriptionItem> Execute()
        {
            var query = QueryBuilder
                .Build("SELECT * FROM SynologenOrderSubscriptionItem");
            return Query(query, SubscriptionItem.Parse);
        }
    }
}
