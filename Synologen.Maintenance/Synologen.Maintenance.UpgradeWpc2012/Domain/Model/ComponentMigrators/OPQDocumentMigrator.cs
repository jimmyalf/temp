using System.Collections.Generic;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Entities;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class OPQDocumentMigrator : IEntityMigrator<OPQDocumentEntity>
	{
		public string ComponentName { get { return "OPQDocument"; } }

		public IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, OPQDocumentEntity entity)
		{
			return new RenameOPQDocumentCommand(entity).Execute(renamedFile.PreviousName, renamedFile.Name);
		}

		public IEnumerable<OPQDocumentEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return new OPQDocumentEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
		}
	}
}