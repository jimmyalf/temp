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

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ReceivePayments
{
    public class Task : TaskBase
    {

        private readonly IBGReceivedPaymentRepository _bgReceivedPaymentRepository;
        private readonly IReceivedFileRepository _receivedFileSectionRepository;
        private readonly IAutogiroFileReader<PaymentsFile, Payment> _fileReader;

        public Task(ILoggingService loggingService,
            IReceivedFileRepository receivedFileSectionRepository,
            IBGReceivedPaymentRepository bgReceivedPaymentRepository,
            IAutogiroFileReader<PaymentsFile, Payment> fileReader)

            : base("ReceivePayments", loggingService, BGTaskSequenceOrder.ReadTask)
        {
            _receivedFileSectionRepository = receivedFileSectionRepository;
            _bgReceivedPaymentRepository = bgReceivedPaymentRepository;
            _fileReader = fileReader;
        }

        public override void Execute()
        {
            RunLoggedTask(() =>
            {
                var paymentFileSections =
                    _receivedFileSectionRepository.FindBy(new AllUnhandledReceivedPaymentFileSectionsCriteria());

                LogDebug("Fetched {0} payment file sections from repository", paymentFileSections.Count());

                paymentFileSections.Each(paymentFileSection =>
                {
                    PaymentsFile file = _fileReader.Read(paymentFileSection.SectionData);
                    var payments = file.Posts;

                    payments.Each(payment =>
                    {
                        BGReceivedPayment receivedConsent = ToBGPayment(payment);
                        _bgReceivedPaymentRepository.Save(receivedConsent);
                    });
                    paymentFileSection.HandledDate = DateTime.Now;
                    _receivedFileSectionRepository.Save(paymentFileSection);
                    LogDebug("Saved {0} payments to repository", payments.Count());
                });
            });                   
        }

        private BGReceivedPayment ToBGPayment(Payment payment)
        {
            return new BGReceivedPayment
            {
                Amount = payment.Amount,
                PayerNumber = int.Parse(payment.Transmitter.CustomerNumber),
                PaymentDate = payment.PaymentDate,
                Reference = payment.Reference,
                Result = payment.Result,
                CreatedDate = DateTime.Now
            };
        }
    }
}