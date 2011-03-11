namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IBGServiceCoordinatorSettingsService
	{
		string GetPaymentRecieverBankGiroNumber();
		string GetPaymentRevieverCustomerNumber();
		string GetSentFilesFolderPath();
		string GetFtpUploadFolderUrl();
		//NetworkCredential GetFtpCredential();
		string GetFtpUserName();
		string GetHMACHashKey();
        string GetReceivedFilesFolderPath();
	    string GetBackupFilesFolderPath();
	}
}