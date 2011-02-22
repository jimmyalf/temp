using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendFile
{
	public class Task : TaskBase
	{
		public Task(ILoggingService loggingService) : base("SendFile", loggingService, BGTaskSequenceOrder.SendFiles) {}
		public override void Execute()
		{
			RunLoggedTask(() => {});
		}
	}
}