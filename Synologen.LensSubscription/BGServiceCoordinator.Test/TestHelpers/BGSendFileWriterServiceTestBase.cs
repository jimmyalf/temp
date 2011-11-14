using System;
using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public abstract class BGSendFileWriterServiceTestBase : BehaviorActionTestbase<BGSendFileWriterService>
	{
		protected IFileIOService FileIOService;
		protected IBGServiceCoordinatorSettingsService BgServiceCoordinatorSettingsService;
		protected DateTime WriteDate;

		protected BGSendFileWriterServiceTestBase()
		{
			FileIOService = A.Fake<IFileIOService>();
			BgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
		}
		
		protected override BGSendFileWriterService GetTestEntity()
		{
			return new BGSendFileWriterService(FileIOService, BgServiceCoordinatorSettingsService, WriteDate);
		}
	}
}