using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public abstract class BGSentFileWriterServiceTestBase : BehaviorTestBase<BGSentFileWriterService>
	{
		protected IFileIOService FileIOService;
		protected IBGConfigurationSettingsService BGConfigurationSettingsService;

		protected BGSentFileWriterServiceTestBase()
		{
			FileIOService = A.Fake<IFileIOService>();
			BGConfigurationSettingsService = A.Fake<IBGConfigurationSettingsService>();
		}
		
		protected override BGSentFileWriterService GetTestModel()
		{
			return new BGSentFileWriterService(FileIOService, BGConfigurationSettingsService);
		}
	}
}