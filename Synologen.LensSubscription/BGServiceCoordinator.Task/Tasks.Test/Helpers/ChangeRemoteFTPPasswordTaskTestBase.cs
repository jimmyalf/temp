using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public class ChangeRemoteFTPPasswordTaskTestBase : CommonTaskTestBase
	{
		protected IFtpCommandService FtpCommandService;

		protected override void SetUp()
		{
			base.SetUp();
			FtpCommandService = A.Fake<IFtpCommandService>();

		}
		protected override ITask GetTask()
		{
			return new ChangeRemoteFTPPassword.Task(Log4NetLogger, FtpCommandService);
		}
	}
}