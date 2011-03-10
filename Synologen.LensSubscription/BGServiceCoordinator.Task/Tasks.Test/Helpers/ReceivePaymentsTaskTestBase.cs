using System;
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
			TaskRepositoryResolver.AddRepository(BGReceivedPaymentRepository);
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

    	protected static Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult MapPaymentResultType(PaymentResult result)
    	{
    		switch (result)
    		{
    			case PaymentResult.Approved: return Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult.Approved;
    			case PaymentResult.InsufficientFunds: return Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult.InsufficientFunds;
    			case PaymentResult.AGConnectionMissing: return Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult.AGConnectionMissing;
    			case PaymentResult.WillTryAgain: return Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult.WillTryAgain;
    			default: throw new ArgumentOutOfRangeException("result");
    		}
    	}
    }
}
