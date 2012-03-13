namespace Synologen.Migration.AutoGiro2.Migrators
{
	public interface IMigrator<TOldEntity,TNewEntity>
	{
		TNewEntity GetNewEntity(TOldEntity oldEntity);
	}
}