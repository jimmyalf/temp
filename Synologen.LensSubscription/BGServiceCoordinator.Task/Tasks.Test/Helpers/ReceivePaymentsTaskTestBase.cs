using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceivePaymentsTaskTestBase : TaskTestBase
    {
    	protected IBGReceivedPaymentRepository BGReceivedPaymentRepository;
    	protected IAutogiroFileReader<PaymentsFile, Payment> PaymentFileReader;

    	protected ReceivePaymentsTaskTestBase()
    	{
    		BGReceivedPaymentRepository = A.Fake<IBGReceivedPaymentRepository>();
    		PaymentFileReader = A.Fake<IAutogiroFileReader<PaymentsFile, Payment>>();
    	}

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
