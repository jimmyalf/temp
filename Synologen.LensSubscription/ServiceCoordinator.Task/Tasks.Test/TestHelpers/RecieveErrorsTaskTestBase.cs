using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class RecieveErrorsTaskTestBase : TaskTestBase 
	{
		protected override ITask GetTask()
		{
			return new ReceiveErrors.Task(
				LoggingService, 
				MockedWebServiceClient.Object,
				MockedSubscriptionErrorRepository.Object,
				MockedSubscriptionRepository.Object);
		}
	}
}