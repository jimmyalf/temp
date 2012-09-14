using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Queries;
using System.Linq;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class OPQDocumentHistoryEntitiesMatchingSearchQuery : Query<OPQDocumentHistoryEntity>
	{
		private readonly string _query;

		public OPQDocumentHistoryEntitiesMatchingSearchQuery(string query)
		{
			_query = query;
		}

		public override IList<OPQDocumentHistoryEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocumentHistories")
				.Where("DocumentContent LIKE @Match")
				.AddParameters(new { Match = '%' + Database.EscapeSqlString(_query) + '%' });
			return Database.Query(query, OPQDocumentHistoryEntity.Parse).ToList();
		}		
	}
}