using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
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
		private readonly IFileSectionToSendRepository _fileSectionToSendRepository;
		private readonly ITamperProtectedFileWriter _tamperProtectedFileWriter;
		private readonly IFtpService _ftpService;
		private readonly IFileWriterService _fileWriterService;

		public Task(ILoggingService loggingService, IFileSectionToSendRepository fileSectionToSendRepository, ITamperProtectedFileWriter tamperProtectedFileWriter, IFtpService ftpService, IFileWriterService fileWriterService) : base("SendFile", loggingService, BGTaskSequenceOrder.SendFiles)
		{
			_fileSectionToSendRepository = fileSectionToSendRepository;
			_tamperProtectedFileWriter = tamperProtectedFileWriter;
			_ftpService = ftpService;
			_fileWriterService = fileWriterService;
		}

		public override void Execute()
		{
			RunLoggedTask(() =>
			{
				var fileSectionsToSend = _fileSectionToSendRepository.FindBy(new AllUnhandledFileSectionsToSendCriteria());
				if(fileSectionsToSend == null || fileSectionsToSend.Count() == 0)
				{
					LogInfo("Found no new file sections to send.");
					return;
				}
				var fileData = ConcatenateFileSections(fileSectionsToSend);
				var sealedFileData  = _tamperProtectedFileWriter.Write(fileData);
				var ftpSendResult = _ftpService.SendFile(sealedFileData);
				UpdateFileSectionsAsSent(fileSectionsToSend, _fileSectionToSendRepository);
				_fileWriterService.WriteFileToDisk(sealedFileData, ftpSendResult.FileName);
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
			fileSections.Each(fileSection =>
			{
				fileSection.SentDate = DateTime.Now;
				fileSectionToSendRepository.Save(fileSection);
			});
		}
	}
}