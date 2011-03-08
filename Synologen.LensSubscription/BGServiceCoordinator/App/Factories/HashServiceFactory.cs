using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.Autogiro.Security;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Factories
{
	public static class HashServiceFactory
	{
		public static IHashService Get(IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService)
		{
			var hashKey = serviceCoordinatorSettingsService.GetHMACHashKey();
			return new HMACHashService(hashKey);
		}
	}
}