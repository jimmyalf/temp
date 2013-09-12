using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGData.CriteriaConverters;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.Service.Web.BGC.App.Logging;
using Synologen.Service.Web.BGC.App.Services;

namespace Synologen.Service.Web.BGC.App.IoC
{
	public class WebServiceRegistry : Registry
	{
		public WebServiceRegistry()
		{
			//NHibernate
			For<IUnitOfWork>().LifecycleIs(new WcfPerOperationLifecycle()).Use<NHibernateUnitOfWork>();
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);

			//Repositories and services
			For<IAutogiroPayerRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<AutogiroPayerRepository>();
			For<IBGConsentToSendRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<BGConsentToSendRepository>();
			For<IBGPaymentToSendRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<BGPaymentToSendRepository>();
			For<IBGReceivedPaymentRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<BGReceivedPaymentRepository>();
			For<IBGReceivedErrorRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<BGReceivedErrorRepository>();
			For<IBGReceivedConsentRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<BGReceivedConsentRepository>();
			For<IBGWebServiceDTOParser>().Use<BGWebServiceDTOParser>();
			For<ILoggingService>().Singleton().Use(LogFactory.CreateLoggingService);

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<AllNewReceivedBGErrorsMatchingServiceTypeCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}
	}
}