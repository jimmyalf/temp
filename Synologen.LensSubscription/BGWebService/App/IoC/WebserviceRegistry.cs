using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.BGData;
using Synologen.LensSubscription.BGData.CriteriaConverters;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGWebService.App.Services;

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

			//Repositories and services
			For<IAutogiroPayerRepository>().Use<AutogiroPayerRepository>();
			For<IBGConsentToSendRepository>().Use<BGConsentToSendRepository>();
			For<IBGPaymentToSendRepository>().Use<BGPaymentToSendRepository>();
			For<IBGReceivedPaymentRepository>().Use<BGReceivedPaymentRepository>();
			For<IBGReceivedErrorRepository>().Use<BGReceivedErrorRepository>();
			For<IBGReceivedConsentRepository>().Use<BGReceivedConsentRepository>();
			For<IBGWebServiceDTOParser>().Use<BGWebServiceDTOParser>();

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