using System.Collections.Generic;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.ComponentMigrators;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Domain.Model.Entities;
using Spinit.Wpc.Maintenance.FileAndContentMigration.Persistence;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class OPQDocumentHistoryMigrator : IEntityMigrator<OPQDocumentHistoryEntity,OPQDocumentHistoryMigratedResult>
	{
		public string EntityName { get { return "OPQDocumentHistory"; } }

		public IExecutor Executor { get; set; }

		public OPQDocumentHistoryMigratedResult MigrateEntity(RenamedFileEntity renamedFile, OPQDocumentHistoryEntity entity)
		{
			return Executor.Execute(new RenameOPQDocumentHistoryCommand(entity, renamedFile.PreviousName, renamedFile.Name));
		}

		public IEnumerable<OPQDocumentHistoryEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return Executor.Query(new OPQDocumentHistoryEntitiesMatchingSearchQuery(renamedFile.PreviousName));
		}
	}
}