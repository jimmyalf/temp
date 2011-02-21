using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.ServiceCoordinator.App
{
	public class MockBgWebServiceClient : ClientBase<IBGWebService>, IBGWebService
	{
		public void SendConsent(ConsentToSend consent) { return; }
		public void SendPayment(PaymentToSend payment) { return; }
		public ReceivedConsent[] GetConsents() { return new ReceivedConsent[]{}; }
		public ReceivedPayment[] GetPayments() { return new ReceivedPayment[]{}; }
		public RecievedError[] GetErrors() { return new RecievedError[]{}; }
		public void SetConsentHandled(int id) { return; }
		public void SetPaymentHandled(int id) { return; }
		public void SetErrorHandled(int id) { return; }
	}
}