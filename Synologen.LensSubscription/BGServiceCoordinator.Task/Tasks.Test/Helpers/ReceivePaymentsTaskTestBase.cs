using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceivePaymentsTaskTestBase : TaskTestBase
    {
        protected override ITask GetTask()
        {
            return new ReceivePayments.Task(
                Log4NetLogger,
                ReceivedFileRepository,
                BGReceivedPaymentRepository,
                PaymentFileReader);
        }
    }
}
