namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IBGConfigurationSettings
	{
		string GetPaymentRecieverBankGiroNumber();
		string GetPaymentRevieverCustomerNumber();
	}
}