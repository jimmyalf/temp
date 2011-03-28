using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class RecieveErrorsTaskTestBase : CommonTaskTestBase 
	{
		protected override ITask GetTask()
		{
			return new ReceiveErrors.Task(LoggingService, MockedWebServiceClient.Object);
		}

		protected static bool ExpectedErrorTypeConversionMatches(SubscriptionErrorType subscriptionErrorType, ErrorCommentCode errorType)
		{
			if(errorType ==  ErrorCommentCode.ConsentMissing)
			{
				return subscriptionErrorType == SubscriptionErrorType.ConsentMissing;
			}
			if(errorType ==  ErrorCommentCode.AccountNotYetApproved)
			{
				return subscriptionErrorType == SubscriptionErrorType.NotApproved;
			}
			if(errorType ==  ErrorCommentCode.ConsentStopped)
			{
				return subscriptionErrorType == SubscriptionErrorType.CosentStopped;
			}
			if(errorType ==  ErrorCommentCode.NotYetDebitable)
			{
				return subscriptionErrorType == SubscriptionErrorType.NotDebitable;
			}
			throw new AssertionException("No Matching enum");
		}
	}
}