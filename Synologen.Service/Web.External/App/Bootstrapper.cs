using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using StructureMap;
using Synologen.Service.Web.External.App.IoC;

namespace Synologen.Service.Web.External.App
{
	public static class Bootstrapper
	{
		public static void Boostrap()
		{
			InitializeCritieriaConverters();
			InitializeDependencyInjection();
		}

		public static void InitializeDependencyInjection()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<ServiceRegistry>());
		}

		public static void InitializeCritieriaConverters()
		{
			NHibernateFactory.MappingAssemblies.Add(typeof(OrderRepository).Assembly);
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
			
		}
	}
}