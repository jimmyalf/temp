using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Queries;
using System.Linq;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class OPQDocumentHistoryEntitiesMatchingSearchQuery : ContentQuery<OPQDocumentHistoryEntity>
	{
		public OPQDocumentHistoryEntitiesMatchingSearchQuery(string query) : base(query) {}

		public override IList<OPQDocumentHistoryEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocumentHistories")
				.Where("DocumentContent LIKE @Match OR DocumentContent LIKE @UrlEncodedMatch")
				.AddParameters(new { Match, UrlEncodedMatch });
			return Database.Query(query, OPQDocumentHistoryEntity.Parse).ToList();
		}		
	}
}