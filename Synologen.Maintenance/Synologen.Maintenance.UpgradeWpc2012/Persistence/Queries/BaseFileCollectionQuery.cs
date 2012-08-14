using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class BaseFileCollectionQuery : PersistenceBase
	{
		public FileCollection Execute()
		{
			var query = QueryBuilder.Build("SELECT * FROM tblBaseFile");
			var fileEntries = Query(query, FileEntry.Parse).ToList();
			return new FileCollection(fileEntries);
		}
	}
}