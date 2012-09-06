using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class NewsEntitiesMatchingSearchQuery : PersistenceBase
	{
		private readonly string _query;

		public NewsEntitiesMatchingSearchQuery(string query)
		{
			_query = query;
		}

		public IEnumerable<NewsEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT cId, cBody, cFormatedBody, cHeading FROM tblNews")
				.Where("cBody LIKE @Match")
				.Where("cFormatedBody LIKE @Match")
				.AddParameters(new { Match = '%' + EscapeSqlString(_query) + '%' });
			return Query(query, NewsEntity.Parse).ToList();
		}
	}
}