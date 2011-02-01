using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	[ServiceContract]
	public interface IBGWebService 
	{
		[OperationContract]
		void SendConsent(ConsentToSend consent);

		[OperationContract]
		RecievedConsent[] GetConsents();

		[OperationContract]
		void SetConsentHandled(int id);


		[OperationContract]
		RecievedError[] GetNewErrors();
	}
}