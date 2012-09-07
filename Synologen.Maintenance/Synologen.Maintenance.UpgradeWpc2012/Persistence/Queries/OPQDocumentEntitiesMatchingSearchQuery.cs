using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class OPQDocumentEntitiesMatchingSearchQuery : PersistenceBase
	{
		private readonly string _query;

		public OPQDocumentEntitiesMatchingSearchQuery(string query)
		{
			_query = query;
		}

		public IEnumerable<OPQDocumentEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocuments")
				.Where("DocumentContent LIKE @Match")
				.AddParameters(new { Match = '%' + EscapeSqlString(_query) + '%' });
			return Query(query, OPQDocumentEntity.Parse).ToList();
		}
	}
}