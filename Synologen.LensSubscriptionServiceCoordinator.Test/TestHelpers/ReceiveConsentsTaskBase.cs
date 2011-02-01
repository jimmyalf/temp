using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Tasks;

namespace Synologen.ServiceCoordinator.Test.TestHelpers
{
	public abstract class ReceiveConsentsTaskBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceiveConsentsTask(MockedWebServiceClient.Object, 
										MockedSubscriptionRepository.Object,
			                            MockedSubscriptionErrorRepository.Object,
										LoggingService);
		}
	}
}
