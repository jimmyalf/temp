using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IBGWebService 
	{
		void SendConsent(ConsentToSend consent);
	}
}