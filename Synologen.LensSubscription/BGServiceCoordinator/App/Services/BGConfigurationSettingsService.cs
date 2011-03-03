using System;
using System.Configuration;
using System.Net;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGConfigurationSettingsService : IBGConfigurationSettingsService
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

		public NetworkCredential GetFtpCredential() 
		{ 
			var userName = ConfigurationManager.AppSettings["FtpUserName"];
			var password = ConfigurationManager.AppSettings["FtpPassword"];
			return new NetworkCredential(userName, password);
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
	        return "";
	    }
	}
}