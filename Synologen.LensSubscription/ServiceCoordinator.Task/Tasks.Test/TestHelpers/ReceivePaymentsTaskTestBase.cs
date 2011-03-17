using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class ReceivePaymentsTaskTestBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceivePayments.Task(MockedWebServiceClient.Object,LoggingService);
		}


		protected static bool MatchTransactionType(TransactionType type, PaymentType paymentType) 
		{
			switch (paymentType)
			{
				case PaymentType.Debit: return type == TransactionType.Deposit;
				case PaymentType.Credit: return type == TransactionType.Withdrawal;
				default: throw new AssertionException("Could not match transaction type");
			}
		}

		protected static bool MatchReason(TransactionReason reason, PaymentResult result) 
		{
			switch (result)
			{
				case PaymentResult.Approved: return reason == TransactionReason.Payment;
				case PaymentResult.WillTryAgain: return reason == TransactionReason.PaymentFailed;
				default: throw new AssertionException("Could not match reason type");
			}
		}

		protected static bool MatchErrorType(SubscriptionErrorType type, PaymentResult result) 
		{
			switch (result)
			{
				case PaymentResult.InsufficientFunds: return type == SubscriptionErrorType.PaymentRejectedInsufficientFunds;
				case PaymentResult.AGConnectionMissing: return type ==  SubscriptionErrorType.PaymentRejectedAgConnectionMissing;
				default: throw new AssertionException("Could not match error type");
			}
		}
	}
}