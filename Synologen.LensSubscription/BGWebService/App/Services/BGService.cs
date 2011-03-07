using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGWebService.App.Services
{
	public class BGService : IBGWebService
	{
		public int RegisterPayer(string name, AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public void SendConsent(ConsentToSend consent) { throw new NotImplementedException(); }
		public void SendPayment(PaymentToSend payment) { throw new NotImplementedException(); }
		public ReceivedConsent[] GetConsents(AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public ReceivedPayment[] GetPayments(AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public RecievedError[] GetErrors(AutogiroServiceType serviceType) { throw new NotImplementedException(); }
		public void SetConsentHandled(ReceivedConsent consent) { throw new NotImplementedException(); }
		public void SetPaymentHandled(ReceivedPayment payment) { throw new NotImplementedException(); }
		public void SetErrorHandled(RecievedError error) { throw new NotImplementedException(); }
	}
}