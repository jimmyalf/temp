using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class ReceiveConsentsTaskBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceiveConsents.Task(MockedWebServiceClient.Object, LoggingService);
		}
	}
}