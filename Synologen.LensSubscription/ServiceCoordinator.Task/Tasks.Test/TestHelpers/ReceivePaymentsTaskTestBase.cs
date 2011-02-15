using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class ReceivePaymentsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new RecievePayments.Task(MockedWebServiceClient.Object,
			                               MockedTransactionRepository.Object,
			                               MockedSubscriptionErrorRepository.Object,
			                               MockedSubscriptionRepository.Object,
			                               LoggingService);
		}
	}
}