using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class NewsEntitiesMatchingSearchQuery : SearchQueryBase
	{
		public NewsEntitiesMatchingSearchQuery(string query) : base(query) { }

		public IEnumerable<NewsEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT cId, cHeading FROM tblNews")
				.Where("cBody LIKE @Match")
				.Where("cFormatedBody LIKE @Match")
				.AddParameters(new { Match = '%' + ParsedQuery + '%' });
			return Query(query, NewsEntity.Parse).ToList();
		}
	}
}