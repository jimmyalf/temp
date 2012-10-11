using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
    internal class FetchPendingPayments : PersistenceBase
    {
        public IEnumerable<PendingPayment> Execute()
        {
            var query = QueryBuilder
                .Build("SELECT * FROM SynologenPendingPayments");
            return Query(query, PendingPayment.Parse);
        }
    }
}
