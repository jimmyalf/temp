using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.Tasks;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
{
	public abstract class RecieveErrorsTaskTestBase : TaskTestBase 
	{
		protected override ITask GetTask()
		{
			return new ReceiveErrorsTask(
				LoggingService, 
				MockedWebServiceClient.Object,
				MockedSubscriptionErrorRepository.Object,
				MockedSubscriptionRepository.Object
				);
		}
	}
}