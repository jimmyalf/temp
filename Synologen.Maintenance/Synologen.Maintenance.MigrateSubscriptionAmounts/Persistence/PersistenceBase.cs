using System.Configuration;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence
{
	public class PersistenceBase : Spinit.Data.SqlClient.PersistenceBase
	{
		public PersistenceBase() : base(ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString) {}
	}
}