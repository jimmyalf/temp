using System.Collections.Generic;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Entities;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class OPQDocumentHistoryMigrator : IEntityMigrator<OPQDocumentHistoryEntity>
	{
		public string ComponentName { get { return "OPQDocumentHistory"; } }

		public IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, OPQDocumentHistoryEntity entity)
		{
			return new RenameOPQDocumentHistoryCommand(entity).Execute(renamedFile.PreviousName, renamedFile.Name);
		}

		public IEnumerable<OPQDocumentHistoryEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return new OPQDocumentHistoryEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
		}
	}
}