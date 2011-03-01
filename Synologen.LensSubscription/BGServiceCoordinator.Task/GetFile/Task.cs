using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.GetFile
{
    public class Task : TaskBase
    {
        private readonly ITamperProtectedFileReader _tamperProtectedFileReader;
        private readonly IFileReaderService _fileReaderService;
        private readonly IReceivedFileRepository _receivedFileRepository;

        public Task(ILoggingService loggingService,
                    ITamperProtectedFileReader tamperProtectedFileReader,
                    IFileReaderService fileReaderService,
                    IReceivedFileRepository receivedFileRepository)
            : base("GetFile", loggingService, BGTaskSequenceOrder.FetchFiles)
        {
            
            _tamperProtectedFileReader = tamperProtectedFileReader;
            _fileReaderService = fileReaderService;
            _receivedFileRepository = receivedFileRepository;
            
            // Hämta en fil med IFileReaderService

            // Använd ITamperProtectedFileReader för att ta bort förändringsskyddet

            // Använd Autogiro-funktion som ger tillbaka en lista på sections

            // Loopa igenom listan och spara till repository
        }

        public override void Execute()
        {
            RunLoggedTask(() =>
            { 
                string file = _fileReaderService.ReadFileFromDisk();

                if (String.IsNullOrEmpty(file))
                {
                    LogDebug("No files found");
                    return;
                }

                _tamperProtectedFileReader.Read(file); // Kastar exception om inte skyddet är ok?

                IEnumerable<FileSection> fileSections = Autogiro.Readers.ReceivedFileSplitter.GetSections(file);

                LogDebug("Found {0} sections in file", fileSections.Count());
                fileSections.Each(section =>
                {
                    ReceivedFileSection receivedFileSection = ToReceivedFileSection(section);
                    _receivedFileRepository.Save(receivedFileSection);
                });
            });
        }

        private static ReceivedFileSection ToReceivedFileSection(FileSection section)
        {
            return new ReceivedFileSection
            {
                SectionData = section.Posts,
                CreatedDate = DateTime.Now,
                Type = section.SectionType,
                TypeName = section.SectionType.GetEnumDisplayName()
            };
        }
    }
}