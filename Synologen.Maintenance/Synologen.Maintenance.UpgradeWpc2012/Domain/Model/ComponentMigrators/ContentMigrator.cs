using System.Collections.Generic;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class ContentMigrator : IComponentMigrator<ContentEntity>
	{
		public string ComponentName
		{
			get { return "Content"; }
		}

		public IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, ContentEntity entity)
		{
			return new RenameContentCommand(entity).Execute(renamedFile.PreviousName, renamedFile.Name);
		}

		public IEnumerable<ContentEntity> GetEntitiesMatching(RenamedFileEntity renamedFile)
		{
			return new ContentEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
		}
	}
}