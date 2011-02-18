using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class ReceiveErrorsTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ReceiveConsents.ReceiveErrors.Task(Log4NetLogger);
		}
	}
}