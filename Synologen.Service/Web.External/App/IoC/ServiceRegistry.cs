using System.ServiceModel.Dispatcher;
using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap.Configuration.DSL;
using Synologen.Service.Web.External.App.Services;
using log4net;

namespace Synologen.Service.Web.External.App.IoC
{
	public class ServiceRegistry : Registry
	{
		public ServiceRegistry()
		{
			//WCF
			For<IErrorHandler>().Use<ErrorHandler>();

			//NHibernate
			For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
			For<IUnitOfWork>().LifecycleIs(new WcfPerOperationLifecycle()).Use<NHibernateUnitOfWork>();
			For<ISession>().Use(x => ((NHibernateUnitOfWork)x.GetInstance<IUnitOfWork>()).Session);

			//Misc
			For<IShopAuthenticationService>().Use<ShopAuthenticationService>();
			For<ICustomerParser>().Use<CustomerParser>();
			For<IValidator<Customer>>().Use<CustomerValidator>();
			For<IHashService>().Use<SHA1HashService>();
			For<ILoggingService>().Use<Log4NetLogger>();
			For<ILog>().Use(LogManager.GetLogger("Synologen.Service.Web.External"));

			//Repositories
			For<IOrderCustomerRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<OrderCustomerRepository>();
			For<IShopRepository>().LifecycleIs(new WcfPerOperationLifecycle()).Use<ShopRepository>();
			
			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<OrderCustomerRepository>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}
	}
}