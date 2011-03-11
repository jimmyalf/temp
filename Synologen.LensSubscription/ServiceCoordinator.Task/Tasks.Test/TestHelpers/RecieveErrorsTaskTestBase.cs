using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class RecieveErrorsTaskTestBase : CommonTaskTestBase 
	{
		protected override ITask GetTask()
		{
			return new ReceiveErrors.Task(LoggingService, MockedWebServiceClient.Object);
		}
	}
}