using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class CourseEntitiesMatchingSearchQuery : PersistenceBase
	{
		private readonly string _query;

		public CourseEntitiesMatchingSearchQuery(string query)
		{
			_query = query;
		}

		public IEnumerable<CourseEntity> Execute()
		{
			var query = QueryBuilder
				.Build(@"SELECT cId, cBody, cFormatedBody, cHeading FROM tblCourse")
				.Where("cBody LIKE @Match")
				.Where("cFormatedBody LIKE @Match")
				.AddParameters(new { Match = '%' + EscapeSqlString(_query) + '%' });
			return Query(query, CourseEntity.Parse).ToList();
		}		 
	}
}