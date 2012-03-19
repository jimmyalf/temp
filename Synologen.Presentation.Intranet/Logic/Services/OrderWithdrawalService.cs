using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class OrderWithdrawalService
	{
		private readonly ISynologenSettingsService _synologenSettingsService;

		public OrderWithdrawalService(ISynologenSettingsService synologenSettingsService)
		{
			_synologenSettingsService = synologenSettingsService;
		}
		
		public DateTime GetExpectedFirstWithdrawalDate(Subscription subscription)
		{
		 	if(subscription.CreatedDate.Day <= _synologenSettingsService.SubscriptionCutoffDate)
		 	{
		 		return new DateTime(
					subscription.CreatedDate.Year, 
					subscription.CreatedDate.Month, 
					_synologenSettingsService.SubscriptionWithdrawalDate);
		 	}
			else
		 	{
		 		return new DateTime(
					subscription.CreatedDate.Year, 
					subscription.CreatedDate.Month, 
					_synologenSettingsService.SubscriptionWithdrawalDate)
					.AddMonths(1);
		 	}
		}
	}
}