using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using WebFormsMvp;
using Subscription=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class EditLensSubscriptionPresenter : Presenter<IEditLensSubscriptionView>
	{
		private const string SubscriptionRequestParameter = "subscription";
		private const bool IsActive = true;
		private const bool IsNotActive = false;
		private const string DateTimeFormat = "yyyy-MM-dd";
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
			View.UpdateForm += View_UpdateForm;
		}

		public void View_UpdateForm(object sender, SaveSubscriptionEventArgs e) 
		{
			View.Model.AccountNumber = e.AccountNumber;
			View.Model.ClearingNumber = e.ClearingNumber;
			View.Model.MonthlyAmount = e.MonthlyAmount;
			View.Model.Notes = e.Notes;
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
			View.StopSubscription -= View_StopSubscription;
			View.StartSubscription -= View_StartSubscription;
			View.UpdateForm -= View_UpdateForm;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var subscription = TryGetSubscription();
			SetAccess(subscription);
			if(!View.Model.DisplayForm) return;
			View.Model.StopButtonEnabled = subscription.Active;
			View.Model.StartButtonEnabled = !subscription.Active;
			View.Model.AccountNumber = subscription.PaymentInfo.AccountNumber;
			View.Model.ActivatedDate = subscription.With(x => x.ActivatedDate).Return(x => x.Value.ToString(DateTimeFormat), "Nej");
			View.Model.ClearingNumber = subscription.PaymentInfo.ClearingNumber;
			View.Model.CreatedDate = subscription.CreatedDate.ToString(DateTimeFormat);
			View.Model.CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			View.Model.MonthlyAmount = subscription.PaymentInfo.MonthlyAmount.ToString();
			View.Model.Status = subscription.Active ? SubscriptionStatus.Started.GetEnumDisplayName() : SubscriptionStatus.Stopped.GetEnumDisplayName();
			View.Model.ConsentStatus = subscription.ConsentStatus.GetEnumDisplayName();
			View.Model.Notes = subscription.Notes;
			if(View.ReturnPageId>0)
			{
				var url = _synologenMemberService.GetPageUrl(View.ReturnPageId);
				View.Model.ReturnUrl = String.Format("{0}?customer={1}", url, subscription.Customer.Id);
			}
			else
			{
				View.Model.ReturnUrl = "#";
			}
		}

		public void View_Submit(object sender, SaveSubscriptionEventArgs eventArgs)
		{
			TrySaveSubscription(eventArgs);
			TryRedirect();
		}

		public void View_StopSubscription(object sender, EventArgs e)
		{
			TryUpdateSubscriptionStatus(IsNotActive);
			TryRedirect();
		}

		public void View_StartSubscription(object sender, EventArgs e) 
		{
			TryUpdateSubscriptionStatus(IsActive);
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
			subscription.PaymentInfo.MonthlyAmount = args.MonthlyAmount.ToDecimalOrDefault();
			subscription.Notes = args.Notes;
			_subscriptionRepository.Save(subscription);
		}

		public void TryUpdateSubscriptionStatus(bool isActive)
		{
			var subscription = TryGetSubscription();
			subscription.Active = isActive;
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