using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ChangeRemoteFTPPassword
{
	public class Task : TaskBase
	{
		private readonly IBGFtpChangePasswordService _bgFTPChangePasswordService;

		public Task(ILoggingService loggingService, IBGFtpChangePasswordService bgFTPChangePasswordService) : base("ChangeRemoteFTPPassword", loggingService)
		{
			_bgFTPChangePasswordService = bgFTPChangePasswordService;
		}

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var bgFtpPasswordService = context.Resolve<IBGFtpPasswordService>();
				var oldPassword = bgFtpPasswordService.GetCurrentPassword();
				var newPassword = bgFtpPasswordService.GenerateNewPassword();
				_bgFTPChangePasswordService.Execute(oldPassword, newPassword);
				bgFtpPasswordService.StoreNewActivePassword(newPassword);
				LogInfo("Password was updated successfully");
			});
		}
	}
}