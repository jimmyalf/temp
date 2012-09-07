using System.Collections.Generic;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands;
using Synologen.Maintenance.UpgradeWpc2012.Persistence.Queries;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public class MemberMigrator : IComponentMigrator<MemberEntity>
	{
		public string ComponentName { get { return "Member"; } }

		public IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, MemberEntity entity)
		{
			return new RenameMemberContentCommand(entity).Execute(renamedFile.PreviousName, renamedFile.Name);
		}

		public IEnumerable<MemberEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile)
		{
			return new MemberContentEntitiesMatchingSearchQuery(renamedFile.PreviousName).Execute();
		}
	}
}