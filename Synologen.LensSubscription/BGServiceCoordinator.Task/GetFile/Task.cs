using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
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
        private readonly IFileReaderService FileReaderService;

        public Task(ILoggingService loggingService,IFileReaderService fileReaderService) : base("GetFile", loggingService, BGTaskSequenceOrder.FetchFiles)
        {
            FileReaderService = fileReaderService;
        }

        public override void Execute(ExecutingTaskContext context)
        {
            RunLoggedTask(() =>
            {

                var receivedFileRepository = context.Resolve<IReceivedFileRepository>();
                var fileNames =  FileReaderService.GetFileNames();

                if (fileNames.Count() == 0)
                {
                    LogDebug("No received files found");
                    return;
                }
                LogDebug("Found {0} files", fileNames.Count());

                fileNames.Each(name =>
                {
					try
					{
                        var file = FileReaderService.ReadFileFromDisk(name);
                        var fileSections = FileReaderService.GetSections(file);

                        LogDebug("Found {0} sections in file {1}", fileSections.Count(), name);
                        fileSections.Each(section =>
                        {
                            var receivedFileSection = ToReceivedFileSection(section);
                            receivedFileRepository.Save(receivedFileSection);
                        });
						try
						{
                            FileReaderService.MoveFile(name);
						}
						catch (Exception ex)
						{
						    LogError("Error when moving read file to backup folder", ex);
						}
                        LogDebug("Saved {0} sections from file {1}", fileSections.Count(), name);
					}
					catch (AutogiroFileSplitException ex)
					{						
					    LogError(string.Format("Exception when parsing and splitting file {0}", name), ex); 
					}
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