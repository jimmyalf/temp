using System;
using FakeItEasy;
using NUnit.Framework;
using Synologen.LensSubscription.BGService.Test.Factories;
using Synologen.LensSubscription.BGService.Test.TestHelpers;

namespace Synologen.LensSubscription.BGService.Test
{
	[TestFixture, Category("BGFtpServiceTest")]
	public class When_sending_file_with_ftp_service : BGFtpServiceTestBase
	{
		private string fileData;
		private string expectedFileName;
		private string expectedFileUri;
		private string ftpRootUrl;
		private string bgCustomerNumber;

		public When_sending_file_with_ftp_service()
		{
			Context = () =>
			{
				fileData = FtpServiceFactory.GenerateFileData();
				ftpRootUrl = "ftp://path/path/path";
				bgCustomerNumber = "123456";
				expectedFileName = String.Format("BFEP.IAGAG.{0}", bgCustomerNumber);
				expectedFileUri = String.Format("{0}/{1}", ftpRootUrl, expectedFileName);
				A.CallTo(() => BgServiceCoordinatorSettingsService.GetFtpUploadFolderUrl()).Returns(ftpRootUrl);
				A.CallTo(() => BgServiceCoordinatorSettingsService.GetPaymentRevieverCustomerNumber()).Returns(bgCustomerNumber);
			};
			Because = ftpService => ftpService.SendFile(fileData);
		}
		
		[Test]
		public void File_is_sent_with_expected_fileName()
		{
			A.CallTo(() => FtpIOService.SendFile(expectedFileUri, A<string>.Ignored)).MustHaveHappened();
		}

		[Test]
		public void File_is_sent_with_expected_fileData()
		{
			A.CallTo(() => FtpIOService.SendFile(A<string>.Ignored, fileData)).MustHaveHappened();
		}
	}
}