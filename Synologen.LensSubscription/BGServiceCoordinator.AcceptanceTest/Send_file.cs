using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers;
using SendFileTask = Synologen.LensSubscription.BGServiceCoordinator.Task.SendFile.Task;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest
{
	/*Feature: Skicka fil till BGC

	Scenario: Nya filsektioner finns sparade i DB
		Verifiera att ny fil skickas över FTP
		Verifiera att en kopia av filen sparas på disk
		Verifiera att filsektioner uppdateras som hanterad/lästa
	 */

	[TestFixture, Category("Feature: Send new file to BGC")]
	public class When_sending_file : TaskTestBase
	{
		private SendFileTask task;
		private ITaskRunnerService taskRunnerService;
		private FileSectionToSend consentFileSectionToSend;
		private FileSectionToSend paymentFileSectionToSend;
		private string[] remoteFtpFiles, localFiles;
		private Encoding ftpEncoding;

		public When_sending_file()
		{
			Context = () =>
			{
				ClearFoldersOnDisk();
				ftpEncoding = Encoding.GetEncoding(858);
				var ftpPassword = ConfigurationManager.AppSettings["FtpPassword"];
				ResolveRepository<IBGFtpPasswordService>().StoreNewActivePassword(ftpPassword);
				consentFileSectionToSend = Factory.GetConsentFileSectionToSend();
				paymentFileSectionToSend = Factory.GetPaymentFileSectionToSend();
				fileSectionToSendRepository.Save(consentFileSectionToSend);
				fileSectionToSendRepository.Save(paymentFileSectionToSend);

				task = ResolveTask<SendFileTask>();
				taskRunnerService = GetTaskRunnerService(task);
			};
			Because = () =>
			{
				taskRunnerService.Run();
				remoteFtpFiles = System.IO.Directory.GetFiles(remoteFtpFolder);
				localFiles = System.IO.Directory.GetFiles(bgServiceCoordinatorSettingsService.GetSentFilesFolderPath());
			};
		}


		[Test]
		public void Task_sends_file_to_remote_ftp()
		{
			remoteFtpFiles.Length.ShouldBe(1);
			Regex.Match(remoteFtpFiles[0], @"BFEP\.IAG(AG|ZZ)\.\d{0,6}$").Success.ShouldBe(true);
		}

		[Test]
		public void Sent_file_contains_expected_file_data()
		{
			var fileData = System.IO.File.ReadAllText(remoteFtpFiles[0], ftpEncoding);
			fileData.ShouldContain(consentFileSectionToSend.SectionData);
			fileData.ShouldContain(paymentFileSectionToSend.SectionData);
		}

		[Test]
		public void Task_stores_a_local_copy_of_sent_file()
		{
			localFiles.Length.ShouldBe(1);
			Regex.Match(localFiles[0], @"BFEP\.IAG(AG|ZZ)\.\d{0,6}\.D\d{6}\.T\d{6}$").Success.ShouldBe(true);
		}

		[Test]
		public void Task_updates_file_sections_as_sent()
		{
			var fetchedConsentFileSection = ResolveRepository<IFileSectionToSendRepository>().Get(consentFileSectionToSend.Id);
			var fetchedPaymentFileSection = ResolveRepository<IFileSectionToSendRepository>().Get(paymentFileSectionToSend.Id);
			fetchedConsentFileSection.HasBeenSent.ShouldBe(true);
			fetchedConsentFileSection.SentDate.Value.Date.ShouldBe(DateTime.Now.Date);
			fetchedPaymentFileSection.HasBeenSent.ShouldBe(true);
			fetchedPaymentFileSection.SentDate.Value.Date.ShouldBe(DateTime.Now.Date);
		}
	}
}