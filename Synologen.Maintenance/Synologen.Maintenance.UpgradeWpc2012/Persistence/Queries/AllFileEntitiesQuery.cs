using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class AllFileEntitiesQuery : PersistenceBase
	{
		public IList<FileEntity> Execute()
		{
			var query = QueryBuilder.Build("SELECT * FROM tblBaseFile");
			return Query(query, FileEntity.Parse).ToList();
		}
	}
}