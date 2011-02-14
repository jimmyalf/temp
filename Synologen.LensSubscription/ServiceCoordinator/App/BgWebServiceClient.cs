using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.App
{
	public class BgWebServiceClient : ClientBase<IBGWebService>, IBGWebService
	{
		public void SendConsent(ConsentToSend consent) { Channel.SendConsent(consent); }
		public RecievedError[] GetNewErrors() { return Channel.GetNewErrors(); }
		public void SendPayment(PaymentToSend payment) { Channel.SendPayment(payment); }
		public ReceivedPayment[] GetPayments() { return Channel.GetPayments(); }
		public RecievedConsent[] GetConsents() { return Channel.GetConsents(); }
		public void SetConsentHandled(int id) { Channel.SetConsentHandled(id); }
		public void SetPaymentHandled(int id) { Channel.SetPaymentHandled(id); }
	}
}