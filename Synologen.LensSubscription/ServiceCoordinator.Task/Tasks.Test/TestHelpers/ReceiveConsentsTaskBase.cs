using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using ConsentInformationCode=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.ConsentInformationCode;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class ReceiveConsentsTaskBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceiveConsents.Task(
				MockedWebServiceClient.Object, 
				MockedSubscriptionRepository.Object,
				MockedSubscriptionErrorRepository.Object,
				LoggingService);
		}

		protected static ConsentInformationCode? GetSubscriptionErrorInformationCode(Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode? code)
		{
			switch (code)
			{
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.AnswerToNewAccountApplication:
					return ConsentInformationCode.AnswerToNewAccountApplication;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPayer:
					return ConsentInformationCode.InitiatedByPayer;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPayersBank:
					return ConsentInformationCode.InitiatedByPayersBank;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.InitiatedByPaymentRecipient:
					return ConsentInformationCode.InitiatedByPaymentRecipient;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode.PaymentRecieversBankGiroAccountClosed:
					return ConsentInformationCode.PaymentRecieversBankGiroAccountClosed;
			}
			throw new AssertionException("No Matching enum");
		}
	}
}