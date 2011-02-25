using System;
using NUnit.Framework;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;

namespace Synologen.LensSubscription.BGService.Test
{
	[TestFixture, Explicit("Test is for proof of concept only")]
	public class TestCustomFtpService
	{
		protected CustomFtpService ftpService;

		public TestCustomFtpService()
		{
			ftpService = new CustomFtpService("ftp.spinit.se");
			ftpService.OnCommandExecuted += (sender, eventArgs) => Console.WriteLine("> " +eventArgs.Command);
			ftpService.OnResponseReceived += (sender, eventArgs) => Console.WriteLine(eventArgs.Response);
		}

		[Test]
		public void Test_login()
		{
			ftpService.Execute("USER spinitupload");
			ftpService.Execute("PASS bradag");
			ftpService.Execute("HELP");
		}

		[TearDown]
		public void TearDown()
		{
			ftpService.Dispose();
		}
	}
}