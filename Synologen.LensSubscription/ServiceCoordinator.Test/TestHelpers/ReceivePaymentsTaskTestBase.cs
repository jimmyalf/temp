using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.Tasks;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
{
	public abstract class ReceivePaymentsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceivePaymentsTask(MockedWebServiceClient.Object,
			                               MockedTransactionRepository.Object,
			                               MockedSubscriptionErrorRepository.Object,
			                               MockedSubscriptionRepository.Object,
			                               LoggingService);
		}
	}
}