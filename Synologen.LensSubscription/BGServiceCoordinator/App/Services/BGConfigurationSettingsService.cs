using System.Configuration;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.Services
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
	}
}