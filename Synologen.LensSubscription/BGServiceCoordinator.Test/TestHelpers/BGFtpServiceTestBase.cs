using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public class BGFtpServiceTestBase : BehaviorTestBase<BGFtpService>
	{
		protected IFtpIOService FtpIOService;
		protected IBGServiceCoordinatorSettingsService BgServiceCoordinatorSettingsService;

		protected override void SetUp()
		{
			FtpIOService = A.Fake<IFtpIOService>();
			BgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
		}
		protected override BGFtpService GetTestModel()
		{
			return new BGFtpService(
				FtpIOService, 
				BgServiceCoordinatorSettingsService,
                BGFtpServiceType.Autogiro
				);
		}
	}
}