using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveConsents.ReceiveErrors
{
    public class Task : TaskBase
    {
    	public Task(ILoggingService loggingService) : base("ReceiveErrors", loggingService, BGTaskSequenceOrder.ReadTask) {}

    	public override void Execute()
    	{
    		base.RunLoggedTask(() => {});
    	}
    }
}
