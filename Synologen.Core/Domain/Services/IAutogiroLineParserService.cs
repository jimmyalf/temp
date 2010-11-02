using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IAutogiroLineParserService
	{
		Model.Autogiro.Recieve.Payment ReadPaymentLine(string line);
		string WritePaymentPostLine(Payment payment);
		string WritePaymentHeaderLine(PaymentsFile paymentsFile);
		string WriteConsentPostLine(Consent consent);
		string WriteConsentHeaderLine(ConsentsFile consentsFile);
	}
}