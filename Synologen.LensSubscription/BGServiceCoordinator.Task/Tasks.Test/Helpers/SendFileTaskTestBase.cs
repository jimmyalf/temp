using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class SendFileTaskTestBase : TaskTestBase
	{
		protected override ITask GetTask()
		{
			return new SendFile.Task(Log4NetLogger);
		}
	}
}