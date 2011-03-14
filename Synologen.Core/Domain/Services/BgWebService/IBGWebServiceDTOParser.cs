using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGWebServiceDTOParser
	{
		BGConsentToSend ParseConsent(ConsentToSend consentToSend, AutogiroPayer payer);
		ReceivedConsent ParseConsent(BGReceivedConsent consent);
		BGPaymentToSend ParsePayment(PaymentToSend payment, AutogiroPayer payer);
		ReceivedPayment ParsePayment(BGReceivedPayment payment);
		RecievedError ParseError(BGReceivedError error);
	}
}