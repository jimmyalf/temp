using EnterpriseDT.Net.Ftp;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Client.BGCTaskRunner.App.Factories
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