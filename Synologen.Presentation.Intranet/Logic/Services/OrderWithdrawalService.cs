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
		
		public DateTime GetExpectedFirstWithdrawalDate(DateTime subscriptionCreatedDate, bool isAlreadyConsentedSubscription)
		{
			//Subscription is already consented and withdrawal date not yet passed
			if(isAlreadyConsentedSubscription && subscriptionCreatedDate.Day <= _synologenSettingsService.SubscriptionWithdrawalDate)
			{
				GetWithdrawalDateInGivenMonth(subscriptionCreatedDate);
			}
			//Subscription is already consented but withdrawal date has passed
			if(isAlreadyConsentedSubscription && subscriptionCreatedDate.Day > _synologenSettingsService.SubscriptionWithdrawalDate)
			{
				GetWithdrawalDateInNextMonth(subscriptionCreatedDate);
			}
			//Subscription is not yet consented and cut off date has not yet passed
			if(!isAlreadyConsentedSubscription && subscriptionCreatedDate.Day <= _synologenSettingsService.SubscriptionCutoffDate)
		 	{
		 		return GetWithdrawalDateInGivenMonth(subscriptionCreatedDate);
		 	}
			//Subscription is not yet consented but cut off date has passed
			return GetWithdrawalDateInNextMonth(subscriptionCreatedDate);
		}

		private DateTime GetWithdrawalDateInGivenMonth(DateTime value)
		{
		 	return new DateTime(value.Year, value.Month, _synologenSettingsService.SubscriptionWithdrawalDate);
		}
		private DateTime GetWithdrawalDateInNextMonth(DateTime value)
		{
			return new DateTime(value.Year, value.Month, _synologenSettingsService.SubscriptionWithdrawalDate).AddMonths(1);
		}
	}
}