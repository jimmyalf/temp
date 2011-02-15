using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class SendPaymentsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new SendPayments.Task(
				MockedWebServiceClient.Object,
				MockedSubscriptionRepository.Object,
				LoggingService);
		}
	}
}