using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using ChangeRemoteFTPPassword = Synologen.LensSubscription.BGServiceCoordinator.Task.ChangeRemoteFTPPassword.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{

	/*
		Feature: Byt FTP-lösenord hos BGC

			Scenario: Det är dags att byta lösenord (viss dag i månaden)
				Verifiera att ett nytt aktivt lösenord finns på plats efter task körts
	 */
	[TestFixture, Category("Feature: When_changing_FTP_Password")]
	public class When_changing_ftp_password : TaskTestBase
	{
		private ITaskRunnerService taskRunnerService;
		private ChangeRemoteFTPPassword task;
		private string currentPassword;

		public When_changing_ftp_password()
		{
			Context = () =>
			{
				var newPassword = bGFtpPasswordService.GenerateNewPassword();
				bGFtpPasswordService.StoreNewActivePassword(newPassword);
				currentPassword = bGFtpPasswordService.GetCurrentPassword();
				task = ResolveTask<ChangeRemoteFTPPassword>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Current_password_has_been_changed()
		{
			var updatedPassword = bGFtpPasswordService.GetCurrentPassword();
			updatedPassword.ShouldNotBe(currentPassword);
		}
	}
}