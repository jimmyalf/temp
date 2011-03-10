using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public class ChangeRemoteFTPPasswordTaskTestBase : CommonTaskTestBase
	{
		protected override ITask GetTask()
		{
			return new ChangeRemoteFTPPassword.Task(Log4NetLogger, TaskRepositoryResolver);
		}
	}
}