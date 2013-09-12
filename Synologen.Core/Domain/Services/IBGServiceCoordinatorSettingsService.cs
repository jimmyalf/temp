namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IBGServiceCoordinatorSettingsService
	{
		string GetPaymentRecieverBankGiroNumber();
		string GetPaymentRevieverCustomerNumber();
		string GetSentFilesFolderPath();
		string GetFtpUploadFolderUrl();
		string GetFtpUserName();
		string GetHMACHashKey();
        string GetReceivedFilesFolderPath();
	    string GetBackupFilesFolderPath();
		bool UseBinaryFTPTransfer { get; }
		string ReceiveFileNameRegexPattern();
	}
}