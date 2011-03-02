using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceivePaymentsTaskTestBase : CommonTaskTestBase
    {
    	protected IBGReceivedPaymentRepository BGReceivedPaymentRepository;
    	protected IAutogiroFileReader<PaymentsFile, Payment> PaymentFileReader;

		protected override void SetUp()
		{
			base.SetUp();

			BGReceivedPaymentRepository = A.Fake<IBGReceivedPaymentRepository>();
    		PaymentFileReader = A.Fake<IAutogiroFileReader<PaymentsFile, Payment>>();
			A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGReceivedPaymentRepository>()).Returns(BGReceivedPaymentRepository);
		}

    	protected override ITask GetTask()
        {
            return new ReceivePayments.Task(
                Log4NetLogger,
                //ReceivedFileRepository,
                //BGReceivedPaymentRepository,
                PaymentFileReader,
				TaskRepositoryResolver);
        }
    }
}
