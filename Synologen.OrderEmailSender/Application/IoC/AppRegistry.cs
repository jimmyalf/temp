using NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap.Configuration.DSL;

namespace Synologen.OrderEmailSender.Application.IoC
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            For<ISessionFactory>().Singleton().Use(NHibernateFactory.Instance.GetSessionFactory);
            For<ISession>().Use(x => x.GetInstance<ISessionFactory>().OpenSession());
            For<IOrderRepository>().Use<OrderRepository>();
        }
    }
}