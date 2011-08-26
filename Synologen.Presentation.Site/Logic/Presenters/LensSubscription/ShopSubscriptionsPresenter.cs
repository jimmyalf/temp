﻿using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ShopSubscriptionsPresenter : Presenter<IShopSubscriptionsView> 
	{
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISynologenMemberService _synologenMemberService;

		public ShopSubscriptionsPresenter(IShopSubscriptionsView view, ISubscriptionRepository subscriptionRepository, ISynologenMemberService synologenMemberService) : base(view)
		{
			_subscriptionRepository = subscriptionRepository;
			_synologenMemberService = synologenMemberService;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var shopId = _synologenMemberService.GetCurrentShopId();
			var subscriptions = _subscriptionRepository.FindBy(new AllSubscriptionsForShopCriteria(shopId));
			Func<int, string> getPageUrl = pageId => _synologenMemberService.GetPageUrl(pageId) ?? "#";
			var viewModelSubscriptions = subscriptions.Select(x => ParseSubscription(x, () => getPageUrl(View.CustomerDetailsPageId), () => getPageUrl(View.SubscriptionDetailsPageId)));
			View.Model = new ShopSubscriptionsModel(viewModelSubscriptions);
		}

		protected SubscriptionListItem ParseSubscription(Subscription subscription, Func<string> getCustomerDetailsUrl, Func<string> getSubscriptionDetailsUrl)
		{
			const string urlFormat = "{Url}?{Parameter}={ParameterValue}";
			return new SubscriptionListItem
			{
				CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName),
				CurrentBalance = GetCurrentBalance(subscription.Transactions),
				MonthlyAmount = subscription.PaymentInfo.MonthlyAmount.ToString("N2"),
				Status = GetCurrentStatus(subscription),
				CustomerDetailsUrl = urlFormat.ReplaceWith(new { Url = getCustomerDetailsUrl(), Parameter = "customer", ParameterValue = subscription.Customer.Id }),
				SubscriptionDetailsUrl = urlFormat.ReplaceWith(new { Url = getSubscriptionDetailsUrl(), Parameter = "subscription", ParameterValue = subscription.Id }),
			};
		}

		protected virtual string GetCurrentStatus(Subscription subscription)
		{
			var key = StatusDictionary.Keys.FirstOrDefault(findMessageKey => findMessageKey(subscription));
			return (key == null) ? "Okänd status" : StatusDictionary[key];
		}

		protected Dictionary<Func<Subscription,bool>,string> StatusDictionary
		{
			get
			{
				return new Dictionary<Func<Subscription, bool>, string>
				{
					{subscription => !subscription.Active, "Inaktivt"}, 
					{subscription => subscription.Errors != null && subscription.Errors.Any(x => !x.IsHandled), "Har ohanterade fel"},
					{subscription => subscription.ConsentStatus == SubscriptionConsentStatus.Accepted, "Medgivet"},
					{subscription => subscription.ConsentStatus == SubscriptionConsentStatus.Denied, "Ej medgivet"},
					{subscription => subscription.ConsentStatus == SubscriptionConsentStatus.NotSent, "Medgivande ej skickat"},
					{subscription => subscription.ConsentStatus == SubscriptionConsentStatus.Sent, "Skickat för medgivande"},
				};
			}
		}

		protected virtual string GetCurrentBalance(IEnumerable<SubscriptionTransaction> transactions)
		{
			if (transactions == null || transactions.Count() == 0) return 0.ToString("N2");
			Func<SubscriptionTransaction, bool> isWithdrawal = transaction => (transaction.Reason == TransactionReason.Withdrawal || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Withdrawal;
			Func<SubscriptionTransaction, bool> isDeposit = transaction => (transaction.Reason == TransactionReason.Payment || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Deposit;
			var deposits = transactions.Where(isDeposit).Sum(x => x.Amount);
			var withdrawals = transactions.Where(isWithdrawal).Sum(x => x.Amount);
			return (deposits - withdrawals).ToString("N2");
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}