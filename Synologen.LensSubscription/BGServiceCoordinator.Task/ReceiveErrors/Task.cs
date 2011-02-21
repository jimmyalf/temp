using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveErrors
{
    public class Task : TaskBase
    {
        private readonly IBGReceivedErrorRepository _bgReceivedErrorRepository;
        private readonly IReceivedFileRepository _receivedFileSectionRepository;
        private readonly IAutogiroFileReader<ErrorsFile, Error> _fileReader;

        public Task(ILoggingService loggingService,
                    IReceivedFileRepository receivedFileSectionRepository,
                    IBGReceivedErrorRepository bgReceivedErrorRepository,
                    IAutogiroFileReader<ErrorsFile, Error> fileReader)
            : base("ReceiveErrors", loggingService, BGTaskSequenceOrder.ReadTask)
        {
            _receivedFileSectionRepository = receivedFileSectionRepository;
            _bgReceivedErrorRepository = bgReceivedErrorRepository;
            _fileReader = fileReader;
        }

        public override void Execute()
    	{
    		base.RunLoggedTask(() =>
            {
                var errorFileSections =
                    _receivedFileSectionRepository.FindBy(new AllUnhandledReceivedErrorFileSectionsCriteria());

                LogDebug("Fetched {0} error file sections from repository", errorFileSections.Count());

                errorFileSections.Each(errorFileSection =>
                {
                    ErrorsFile file = _fileReader.Read(errorFileSection.SectionData);
                    var errors = file.Posts;

                    errors.Each(error =>
                    {
                        BGReceivedError bgError = ToBgError(error);
                        _bgReceivedErrorRepository.Save(bgError);
                    });
                    errorFileSection.HandledDate = DateTime.Now;
                    _receivedFileSectionRepository.Save(errorFileSection);
                    LogDebug("Saved {0} errors to repository", errors.Count());
                });
            });
    	}

        private static BGReceivedError ToBgError(Error error)
        {
            return new BGReceivedError
            {
                Amount = error.Amount,
                CommentCode = error.CommentCode,
                PayerNumber = int.Parse(error.Transmitter.CustomerNumber),
                PaymentDate = error.PaymentDate,
                Reference = error.Reference,
                CreatedDate = DateTime.Now
            };
        }
    }
}
