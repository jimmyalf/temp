using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.ServiceCoordinator.App
{
	public class MockBgWebServiceClient : ClientBase<IBGWebService>, IBGWebService
	{
		public void SendConsent(ConsentToSend consent) { return; }
		public RecievedError[] GetNewErrors() { return new RecievedError[]{}; }
		public void SendPayment(PaymentToSend payment) { return; }
		public ReceivedConsent[] GetConsents() { return new ReceivedConsent[]{}; }
		public void SetConsentHandled(int id) { return; }
		public ReceivedPayment[] GetPayments() { return new ReceivedPayment[]{}; }
		public void SetPaymentHandled(int id) { return; }
	}
}