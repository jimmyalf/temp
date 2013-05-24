using NHibernate;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Services.Client;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.Orders;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap.Configuration.DSL;
using Synologen.Service.Client.OrderEmailSender.Factory;
using Synologen.Service.Client.OrderEmailSender.Logging;
using Synologen.Service.Client.OrderEmailSender.Services;

namespace Synologen.Service.Client.OrderEmailSender.Application.IoC
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
            For<ISession>().Use(x => x.GetInstance<ISessionFactory>().OpenSession());
            For<IOrderRepository>().Use<OrderRepository>();
        	For<IConfigurationSettings>().Use<ConfigurationSettings>();
        	For<EmailClient2>().Use(x => EmailClientFactory.Create(x.GetInstance<IConfigurationSettings>()));
        	For<ISendOrderService>().Use<SendOrderService>();
			For<ILoggingService>().Singleton().Use(LogFactory.CreateLoggingService);

			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<AllOrdersToSendEmailForCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
        }
    }
}