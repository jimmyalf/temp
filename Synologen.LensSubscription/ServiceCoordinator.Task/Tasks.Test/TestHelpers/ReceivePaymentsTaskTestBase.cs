using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class ReceivePaymentsTaskTestBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceivePayments.Task(MockedWebServiceClient.Object,LoggingService, TaskRepositoryResolver);
		}
	}
}