using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap;
using Synologen.Service.Client.OrderEmailSender.Application.IoC;

namespace Synologen.Service.Client.OrderEmailSender
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
			//ObjectFactory.Initialize(x => x.AddRegistry<AppRegistry>());
            //BootstrapNHibernate();
			NHibernateFactory.MappingAssemblies.Add(typeof(OrderRepository).Assembly);
			ObjectFactory.Initialize(x => x.AddRegistry<AppRegistry>());
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
        }

		//private static void BootstrapNHibernate()
		//{
		//    NHibernateFactory.MappingAssemblies.Add(typeof(OrderRepository).Assembly);
		//    ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		//}
    }
}