using System.Collections.Generic;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class NewsMigrator : IComponentMigrator<NewsEntity>
	{
		public string ComponentName { get { return "News"; } }

		public IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, NewsEntity entity)
		{
			return new RenameNewsCommand(entity).Execute(renamedFile.PreviousName, renamedFile.Name);
		}

		public IEnumerable<NewsEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return new NewsEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
		}
	}
}