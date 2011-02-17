using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendPayments
{
	public class Task : TaskBase
	{
		public Task(ILoggingService loggingService) 
			: base("SendPayments", loggingService, BGTaskSequenceOrder.SendTask) {}

		public override void Execute() { return; }
	}
}