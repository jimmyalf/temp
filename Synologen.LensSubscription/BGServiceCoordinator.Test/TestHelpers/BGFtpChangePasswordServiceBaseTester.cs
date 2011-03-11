using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public class BGFtpChangePasswordServiceBaseTester : BehaviorActionTestbase<BGFtpChangePasswordService>
	{
		protected ILoggingService LoggingService;
		protected IFtpCommandService FtpCommandService;
		protected IBGServiceCoordinatorSettingsService BGServiceCoordinatorSettingsService;

		protected override void SetUp()
		{
			FtpCommandService = A.Fake<IFtpCommandService>();
			LoggingService = A.Fake<ILoggingService>();
			BGServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();

		}
		protected override BGFtpChangePasswordService GetTestEntity() 
		{ 
			return new BGFtpChangePasswordService(FtpCommandService, BGServiceCoordinatorSettingsService, LoggingService);
		}
	}
}