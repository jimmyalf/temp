using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.BGCTaskRunner.App.Services;

namespace Synologen.Service.Client.BGCTaskRunner.App.Factories
{
	public static class SendFileWriterServiceFactory
	{
		public static BGSendFileWriterService Get(IFileIOService fileIoService, IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService)
		{
			var writeDate = DateTime.Now;
			return new BGSendFileWriterService(fileIoService, serviceCoordinatorSettingsService, writeDate);
		}
		
	}
}