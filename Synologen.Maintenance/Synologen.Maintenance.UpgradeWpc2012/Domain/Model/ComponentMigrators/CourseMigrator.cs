using System.Collections.Generic;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class CourseMigrator : IComponentMigrator<CourseEntity>
	{
		public string ComponentName { get { return "Course"; } }

		public IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, CourseEntity entity)
		{
			return new RenameCourseCommand(entity).Execute(renamedFile.PreviousName, renamedFile.Name);
		}

		public IEnumerable<CourseEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return new CourseEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
		}
	}
}