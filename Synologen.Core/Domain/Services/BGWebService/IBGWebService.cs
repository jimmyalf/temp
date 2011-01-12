using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BGWebService
{
	public interface IBGWebService
	{
		void SendConsents(ConsentToSend[] consents);
		void SendPayments(PaymentToSend[] payments);
		RecievedConsent[] GetNewConsents();
		RecievedPayment[] GetNewPayments();
		RecievedError[] GetNewErrors();
	}
}