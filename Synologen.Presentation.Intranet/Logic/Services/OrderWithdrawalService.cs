using System;
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
		
		public DateTime GetExpectedFirstWithdrawalDate(DateTime subscriptionCreatedDate)
		{
			if(subscriptionCreatedDate.Day <= _synologenSettingsService.SubscriptionCutoffDate)
		 	{
		 		return new DateTime(
					subscriptionCreatedDate.Year, 
					subscriptionCreatedDate.Month, 
					_synologenSettingsService.SubscriptionWithdrawalDate);
		 	}
			return new DateTime(
				subscriptionCreatedDate.Year, 
				subscriptionCreatedDate.Month, 
				_synologenSettingsService.SubscriptionWithdrawalDate)
				.AddMonths(1);
		}
	}
}