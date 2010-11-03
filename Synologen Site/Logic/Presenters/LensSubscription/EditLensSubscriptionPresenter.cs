using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class EditLensSubscriptionPresenter : Presenter<IEditLensSubscriptionView>
	{
		private const string SubscriptionRequestParameter = "subscription";
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISynologenMemberService _synologenMemberService;

		public EditLensSubscriptionPresenter(IEditLensSubscriptionView view, ISubscriptionRepository subscriptionRepository, ISynologenMemberService synologenMemberService) : base(view)
		{
			_subscriptionRepository = subscriptionRepository;
			_synologenMemberService = synologenMemberService;
			View.Load += View_Load;
			View.Submit += View_Submit;
			View.StopSubscription += View_StopSubscription;
			View.StartSubscription += View_StartSubscription;
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
			View.StopSubscription -= View_StopSubscription;
			View.StartSubscription -= View_StartSubscription;
		}
		public void View_Load(object sender, EventArgs e)
		{
			var subscription = TryGetSubscription();
			SetAccess(subscription);
			if(!View.Model.DisplayForm) return;
			View.Model.StopButtonEnabled = subscription.Status == SubscriptionStatus.Active;
			View.Model.StartButtonEnabled = subscription.Status == SubscriptionStatus.Stopped;
			View.Model.AccountNumber = subscription.PaymentInfo.AccountNumber;
			View.Model.ActivatedDate = subscription.With(x => x.ActivatedDate).Return(x => x.Value.ToString("yyyy-MM-dd"), String.Empty);
			View.Model.ClearingNumber = subscription.PaymentInfo.ClearingNumber;
			View.Model.CreatedDate = subscription.CreatedDate.ToString("yyyy-MM-dd");
			View.Model.CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			View.Model.MonthlyAmount = subscription.PaymentInfo.MonthlyAmount;
			View.Model.Status = subscription.Status.GetEnumDisplayName();
		}

		public void View_Submit(object sender, SaveSubscriptionEventArgs eventArgs)
		{
			TrySaveSubscription(eventArgs);
			TryRedirect();
		}

		public void View_StopSubscription(object sender, EventArgs e)
		{
			TryUpdateSubscriptionStatus(SubscriptionStatus.Stopped);
			TryRedirect();
		}

		public void View_StartSubscription(object sender, EventArgs e) 
		{ 
			TryUpdateSubscriptionStatus(SubscriptionStatus.Active);
			TryRedirect();
		}

		private void SetAccess(Subscription subscription) 
		{
			if(subscription == null)
			{
				View.Model.SubscriptionDoesNotExist = true;
				return;
			}
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions = !_synologenMemberService.ShopHasAccessTo(ShopAccess.LensSubscription);
			View.Model.ShopDoesNotHaveAccessGivenCustomer = !_synologenMemberService.GetCurrentShopId().Equals(subscription.Customer.Shop.Id);
		}

		private void TrySaveSubscription(SaveSubscriptionEventArgs args)
		{
			var subscription = TryGetSubscription();
			subscription.PaymentInfo.AccountNumber = args.AccountNumber;
			subscription.PaymentInfo.ClearingNumber = args.ClearingNumber;
			subscription.PaymentInfo.MonthlyAmount = args.MonthlyAmount;
			_subscriptionRepository.Save(subscription);
		}

		public void TryUpdateSubscriptionStatus(SubscriptionStatus newStatus)
		{
			var subscription = TryGetSubscription();
			subscription.Status = newStatus;
			_subscriptionRepository.Save(subscription);
			TryRedirect();
		}

		public Subscription TryGetSubscription()
		{
			var subscriptionId = HttpContext.Request.Params[SubscriptionRequestParameter].ToIntOrDefault();
			return subscriptionId <= 0 ? null : _subscriptionRepository.Get(subscriptionId);
		}

		private void TryRedirect()
		{
			if(View.RedirectOnSavePageId <= 0)
			{
				var currentpage = HttpContext.Request.Url.PathAndQuery;
				HttpContext.Response.Redirect(currentpage);
				return;
			}
			var redirectPageUrl = _synologenMemberService.GetPageUrl(View.RedirectOnSavePageId);
			if(String.IsNullOrEmpty(redirectPageUrl)) return;
			var subscription = TryGetSubscription();
			HttpContext.Response.Redirect(String.Concat(redirectPageUrl, "?customer=", subscription.Customer.Id));
		}
	}
}