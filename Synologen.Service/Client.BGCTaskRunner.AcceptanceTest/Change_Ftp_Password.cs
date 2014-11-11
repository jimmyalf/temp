using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.Task.ChangeRemoteFTPPassword;
using Synologen.Service.Client.BGCTaskRunner.AcceptanceTest.TestHelpers;

namespace Synologen.Service.Client.BGCTaskRunner.AcceptanceTest
{

	/* Feature: Change FTP password at BGC

	Scenario: It's time to change password (certain day in month)
		Verify that active password has changed after task has been run
	 */

	[TestFixture, Category("Feature: Change FTP password at BGC")]
	public class When_changing_ftp_password : TaskTestBase
	{
		private ITaskRunnerService taskRunnerService;
		private Task task;
		private string currentPassword;

		public When_changing_ftp_password()
		{
			Context = () =>
			{
				var newPassword = bGFtpPasswordService.GenerateNewPassword();
				bGFtpPasswordService.StoreNewActivePassword(newPassword);
				currentPassword = bGFtpPasswordService.GetCurrentPassword();
				task = ResolveTask<Task>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
			};
		}

		[Test]
		public void Current_password_has_changed()
		{
			var updatedPassword = bGFtpPasswordService.GetCurrentPassword();
			updatedPassword.ShouldNotBe(currentPassword);
		}
	}
}