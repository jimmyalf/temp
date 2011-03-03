using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Factories
{
	public static class SendFileWriterServiceFactory
	{
		public static BGSentFileWriterService Get(IFileIOService fileIoService, IBGConfigurationSettingsService configurationSettingsService)
		{
			var writeDate = DateTime.Now;
			return new BGSentFileWriterService(fileIoService, configurationSettingsService, writeDate);
		}
		
	}
}