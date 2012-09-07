using System.Collections.Generic;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.ComponentMigrators
{
	public interface IComponentMigrator<TEntity> where TEntity : IEntity
	{
		string ComponentName { get; }
		IEntityMigratedResult MigrateEntity(RenamedFileEntity renamedFile, TEntity entity);
		IEnumerable<TEntity> GetEntitiesToBeMigrated(RenamedFileEntity renamedFile);	
	}
}