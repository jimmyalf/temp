using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public class BGFtpServiceTestBase : BehaviorTestBase<BGFtpService>
	{
		protected IFtpIOService FtpIOService;
		protected IBGConfigurationSettingsService BGConfigurationSettingsService;

		protected override void SetUp()
		{
			FtpIOService = A.Fake<IFtpIOService>();
			BGConfigurationSettingsService = A.Fake<IBGConfigurationSettingsService>();
		}
		protected override BGFtpService GetTestModel()
		{
			return new BGFtpService(
				FtpIOService, 
				BGConfigurationSettingsService,
                BGFtpServiceType.Autogiro
				);
		}
	}
}