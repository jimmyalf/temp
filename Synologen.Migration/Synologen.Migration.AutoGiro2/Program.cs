using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders;

namespace Synologen.Migration.AutoGiro2
{
	class Program
	{
		static void Main(string[] args)
		{
			NHibernateFactory.MappingAssemblies.Add(typeof(SubscriptionMap).Assembly);
			var nhibernateFactory = new NHibernateFactory();
			var sessionFactory = nhibernateFactory.GetSessionFactory();
			var session = sessionFactory.OpenSession();
			var migration = new MigrationRunner(session);
			migration.Run();
		}
	}
}
