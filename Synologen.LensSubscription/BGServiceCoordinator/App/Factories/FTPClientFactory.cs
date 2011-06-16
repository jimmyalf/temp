using EnterpriseDT.Net.Ftp;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Factories
{
	public static class FTPClientFactory
	{
		public static FTPClient GetClient(IBGServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService)
		{
			var remoteHost = bgServiceCoordinatorSettingsService.GetFtpUploadFolderUrl();
			return new FTPClient {RemoteHost = remoteHost};
		}
	}
}