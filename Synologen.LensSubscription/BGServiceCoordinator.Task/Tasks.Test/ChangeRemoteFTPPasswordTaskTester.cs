using FakeItEasy;
using NUnit.Framework;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture, Category("ChangeRemoteFTPPasswordTaskTester")]
	public class When_changing_remote_ftp_password : ChangeRemoteFTPPasswordTaskTestBase
	{
		private string currentPassword;
		private string generatedPassword;

		public When_changing_remote_ftp_password()
		{
			Context = () =>
			{
				currentPassword = "ABC";
				generatedPassword = "ÅÄÖ";
				A.CallTo(() => BGFtpPasswordService.GetCurrentPassword()).Returns(currentPassword);
				A.CallTo(() => BGFtpPasswordService.GenerateNewPassword()).Returns(generatedPassword);
			};
			Because = task => task.Execute(ExecutingTaskContext);
		}

		[Test]
		public void Task_fetches_current_active_password()
		{
			A.CallTo(() => BGFtpPasswordService.GetCurrentPassword()).MustHaveHappened();
		}

		[Test]
		public void Task_generates_a_new_password()
		{
			A.CallTo(() => BGFtpPasswordService.GenerateNewPassword()).MustHaveHappened();
		}

		[Test]
		public void Task_updates_new_password_at_remote_ftp_service()
		{
			A.CallTo(() => BGFtpChangePasswordService.Execute(currentPassword, generatedPassword)).MustHaveHappened();
		}

		[Test]
		public void Task_stores_new_password_as_current_active_password()
		{
			A.CallTo(() => BGFtpPasswordService.StoreNewActivePassword(generatedPassword)).MustHaveHappened();
		}
	}
}