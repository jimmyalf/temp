using System.Configuration;

namespace Synologen.Maintenance.ExternalShopLogin.Persistence
{
	public class PersistenceBase : Spinit.Data.SqlClient.PersistenceBase
	{
		public PersistenceBase() : base(ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString) {}
	}
}