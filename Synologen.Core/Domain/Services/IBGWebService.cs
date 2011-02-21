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
		ReceivedConsent[] GetConsents();

		[OperationContract]
		void SetConsentHandled(int id);

		[OperationContract]
		RecievedError[] GetNewErrors();

		[OperationContract]
		void SendPayment(PaymentToSend payment);

		[OperationContract]
		ReceivedPayment[] GetPayments();

		[OperationContract]
		void SetPaymentHandled(int id);

		[OperationContract]
		void SetErrorHandled(int id);
	}
}