using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.BGData;

namespace Synologen.LensSubscription.BGWebService.App.IoC
{
	public class WebserviceRegistry : Registry
	{
		public WebserviceRegistry()
		{
			For<IUnitOfWork>().LifecycleIs(new WcfPerOperationLifecycle()).Use<NHibernateUnitOfWork>();
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);
		}
	}
}