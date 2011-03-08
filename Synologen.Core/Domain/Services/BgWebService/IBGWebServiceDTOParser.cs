using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGWebServiceDTOParser
	{
		AutogiroPayer GetAutogiroPayer(string name, AutogiroServiceType serviceType);
		BGConsentToSend ParseConsent(ConsentToSend consentToSend, AutogiroPayer payer);
	}
}