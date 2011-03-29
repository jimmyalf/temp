using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest.TestHelpers;

namespace ServiceCoordinator.AcceptanceTest.TestHelpers
{
	public abstract class ReceivePaymentTaskbase : TaskBase
	{
		protected BGReceivedPayment StoreBGPayment(Func<AutogiroPayer, BGReceivedPayment> getPayment, int payerNumber)
		{
			var autogiroPayer = autogiroPayerRepository.Get(payerNumber);
			var payment = getPayment.Invoke(autogiroPayer);
			bgReceivedPaymentRepository.Save(payment);
			return payment;
		}
	}
}