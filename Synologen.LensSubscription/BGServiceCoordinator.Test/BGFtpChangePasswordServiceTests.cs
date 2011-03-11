using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.LensSubscription.BGService.Test.TestHelpers;

namespace Synologen.LensSubscription.BGService.Test
{
	[TestFixture, Category("BGFtpChangePasswordServiceTests")]
	public class When_successfully_changing_password : BGFtpChangePasswordServiceBaseTester
	{
		private string oldPassword;
		private string newPassword;
		private string userName;
		private Exception caughtException;

		public When_successfully_changing_password()
		{
			Context = () =>
			{
				oldPassword = "ABC";
				newPassword = "DEF";
				userName = "user";
				A.CallTo(() => FtpCommandService.Execute("PASS ABC/DEF/DEF")).Returns("Positive test response");
				A.CallTo(() => BGServiceCoordinatorSettingsService.GetFtpUserName()).Returns(userName);
			};
			Because = service =>
			{
				caughtException = TryGetException(() => service.Execute(oldPassword, newPassword));
			};
		}

		[Test]
		public void Service_fetches_ftp_path_setting()
		{
			A.CallTo(() => BGServiceCoordinatorSettingsService.GetFtpUploadFolderUrl()).MustHaveHappened();
		}

		[Test]
		public void Service_fetches_ftp_username_setting()
		{
			A.CallTo(() => BGServiceCoordinatorSettingsService.GetFtpUserName()).MustHaveHappened();
		}

		[Test]
		public void Service_connects_with_username()
		{
			A.CallTo(() => FtpCommandService.Execute("USER user")).MustHaveHappened();
		}

		[Test]
		public void Service_runs_change_password_command()
		{
			A.CallTo(() => FtpCommandService.Execute("PASS ABC/DEF/DEF")).MustHaveHappened();
		}

		[Test]
		public void Service_exits_without_throwing_exception()
		{
			caughtException.ShouldBe(null);
		}
	}

	[TestFixture, Category("BGFtpChangePasswordServiceTests")]
	public class When_failing_to_change_password : BGFtpChangePasswordServiceBaseTester
	{
		private string oldPassword;
		private string newPassword;
		private Exception caughtException;

		public When_failing_to_change_password()
		{
			Context = () =>
			{
				oldPassword = "ABC";
				newPassword = "DEF";
				A.CallTo(() => FtpCommandService.Execute("PASS ABC/DEF/DEF")).Returns("PASS command failed");
			};
			Because = service =>
			{
				caughtException = TryGetException(() => service.Execute(oldPassword, newPassword));
			};
		}

		[Test]
		public void Service_returns_FtpChangePasswordException()
		{
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(FtpChangePasswordException));
		}
	}
}