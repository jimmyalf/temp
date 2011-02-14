using Spinit.Data;
using StructureMap;
using Synologen.LensSubscription.ServiceCoordinator.App.IoC;

namespace Synologen.LensSubscription.ServiceCoordinator
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<TaskRunnerRegistry>());
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}
		
	}
}