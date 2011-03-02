using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	[ServiceContract]
	public interface IBGWebService 
	{
		[OperationContract] int RegisterPayer(string name, AutogiroServiceType serviceType);
		[OperationContract] void SendConsent(ConsentToSend consent);
		[OperationContract] void SendPayment(PaymentToSend payment);

		[OperationContract] ReceivedConsent[] GetConsents(AutogiroServiceType serviceType);
		[OperationContract] ReceivedPayment[] GetPayments(AutogiroServiceType serviceType);
		[OperationContract] RecievedError[] GetErrors(AutogiroServiceType serviceType);

		[OperationContract] void SetConsentHandled(ReceivedConsent consent);
		[OperationContract] void SetPaymentHandled(ReceivedPayment payment);
		[OperationContract] void SetErrorHandled(RecievedError error);
	}


}