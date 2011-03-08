using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Synologen.LensSubscription.ServiceCoordinator.App
{
	public class MockBgWebServiceClient : ClientBase<IBGWebService>, IBGWebService
	{
		public bool TestConnection() { return true; }
		public int RegisterPayer(string name, AutogiroServiceType serviceType) { return 0; }
		public void SendConsent(ConsentToSend consent) { return; }
		public void SendPayment(PaymentToSend payment) { return; }
		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType) { return new ReceivedConsent[]{}; }
		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType) { return new ReceivedPayment[]{}; }
		public RecievedError[] GetErrors(AutogiroServiceType serviceType) { return new RecievedError[]{}; }
		public void SetConsentHandled(ReceivedConsent consent) { return; }
		public void SetPaymentHandled(ReceivedPayment payment) { return; }
		public void SetErrorHandled(RecievedError error) { return; }
	}
}