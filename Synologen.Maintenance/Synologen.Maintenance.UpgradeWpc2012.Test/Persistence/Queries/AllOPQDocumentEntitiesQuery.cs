using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence.Queries
{
	public class AllOPQDocumentEntitiesQuery : PersistenceBase
	{
		public IEnumerable<OPQDocumentEntity> Execute()
		{
			var query = QueryBuilder.Build(@"SELECT Id, DocumentContent FROM SynologenOpqDocuments");
			return Query(query, OPQDocumentEntity.Parse).ToList();
		}		 
	}
}