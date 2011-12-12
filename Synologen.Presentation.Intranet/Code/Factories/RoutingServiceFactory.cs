using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code.Factories
{
	public static class RoutingServiceFactory
	{
		 public static IRoutingService GetCachedRoutingService()
		 {
			var routingService = new RoutingService();
			return new CachedRoutingService(routingService);
		 }
	}
}