using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Utility;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription
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
		}

		protected SubscriptionListItem ParseSubscription(Subscription subscription, Func<string> getCustomerDetailsUrl, Func<string> getSubscriptionDetailsUrl)
		{
			const string urlFormat = "{Url}?{Parameter}={ParameterValue}";
			return new SubscriptionListItem
			{
				CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName),
				CurrentBalance = subscription.GetCurrentAccountBalance().ToString("N2"),
				MonthlyAmount = subscription.PaymentInfo.MonthlyAmount.ToString("N2"),
				Status = GetStatusMessage(subscription),
				CustomerDetailsUrl = urlFormat.ReplaceWith(new { Url = getCustomerDetailsUrl(), Parameter = "customer", ParameterValue = subscription.Customer.Id }),
				SubscriptionDetailsUrl = urlFormat.ReplaceWith(new { Url = getSubscriptionDetailsUrl(), Parameter = "subscription", ParameterValue = subscription.Id }),
				MigrationUrl = _routingService.GetPageUrl(View.MigrationPageId, new { subscription = subscription.Id})
			};
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
				.Case(s => s.ConsentStatus == SubscriptionConsentStatus.Migrated, "Migrerat")
				.Evaluate();
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}