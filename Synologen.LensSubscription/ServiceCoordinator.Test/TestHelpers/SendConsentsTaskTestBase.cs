using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.Tasks;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
{
	public abstract class SendConsentsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask() 
		{
			return new SendConsentsTask(
				MockedWebServiceClient.Object,
				MockedSubscriptionRepository.Object,
				LoggingService);
		}
	}
}