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

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveConsents
{
    public class Task : TaskBase
    {
        private readonly IAutogiroFileReader<ConsentsFile, Consent> _fileReader;

    	public Task(ILoggingService loggingService, IAutogiroFileReader<ConsentsFile, Consent> fileReader)

            : base("ReceiveConsents", loggingService , BGTaskSequenceOrder.ReadTask)
        {

            _fileReader = fileReader;
        }


        public override void Execute(ExecutingTaskContext context)
        {
            RunLoggedTask(() =>
            {
				var receivedFileSectionRepository = context.GetRepository<IReceivedFileRepository>();
				var bgReceivedConsentRepository = context.GetRepository<IBGReceivedConsentRepository>();
				var autogiroPayerRepository = context.GetRepository<IAutogiroPayerRepository>();
                var consentFileSections = receivedFileSectionRepository.FindBy(new AllUnhandledReceivedConsentFileSectionsCriteria());

                LogDebug("Fetched {0} consent file sections from repository", consentFileSections.Count());

                consentFileSections.Each(consentFileSection =>
                {
                    var file = _fileReader.Read(consentFileSection.SectionData);
                    var consents = file.Posts;

                    consents.Each(consent =>
                    {
                    	var payerId = consent.Transmitter.CustomerNumber.ToInt();
                    	var payer = autogiroPayerRepository.Get(payerId);
                        var receivedConsent = ToBGConsent(consent, payer);
                        bgReceivedConsentRepository.Save(receivedConsent);
                    });
                    consentFileSection.HandledDate = DateTime.Now;
                    receivedFileSectionRepository.Save(consentFileSection);
                    LogDebug("Saved {0} consents to repository", consents.Count());
                });
            });
        }

        private static BGReceivedConsent ToBGConsent(Consent consent, AutogiroPayer payer)
        {
            return new BGReceivedConsent
            {
                  ActionDate = consent.ActionDate,
                  CommentCode = consent.CommentCode,
                  ConsentValidForDate = consent.ConsentValidForDate,
                  InformationCode = consent.InformationCode,
                  //PayerNumber = int.Parse(consent.Transmitter.CustomerNumber),
				  Payer = payer,
                  CreatedDate = DateTime.Now
            };
        }
    }
}
