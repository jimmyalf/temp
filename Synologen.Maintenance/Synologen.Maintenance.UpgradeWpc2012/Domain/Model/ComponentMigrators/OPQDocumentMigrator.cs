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
	public class OPQDocumentMigrator : IEntityMigrator<OPQDocumentEntity,OPQDocumentMigratedResult>
	{
		public string EntityName { get { return "OPQDocument"; } }

		public IExecutor Executor { get; set; }

		public OPQDocumentMigratedResult MigrateEntity(RenamedFileEntity renamedFile, OPQDocumentEntity entity)
		{
			return Executor.Execute(new RenameOPQDocumentCommand(entity, renamedFile.PreviousName, renamedFile.Name));
		}

		public IEnumerable<OPQDocumentEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return Executor.Query(new OPQDocumentEntitiesMatchingSearchQuery(renamedFile.PreviousName));
		}
	}
}