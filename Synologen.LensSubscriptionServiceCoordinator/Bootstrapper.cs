using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Ioc;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<ServiceRegistry>());
		}
		
	}
}