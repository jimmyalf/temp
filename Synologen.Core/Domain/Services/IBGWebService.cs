using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	[ServiceContract]
	public interface IBGWebService 
	{
		[OperationContract] void SendConsent(ConsentToSend consent);
		[OperationContract] void SendPayment(PaymentToSend payment);

		[OperationContract] ReceivedConsent[] GetConsents();
		[OperationContract] ReceivedPayment[] GetPayments();
		[OperationContract] RecievedError[] GetErrors();

		[OperationContract] void SetConsentHandled(int id);
		[OperationContract] void SetPaymentHandled(int id);
		[OperationContract] void SetErrorHandled(int id);
	}
}