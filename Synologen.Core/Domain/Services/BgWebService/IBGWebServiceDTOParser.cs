using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGWebServiceDTOParser
	{
		AutogiroPayer GetAutogiroPayer(string name, AutogiroServiceType serviceType);
	}
}