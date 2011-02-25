using System.Configuration;
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
	}
}