using System;
using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.App
{
	public class BgWebServiceClient : ClientBase<IBGWebService>, IBGWebService
	{
		public void SendConsent(ConsentToSend consent) { Channel.SendConsent(consent); }
		public RecievedError[] GetNewErrors() { return Channel.GetNewErrors(); }
		public void SendPayment(PaymentToSend payment) { Channel.SendPayment(payment); }
		public RecievedConsent[] GetConsents() { return Channel.GetConsents(); }
		public void SetConsentHandled(int id) { Channel.SetConsentHandled(id); }
	}
}