using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace Synologen.Migration.AutoGiro2.Migrators
{
	public abstract class MigratorBase<TOldEntity,TNewEntity> : CommandQueryBase, 
		IMigrator<TOldEntity,TNewEntity>,
		IEnumerable<MigratedEntityPair<TOldEntity,TNewEntity>>
	{
		private readonly Dictionary<TOldEntity, TNewEntity> _migrationDictionary;

		public MigratorBase(ISession session) : base(session)
		{
			_migrationDictionary = new Dictionary<TOldEntity, TNewEntity>();
		}

		public void Migrate(IEnumerable<TOldEntity> oldEntities)
		{
			foreach (var oldEntity in oldEntities)
			{
				var newEntity = Migrate(oldEntity);
				_migrationDictionary.Add(oldEntity, newEntity);
			}
		}

		public TNewEntity GetNewEntity(TOldEntity oldEntity)
		{
			if (_migrationDictionary.ContainsKey(oldEntity)) return _migrationDictionary[oldEntity];
			var newEntity = Migrate(oldEntity);
			_migrationDictionary.Add(oldEntity,newEntity);
			return newEntity;
		}

		protected abstract TNewEntity Migrate(TOldEntity oldEntity);

		public IEnumerator<MigratedEntityPair<TOldEntity, TNewEntity>> GetEnumerator()
		{
			return _migrationDictionary
				.Select(entity => new MigratedEntityPair<TOldEntity, TNewEntity>(entity.Key, entity.Value))
				.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class MigratedEntityPair<TOldEntity,TNewEntity>
	{
		public TOldEntity OldEntity { get; set; }
		public TNewEntity NewEntity { get; set; }

		public MigratedEntityPair(TOldEntity oldEntity, TNewEntity newEntity)
		{
			OldEntity = oldEntity;
			NewEntity = newEntity;
		}
	}

}