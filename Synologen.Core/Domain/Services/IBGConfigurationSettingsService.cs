using System.Net;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IBGConfigurationSettingsService
	{
		string GetPaymentRecieverBankGiroNumber();
		string GetPaymentRevieverCustomerNumber();
		string GetSentFilesFolderPath();
		string GetFtpUploadFolderUrl();
		NetworkCredential GetFtpCredential();
	}
}