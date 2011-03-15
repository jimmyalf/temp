using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGService.Test
{
	[TestFixture, Explicit("Test is for proof of concept only")]
	public class TestFtpCommandService
	{
		protected FtpCommandService _ftpCommandService;
		public TestFtpCommandService()
		{
			_ftpCommandService = new FtpCommandService();
			_ftpCommandService.OnCommandSent += (sender, eventArgs) => Console.WriteLine("> {0}", eventArgs.Command);
			_ftpCommandService.OnResponseReceived += (sender, eventArgs) => Console.WriteLine(eventArgs.Response);
		}

		[SetUp]
		public void Setup()
		{
			_ftpCommandService.Open("black");
		}

		[Test]
		public void Test_login()
		{
			_ftpCommandService.Execute(@"USER HOTEL\dev-ftp");
			_ftpCommandService.Execute("PASS zdUFQRq");
			_ftpCommandService.Execute("STAT");
			_ftpCommandService.ExecuteNoReply("QUIT");
		}

		[Test]
		public void Test_login2()
		{
			_ftpCommandService.Execute(@"USER HOTEL\dev-ftp");
			_ftpCommandService.Execute("PASS zdUFQRq");
			_ftpCommandService.Execute("STAT");
			_ftpCommandService.ExecuteNoReply("QUIT");
		}

		[TearDown]
		public void TearDown()
		{
			_ftpCommandService.Close();
		}
	}
}