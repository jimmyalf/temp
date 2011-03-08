using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public abstract class BGSendFileWriterServiceTestBase : BehaviorTestBase<BGSendFileWriterService>
	{
		protected IFileIOService FileIOService;
		protected IBGServiceCoordinatorSettingsService BgServiceCoordinatorSettingsService;
		protected DateTime WriteDate;

		protected BGSendFileWriterServiceTestBase()
		{
			FileIOService = A.Fake<IFileIOService>();
			BgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
		}
		
		protected override BGSendFileWriterService GetTestModel()
		{
			return new BGSendFileWriterService(FileIOService, BgServiceCoordinatorSettingsService, WriteDate);
		}
	}
}