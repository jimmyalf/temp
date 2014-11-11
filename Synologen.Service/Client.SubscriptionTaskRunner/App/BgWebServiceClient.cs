using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Synologen.Service.Client.SubscriptionTaskRunner.App
{
	public class BgWebServiceClient : ClientBase<IBGWebService>, IBGWebServiceClient
	{
		public bool TestConnection() { return Channel.TestConnection(); }
		public int RegisterPayer(string name, AutogiroServiceType serviceType) { return Channel.RegisterPayer(name, serviceType); }
		public void SendConsent(ConsentToSend consent) { Channel.SendConsent(consent); }
		public RecievedError[] GetErrors(AutogiroServiceType serviceType) { return Channel.GetErrors(serviceType); }
		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType) { return Channel.GetPayments(serviceType); }
		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType) { return Channel.GetConsents(serviceType); }
		public void SendPayment(PaymentToSend payment) { Channel.SendPayment(payment); }
		public void SetConsentHandled(ReceivedConsent consent) { Channel.SetConsentHandled(consent); }
		public void SetPaymentHandled(ReceivedPayment payment) { Channel.SetPaymentHandled(payment); }
		public void SetErrorHandled(RecievedError error) { Channel.SetErrorHandled(error); }
	}
}