﻿using System;
//using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
//using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Utility;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
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
		        CurrentBalance = GetCurrentAccountBalance(subscription.Transactions.ToList()).ToString("N2"),
		        MonthlyAmount = subscription.SubscriptionItems.Where(x => x.IsActive).Sum(x => x.AmountForAutogiroWithdrawal).ToString("N2"),
		        Status = GetStatusMessage(subscription),
		        CustomerDetailsUrl = urlFormat.ReplaceWith(new { Url = getCustomerDetailsUrl(), Parameter = "customer", ParameterValue = subscription.Customer.Id }),
		        SubscriptionDetailsUrl = urlFormat.ReplaceWith(new { Url = getSubscriptionDetailsUrl(), Parameter = "subscription", ParameterValue = subscription.Id }),
		    };
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}

		protected string GetStatusMessage(Subscription subscription)
		{
		    return Switch.On<Subscription,string>(subscription)
		        .Case(s => !s.Active, "Inaktivt")
		        .Case(s => s.Errors != null && s.Errors.Any(e => !e.IsHandled), "Har ohanterade fel")
		        .Case(s => s.ConsentStatus == SubscriptionConsentStatus.Accepted, "Aktivt")
		        .Case(s => s.ConsentStatus == SubscriptionConsentStatus.Denied, "Ej medgivet")
		        .Case(s => s.ConsentStatus == SubscriptionConsentStatus.NotSent, "Medgivande ej skickat")
		        .Case(s => s.ConsentStatus == SubscriptionConsentStatus.Sent, "Skickat för medgivande")
		        .Evaluate();
		}

		protected decimal GetCurrentAccountBalance(IList<SubscriptionTransaction> transactions)
		{
			if (transactions == null || !transactions.Any()) return 0;
			Func<SubscriptionTransaction, bool> isWithdrawal = transaction => (transaction.Reason == TransactionReason.Withdrawal || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Withdrawal;
			Func<SubscriptionTransaction, bool> isDeposit = transaction => (transaction.Reason == TransactionReason.Payment || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Deposit;
			var withdrawals = transactions.Where(isWithdrawal).Sum(x => x.Amount);
			var deposits = transactions.Where(isDeposit).Sum(x => x.Amount);
			return deposits - withdrawals;
		}
	}
}