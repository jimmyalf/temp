using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence.Queries
{
    class FetchOrderTransactions : PersistenceBase
    {
        public IEnumerable<Transaction> Execute()
        {
            var query = QueryBuilder
                .Build("SELECT * FROM SynologenOrderTransaction");
            return Query(query, Transaction.Parse);
        }
    }
}
