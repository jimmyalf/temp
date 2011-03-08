using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGData.Repositories;

namespace Synologen.LensSubscription.BGWebService.App.IoC
{
	public class WebServiceRegistry : Registry
	{
		public WebServiceRegistry()
		{
			//NHibernate
			For<IUnitOfWork>().LifecycleIs(new WcfPerOperationLifecycle()).Use<NHibernateUnitOfWork>();
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);

			//Repositories
			For<IAutogiroPayerRepository>().Use<AutogiroPayerRepository>();
		}
	}
}