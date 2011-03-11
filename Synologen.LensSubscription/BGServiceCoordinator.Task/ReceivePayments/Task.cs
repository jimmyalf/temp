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
using PaymentResult=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ReceivePayments
{
    public class Task : TaskBase
    {
        private readonly IAutogiroFileReader<PaymentsFile, Payment> _fileReader;

    	public Task(ILoggingService loggingService,IAutogiroFileReader<PaymentsFile, Payment> fileReader)
            : base("ReceivePayments", loggingService, BGTaskSequenceOrder.ReadTask)
        {
            _fileReader = fileReader;
        }

        public override void Execute(ExecutingTaskContext context)
        {
            RunLoggedTask(() =>
            {
				var receivedFileSectionRepository = context.Resolve<IReceivedFileRepository>();
				var bgReceivedPaymentRepository = context.Resolve<IBGReceivedPaymentRepository>();
				var autogiroPayerRepository = context.Resolve<IAutogiroPayerRepository>();
                var paymentFileSections = receivedFileSectionRepository.FindBy(new AllUnhandledReceivedPaymentFileSectionsCriteria());

                LogDebug("Fetched {0} payment file sections from repository", paymentFileSections.Count());

                paymentFileSections.Each(paymentFileSection =>
                {
                    var file = _fileReader.Read(paymentFileSection.SectionData);
                    var payments = file.Posts;

                    payments.Each(payment =>
                    {
                    	var payerId = payment.Transmitter.CustomerNumber.ToInt();
                    	var payer = autogiroPayerRepository.Get(payerId);
                        var receivedConsent = ToBGPayment(payment, payer);
                        bgReceivedPaymentRepository.Save(receivedConsent);
                    });
                    paymentFileSection.HandledDate = DateTime.Now;
                    receivedFileSectionRepository.Save(paymentFileSection);
                    LogDebug("Saved {0} payments to repository", payments.Count());
                });
            });                   
        }

        private static BGReceivedPayment ToBGPayment(Payment payment, AutogiroPayer payer)
        {
            return new BGReceivedPayment
            {
                Amount = payment.Amount,
				Payer = payer,
                PaymentDate = payment.PaymentDate,
                Reference = payment.Reference,
                ResultType = MapPaymentResult(payment.Result),
                CreatedDate = DateTime.Now
            };
        }

    	private static PaymentResult MapPaymentResult(Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentResult result) 
		{
    		switch (result)
    		{
    			case Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentResult.Approved: return PaymentResult.Approved;
    			case Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentResult.InsufficientFunds: return PaymentResult.InsufficientFunds;
    			case Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentResult.AGConnectionMissing: return PaymentResult.AGConnectionMissing;
    			case Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentResult.WillTryAgain: return PaymentResult.WillTryAgain;
    			default: throw new ArgumentOutOfRangeException("result");
    		}
		}
    }
}