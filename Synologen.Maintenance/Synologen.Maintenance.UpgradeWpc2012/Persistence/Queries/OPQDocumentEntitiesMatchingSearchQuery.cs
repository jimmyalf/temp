using System.Collections.Generic;
using Spinit.Data.SqlClient.SqlBuilder;
using System.Linq;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Queries;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class OPQDocumentEntitiesMatchingSearchQuery : ContentQuery<OPQDocumentEntity>
	{

		public OPQDocumentEntitiesMatchingSearchQuery(string query) : base(query) { }

		public override IList<OPQDocumentEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocuments")
				.Where("DocumentContent LIKE @Match OR DocumentContent LIKE @UrlEncodedMatch")
				.AddParameters(new { Match, UrlEncodedMatch });
			return Database.Query(query, OPQDocumentEntity.Parse).ToList();
		}
	}
}