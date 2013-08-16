using NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using StructureMap.Configuration.DSL;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.App.IoC
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			// Setup NHibernate Session Management
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => x.GetInstance<ISessionFactory>().OpenSession());
		}
	}
}