using System.Configuration;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence
{
	public class PersistenceBase : Spinit.Data.SqlClient.PersistenceBase
	{
		public PersistenceBase() : base(ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString) {}
	}
}