using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class ChangeRemoteFTPPasswordTaskTestBase : CommonTaskTestBase
	{
		//protected IFtpCommandService FtpCommandService;
		protected IBGFtpChangePasswordService BGFtpChangePasswordService;
		protected IBGFtpPasswordService BGFtpPasswordService;

		protected override void SetUp()
		{
			base.SetUp();
			//FtpCommandService = A.Fake<IFtpCommandService>();
			BGFtpChangePasswordService = A.Fake<IBGFtpChangePasswordService>();
			BGFtpPasswordService = A.Fake<IBGFtpPasswordService>();
			TaskRepositoryResolver.AddRepository(BGFtpPasswordService);

		}
		protected override ITask GetTask()
		{
			return new ChangeRemoteFTPPassword.Task(LoggingService, BGFtpChangePasswordService);
		}
	}
}