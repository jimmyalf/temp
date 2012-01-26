using NHibernate;
using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap;
using Synologen.OrderEmailSender.Application.IoC;

namespace Synologen.OrderEmailSender
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            BootstrapNHibernate();
            ObjectFactory.Initialize(x => x.AddRegistry<AppRegistry>());
            
        }

        private static void BootstrapNHibernate()
        {
            NHibernateFactory.MappingAssemblies.Add(typeof(OrderRepository).Assembly);
            ActionCriteriaExtensions.ConstructConvertersUsing(
                ObjectFactory.GetInstance
            );
        }
    }
}