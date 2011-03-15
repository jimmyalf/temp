using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendFile
{
	public class Task : TaskBase
	{
		private readonly ITamperProtectedFileWriter _tamperProtectedFileWriter;
		private readonly IFileWriterService _fileWriterService;

		public Task(ILoggingService loggingService, ITamperProtectedFileWriter tamperProtectedFileWriter, IFileWriterService fileWriterService) 
			: base("SendFile", loggingService, BGTaskSequenceOrder.SendFiles)
		{
			_tamperProtectedFileWriter = tamperProtectedFileWriter;
			_fileWriterService = fileWriterService;
		}

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var fileSectionToSendRepository = context.Resolve<IFileSectionToSendRepository>();
				var ftpService = context.Resolve<IFtpService>();
				var fileSectionsToSend = fileSectionToSendRepository.FindBy(new AllUnhandledFileSectionsToSendCriteria());
				if(fileSectionsToSend == null || fileSectionsToSend.Count() == 0)
				{
					LogInfo("Found no new file sections to send.");
					return;
				}
				LogDebug("Found {0} new file sections to send",fileSectionsToSend.Count());
				var fileData = ConcatenateFileSections(fileSectionsToSend);
				var sealedFileData  = _tamperProtectedFileWriter.Write(fileData);
				var ftpSendResult = ftpService.SendFile(sealedFileData);
				LogInfo("Sent file to remote FTP successfully");
				UpdateFileSectionsAsSent(fileSectionsToSend, fileSectionToSendRepository);
				try
				{
					_fileWriterService.WriteFileToDisk(sealedFileData, ftpSendResult.FileName);
				}
				catch(Exception ex)
				{
					LogError("Caught exception while trying to save sent file to disk (Non fatal error).", ex);
				}
			});
		}

		private static string ConcatenateFileSections(IEnumerable<FileSectionToSend> fileSections)
		{
			if (fileSections == null || fileSections.Count() == 0) return null;
			if(fileSections.Count() == 1) return fileSections.First().SectionData;
			return fileSections
					.Select(x => x.SectionData)
					.Aggregate((item, next) => string.Format("{0}\r\n{1}", item, next));
		}

		private static void UpdateFileSectionsAsSent(IEnumerable<FileSectionToSend> fileSections, IFileSectionToSendRepository fileSectionToSendRepository)
		{
			foreach (var fileSection in fileSections)
			{
				fileSection.SentDate = DateTime.Now;
				fileSectionToSendRepository.Save(fileSection);
			}
		}
	}
}