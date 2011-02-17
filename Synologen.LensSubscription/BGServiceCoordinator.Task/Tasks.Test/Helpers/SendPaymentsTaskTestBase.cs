using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public class SendPaymentsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new SendPayments.Task(Log4NetLogger);
		}
	}
}