using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public abstract class BGSentFileWriterServiceTestBase : BehaviorTestBase<BGSendFileWriterService>
	{
		protected IFileIOService FileIOService;
		protected IBGConfigurationSettingsService BGConfigurationSettingsService;
		protected DateTime WriteDate;

		protected BGSentFileWriterServiceTestBase()
		{
			FileIOService = A.Fake<IFileIOService>();
			BGConfigurationSettingsService = A.Fake<IBGConfigurationSettingsService>();
		}
		
		protected override BGSendFileWriterService GetTestModel()
		{
			return new BGSendFileWriterService(FileIOService, BGConfigurationSettingsService, WriteDate);
		}
	}
}