using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.BGCTaskRunner.App.Services;

namespace Synologen.Service.Client.BGCTaskRunner.Test.TestHelpers
{
	public class BGFtpServiceTestBase : BehaviorActionTestbase<BGFtpService>
	{
		protected IFtpIOService FtpIOService;
		protected IBGServiceCoordinatorSettingsService BgServiceCoordinatorSettingsService;

		protected override void SetUp()
		{
			FtpIOService = A.Fake<IFtpIOService>();
			BgServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
		}

		protected override BGFtpService GetTestEntity()
		{
			return new BGFtpService(
				FtpIOService, 
				BgServiceCoordinatorSettingsService,
                BGFtpServiceType.Autogiro
				);
		}
	}
}