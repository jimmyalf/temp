using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
	public abstract class SendPaymentsTaskTestBase : TaskTestBase
	{
		protected IAutogiroFileWriter<PaymentsFile, Payment> PaymentFileWriter;
		protected IBGPaymentToSendRepository BGPaymentToSendRepository;

		protected SendPaymentsTaskTestBase()
		{
			PaymentFileWriter = A.Fake<IAutogiroFileWriter<PaymentsFile, Payment>>();
			BGPaymentToSendRepository = A.Fake<IBGPaymentToSendRepository>();
		}
		protected override ITask GetTask()
		{
			return new SendPayments.Task(
				Log4NetLogger,
				BGPaymentToSendRepository);
		}
	}
}