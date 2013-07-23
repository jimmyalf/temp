using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Client.SubscriptionTaskRunner.App
{
	public class AutogiroPaymentService : IAutogiroPaymentService
	{
		private readonly IServiceCoordinatorSettingsService _bgServiceCoordinatorSettingsService;

		public AutogiroPaymentService(IServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService)
		{
			_bgServiceCoordinatorSettingsService = bgServiceCoordinatorSettingsService;
		}

		public DateTime GetPaymentDate()
		{
			var paymentDayInMonth = _bgServiceCoordinatorSettingsService.GetPaymentDayInMonth();
			return GetSelectedOrLastDateInMonth(paymentDayInMonth);
		}

		public DateTime GetCutOffDateTime()
		{
			var paymentCutOffDayInMonth = _bgServiceCoordinatorSettingsService.GetPaymentCutOffDayInMonth();
			return GetSelectedOrLastDateInMonth(paymentCutOffDayInMonth, 23, 59, 59);
		}

		private DateTime GetSelectedOrLastDateInMonth(int day, int hour = 0, int minute = 0, int second = 0)
		{
			var lastDayInThisMonth = DateTime.DaysInMonth(SystemTime.Now.Year, SystemTime.Now.Month);
			return lastDayInThisMonth < day 
				? new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, lastDayInThisMonth, hour, minute, second)
				: new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, day, hour, minute, second);
		}
	}
}