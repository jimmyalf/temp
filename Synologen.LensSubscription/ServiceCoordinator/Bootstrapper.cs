using Spinit.Data;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using StructureMap;
using Synologen.LensSubscription.ServiceCoordinator.App.IoC;

namespace Synologen.LensSubscription.ServiceCoordinator
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			NHibernateFactory.MappingAssemblies.Add(typeof(CountryRepository).Assembly);
			ObjectFactory.Initialize(x => x.AddRegistry<TaskRunnerRegistry>());
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}
		
	}
}