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
			var paymentCutOffDayInMonth = _bgServiceCoordinatorSettingsService.GetPaymentCutOffDayInMonth();
			var returnDate = new DateTime(SystemTime.Now.Year, SystemTime.Now.Month, paymentDayInMonth);
			return SystemTime.Now.Day >= paymentCutOffDayInMonth ? returnDate.AddMonths(1) : returnDate;
		}
	}
}