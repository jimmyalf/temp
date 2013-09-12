using System;
using EnterpriseDT.Net.Ftp;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.Service.Client.BGCTaskRunner.Test.TestHelpers;

namespace Synologen.Service.Client.BGCTaskRunner.Test
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
	        	FTPClient.RemoteHost = "remote.host";
	            A.CallTo(() => FTPClient.Password("ABC/DEF/DEF")).Invokes(call =>
	            {
	            	FTPClient.ReplyReceived += Raise.With(new FTPMessageEventArgs("230-Password was changed.")).Now;
	            });
	            A.CallTo(() => BGServiceCoordinatorSettingsService.GetFtpUserName()).Returns(userName);
	        };
	        Because = service =>
	        {
	            caughtException = TryGetException(() => service.Execute(oldPassword, newPassword));
	        };
	    }

	    [Test]
	    public void Service_fetches_ftp_username_setting()
	    {
	        A.CallTo(() => BGServiceCoordinatorSettingsService.GetFtpUserName()).MustHaveHappened();
	    }

	    [Test]
	    public void Service_connects()
	    {
	        A.CallTo(() => FTPClient.Connect()).MustHaveHappened();
	    }

	    [Test]
	    public void Service_loggs_in_with_username()
	    {
	        A.CallTo(() => FTPClient.User("user")).MustHaveHappened();
	    }

	    [Test]
	    public void Service_runs_change_password_command()
	    {
	        A.CallTo(() => FTPClient.Password("ABC/DEF/DEF")).MustHaveHappened();
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