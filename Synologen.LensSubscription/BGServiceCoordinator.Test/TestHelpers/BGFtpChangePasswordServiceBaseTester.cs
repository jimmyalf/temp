using EnterpriseDT.Net.Ftp;
using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;

namespace Synologen.LensSubscription.BGService.Test.TestHelpers
{
	public class BGFtpChangePasswordServiceBaseTester : BehaviorActionTestbase<BGFtpChangePasswordService>
	{
		protected ILoggingService LoggingService;
		protected IBGServiceCoordinatorSettingsService BGServiceCoordinatorSettingsService;
		protected FTPClient FTPClient;

		protected override void SetUp()
		{
			LoggingService = A.Fake<ILoggingService>();
			BGServiceCoordinatorSettingsService = A.Fake<IBGServiceCoordinatorSettingsService>();
			FTPClient = A.Fake<FTPClient>();

		}
		protected override BGFtpChangePasswordService GetTestEntity() 
		{ 
			return new BGFtpChangePasswordService(BGServiceCoordinatorSettingsService, LoggingService, FTPClient);
		}
	}
}