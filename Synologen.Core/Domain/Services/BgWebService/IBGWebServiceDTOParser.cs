using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using BGWebService_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;
using BGServer_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.AutogiroServiceType;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGWebServiceDTOParser
	{
		AutogiroPayer GetAutogiroPayer(string name, BGWebService_AutogiroServiceType serviceType);
		BGConsentToSend ParseConsent(ConsentToSend consentToSend, AutogiroPayer payer);
		BGPaymentToSend ParsePayment(PaymentToSend payment, AutogiroPayer payer);
		ReceivedPayment ParsePayment(BGReceivedPayment payment);
		BGServer_AutogiroServiceType ParseServiceType(BGWebService_AutogiroServiceType type);
	}
}