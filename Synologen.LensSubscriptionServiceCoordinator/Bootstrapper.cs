using Spinit.Data;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.App.Ioc;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
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