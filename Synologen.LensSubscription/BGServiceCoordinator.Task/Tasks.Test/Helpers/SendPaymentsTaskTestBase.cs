using System.Collections.Generic;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class SendPaymentsTaskTestBase : CommonTaskTestBase
	{
		protected IAutogiroFileWriter<PaymentsFile, Payment> PaymentFileWriter;
		protected IBGPaymentToSendRepository BGPaymentToSendRepository;

		protected override void SetUp()
		{
			base.SetUp();

			PaymentFileWriter = A.Fake<IAutogiroFileWriter<PaymentsFile, Payment>>();
			BGPaymentToSendRepository = A.Fake<IBGPaymentToSendRepository>();
			A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGPaymentToSendRepository>()).Returns(BGPaymentToSendRepository);
		}


		protected override ITask GetTask()
		{
			return new SendPayments.Task(
				Log4NetLogger,
				//BGPaymentToSendRepository,
				BgConfigurationSettingsService,
				PaymentFileWriter,
				//FileSectionToSendRepository,
				TaskRepositoryResolver);
		}

		protected static bool MatchPayments(IEnumerable<Payment> parsedPayments, IList<BGPaymentToSend> originalPayments, string recieverBankGiroNumber)
		{
			var allMatches = true;
			parsedPayments.ForBoth(originalPayments, (parsedPayment,originalPayment) =>
			{
				if(!PaymentIsMatch(parsedPayment, originalPayment, recieverBankGiroNumber))
				{
					allMatches =  false;
				}
			});
			return allMatches;
		}

		protected static bool PaymentIsMatch(Payment parsedPayment, BGPaymentToSend originalPayment, string bankGiroNumber) 
		{
			return Equals(parsedPayment.Amount, originalPayment.Amount) &&
			       Equals(parsedPayment.PaymentDate, originalPayment.PaymentDate) &&
			       Equals(parsedPayment.PeriodCode.ToInteger(), originalPayment.PeriodCode.ToInteger()) &&
			       Equals(parsedPayment.RecieverBankgiroNumber, bankGiroNumber) &&
			       Equals(parsedPayment.Reference, originalPayment.Reference) &&
			       Equals(parsedPayment.Transmitter.CustomerNumber, originalPayment.Payer.Id.ToString()) &&
			       Equals(parsedPayment.Type.ToInteger(), originalPayment.Type.ToInteger());
		}
	}
}