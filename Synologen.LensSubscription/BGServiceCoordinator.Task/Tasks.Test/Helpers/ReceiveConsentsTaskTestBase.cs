using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceiveConsentsTaskTestBase : TaskTestBase
    {
        protected override ITask GetTask()
        {
            return new ReceiveConsents.Task(
                Log4NetLogger,
                ReceivedFileRepository,
                BGReceivedConsentRepository,
                ConsentFileReader);
        }
    }
}
