using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using System.Linq;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Queries;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class OPQDocumentEntitiesMatchingSearchQuery : Query<OPQDocumentEntity>
	{
		private readonly string _query;

		public OPQDocumentEntitiesMatchingSearchQuery(string query)
		{
			_query = query;
		}

		public override IList<OPQDocumentEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocuments")
				.Where("DocumentContent LIKE @Match")
				.AddParameters(new { Match = '%' + Database.EscapeSqlString(_query) + '%' });
			return Database.Query(query, OPQDocumentEntity.Parse).ToList();
		}
	}
}