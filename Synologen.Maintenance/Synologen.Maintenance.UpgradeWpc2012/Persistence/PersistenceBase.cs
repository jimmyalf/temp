using Synologen.Maintenance.UpgradeWpc2012.Domain.Settings;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence
{
	public class PersistenceBase : Spinit.Data.SqlClient.PersistenceBase
	{
		public PersistenceBase() : base(Settings.ConnectionString) {}

		protected virtual string EscapeSqlString(string input)
		{
			return input
				.Replace("[","[[]")
				.Replace("_","[_]")
				.Replace("%","[%]");
		}
	}
}