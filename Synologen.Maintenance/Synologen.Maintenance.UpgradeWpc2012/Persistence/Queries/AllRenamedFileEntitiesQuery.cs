using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class AllRenamedFileEntitiesQuery : PersistenceBase
	{
		public IList<RenamedFileEntity> Execute()
		{
			var query = QueryBuilder
				.Build("SELECT * FROM tblBaseFile")
				.Where("cPreviousName IS NOT NULL");
			return Query(query, RenamedFileEntity.Parse).ToList();
		}
	}
}