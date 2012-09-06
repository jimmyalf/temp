using System.Collections.Generic;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries
{
	public class AllCourseEntitiesQuery : PersistenceBase
	{
		 public IEnumerable<CourseEntity> Execute()
		 {
			var query = QueryBuilder.Build(@"SELECT cId, cBody, cFormatedBody, cHeading FROM tblCourse");
		 	return Query(query, CourseEntity.Parse).ToList();
		 }		 
	}
}