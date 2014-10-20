using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	public abstract class SendPaymentsTaskTestBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new SendPayments.Task(LoggingService, MockedWebServiceClient.Object, AutogiroPaymentService);
		}
	}
}