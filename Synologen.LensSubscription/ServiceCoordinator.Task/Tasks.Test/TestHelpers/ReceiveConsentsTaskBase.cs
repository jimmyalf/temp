using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using ConsentInformationCode = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.ConsentInformationCode;
using BGConsentInformationCode = Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class ReceiveConsentsTaskBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceiveConsents.Task(MockedWebServiceClient.Object, LoggingService, TaskRepositoryResolver);
		}

		protected static ConsentInformationCode? GetSubscriptionErrorInformationCode(Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentInformationCode? code)
		{
			switch (code)
			{
				case BGConsentInformationCode.AnswerToNewAccountApplication: return ConsentInformationCode.AnswerToNewAccountApplication;
				case BGConsentInformationCode.InitiatedByPayer: return ConsentInformationCode.InitiatedByPayer;
				case BGConsentInformationCode.InitiatedByPayersBank: return ConsentInformationCode.InitiatedByPayersBank;
				case BGConsentInformationCode.InitiatedByPaymentRecipient: return ConsentInformationCode.InitiatedByPaymentRecipient;
				case BGConsentInformationCode.PaymentRecieversBankGiroAccountClosed: return ConsentInformationCode.PaymentRecieversBankGiroAccountClosed;
			}
			throw new AssertionException("No Matching enum");
		}
	}
}