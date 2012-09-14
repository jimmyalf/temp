using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence.Queries;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence.Queries
{
	public class AllOPQDocumentHistoryEntitiesQuery : Query<OPQDocumentHistoryEntity>
	{
		public override IList<OPQDocumentHistoryEntity> Execute()
		{
			var query = QueryBuilder.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocumentHistories");
			return Database.Query(query, OPQDocumentHistoryEntity.Parse).ToList();
		}		 
	}
}