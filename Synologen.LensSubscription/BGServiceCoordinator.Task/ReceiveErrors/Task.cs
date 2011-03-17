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
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveErrors
{
    public class Task : TaskBase
    {
        private readonly IAutogiroFileReader<ErrorsFile, Error> _fileReader;

    	public Task(ILoggingService loggingService, IAutogiroFileReader<ErrorsFile, Error> fileReader)
            : base("ReceiveErrors", loggingService,  BGTaskSequenceOrder.ReadTask)
        {
            _fileReader = fileReader;
        	
        }

        public override void Execute(ExecutingTaskContext context)
    	{
    		RunLoggedTask(() =>
            {
				var receivedFileSectionRepository = context.Resolve<IReceivedFileRepository>();
				var bgReceivedErrorRepository = context.Resolve<IBGReceivedErrorRepository>();
				var autogiroPayerRepository = context.Resolve<IAutogiroPayerRepository>();
                var errorFileSections = receivedFileSectionRepository.FindBy(new AllUnhandledReceivedErrorFileSectionsCriteria());

                LogDebug("Fetched {0} error file sections from repository", errorFileSections.Count());

                errorFileSections.Each(errorFileSection =>
                {
                    var file = _fileReader.Read(errorFileSection.SectionData);
                    var errors = file.Posts;

                    errors.Each(error =>
                    {
                    	var payerId = error.Transmitter.CustomerNumber.ToInt();
                    	var payer = autogiroPayerRepository.Get(payerId);
                        var bgError = ToBgError(error, payer);
                        bgReceivedErrorRepository.Save(bgError);
                    });
                    errorFileSection.HandledDate = DateTime.Now;
                    receivedFileSectionRepository.Save(errorFileSection);
                    LogDebug("Saved {0} errors to repository", errors.Count());
                });
            });
    	}

        private static BGReceivedError ToBgError(Error error, AutogiroPayer payer)
        {
            return new BGReceivedError
            {
                Amount = error.Amount,
                CommentCode = error.CommentCode,
                Payer = payer,
                PaymentDate = error.PaymentDate,
                Reference = error.Reference,
                CreatedDate = DateTime.Now
            };
        }
    }
}
