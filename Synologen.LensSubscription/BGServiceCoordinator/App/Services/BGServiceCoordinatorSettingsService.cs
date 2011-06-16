using System.Configuration;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGServiceCoordinatorSettingsService : IBGServiceCoordinatorSettingsService
	{
		public string GetPaymentRecieverBankGiroNumber()
		{
			return ConfigurationManager.AppSettings["PaymentRecieverBankGiroNumber"];
		}
		public string GetPaymentRevieverCustomerNumber()
		{
			return ConfigurationManager.AppSettings["PaymentRevieverCustomerNumber"];
		}
		public string GetSentFilesFolderPath()
		{
			return ConfigurationManager.AppSettings["SentFilesFolderPath"];
		}
		public string GetFtpUploadFolderUrl()
		{
			return ConfigurationManager.AppSettings["FtpUploadFolderUrl"];
		}

		public string GetFtpUserName()
		{
			return ConfigurationManager.AppSettings["FtpUserName"];
		}

		public string GetHMACHashKey()
		{
			return ConfigurationManager.AppSettings["HMACHashKey"];
		}
        public string GetReceivedFilesFolderPath()
        {
            return ConfigurationManager.AppSettings["ReceivedFilesFolderPath"];
        }

	    public string GetBackupFilesFolderPath()
	    {
	        return ConfigurationManager.AppSettings["BackupFilesFolderPath"];
	    }

		public bool UseBinaryFTPTransfer
		{
			get
			{
				var useBinaryFTPTransfer = ConfigurationManager.AppSettings["UseBinaryFTPTransfer"];
				return useBinaryFTPTransfer != null && useBinaryFTPTransfer.ToLower().Equals("true");
			}
		}
	}
}