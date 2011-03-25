using System;
using System.Configuration;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.ServiceCoordinator.App
{
	public class ServiceCoordinatorSettingsService : IServiceCoordinatorSettingsService
	{
		public int GetPaymentDayInMonth() 
		{ 
			return Int32.Parse(ConfigurationManager.AppSettings["PaymentDayInMonth"]);
		}
		public int GetPaymentCutOffDayInMonth()
		{
			return Int32.Parse(ConfigurationManager.AppSettings["PaymentCutOffDayInMonth"]);
		}
	}
}