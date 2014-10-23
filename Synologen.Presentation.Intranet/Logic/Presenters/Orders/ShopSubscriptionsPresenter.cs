using System;
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
using Spinit.Wpc.Synologen.Core.Extensions;
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
		private readonly IRoutingService _routingService;

		public ShopSubscriptionsPresenter(
			IShopSubscriptionsView view, 
			ISubscriptionRepository subscriptionRepository, 
			ISynologenMemberService synologenMemberService,
			IRoutingService routingService) : base(view)
		{
			_subscriptionRepository = subscriptionRepository;
			_synologenMemberService = synologenMemberService;
			_routingService = routingService;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var shopId = _synologenMemberService.GetCurrentShopId();
			var subscriptions = _subscriptionRepository.FindBy(new AllSubscriptionsForShopCriteria(shopId));
			Func<int, string> getPageUrl = pageId => _routingService.GetPageUrl(pageId) ?? "#";
			var viewModelSubscriptions = subscriptions.Select(x => ParseSubscription(x, () => getPageUrl(View.CustomerDetailsPageId), () => getPageUrl(View.SubscriptionDetailsPageId)));
			View.Model = new ShopSubscriptionsModel(viewModelSubscriptions);
			_routingService.GetPageUrl(55, new {customer = 1, subscription = 55});
		}

		protected SubscriptionListItem ParseSubscription(Subscription subscription, Func<string> getCustomerDetailsUrl, Func<string> getSubscriptionDetailsUrl)
		{
		    const string urlFormat = "{Url}?{Parameter}={ParameterValue}";
		    return new SubscriptionListItem
		    {
		        CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName),
		        CurrentBalance = subscription.GetCurrentAccountBalance().Total.ToString("N2"),
		        MonthlyAmount = subscription.SubscriptionItems.Where(x => x.IsActive).Sum(x => x.MonthlyWithdrawal.Total).ToString("N2"),
		        Status = GetStatusMessage(subscription),
		        CustomerDetailsUrl = urlFormat.ReplaceWith(new { Url = getCustomerDetailsUrl(), Parameter = "customer", ParameterValue = subscription.Customer.Id }),
		        SubscriptionDetailsUrl = urlFormat.ReplaceWith(new { Url = getSubscriptionDetailsUrl(), Parameter = "subscription", ParameterValue = subscription.Id }),
				BankAccountNumber = subscription.BankAccountNumber
		    };
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}

		protected string GetStatusMessage(Subscription subscription)
		{
		    return Switch.On<Subscription,string>(subscription)
		        .Case(s => !s.Active, "Vilande autogiro")
		        .Case(s => s.Errors != null && s.Errors.Any(e => !e.IsHandled), "Transaktion ej genomförd")
		        .Default(s => s.ConsentStatus.GetEnumDisplayName())
		        .Evaluate();
		}
	}
}