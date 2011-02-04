using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Tasks;

namespace Synologen.ServiceCoordinator.Test.TestHelpers
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
