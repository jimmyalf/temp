using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using WebFormsMvp;
using Spinit.Extensions;
using Customer = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription
{
	public class CreateLensSubscriptionPresenter : Presenter<ICreateLensSubscriptionView>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly IRoutingService _routingService;

		public CreateLensSubscriptionPresenter(
			ICreateLensSubscriptionView view, 
			ICustomerRepository customerRepository, 
			ISubscriptionRepository subscriptionRepository, 
			ISynologenMemberService synologenMemberService,
			IRoutingService routingService) : base(view)
		{
			_customerRepository = customerRepository;
			_subscriptionRepository = subscriptionRepository;
			_synologenMemberService = synologenMemberService;
			_routingService = routingService;
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
			if(!_synologenMemberService.ShopHasAccessTo(ShopAccess.LensSubscription))
			{
				View.Model.ShopDoesNotHaveAccessToLensSubscriptions = true;
			}
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			if(!ShopHasAccessToCustomer(customerId))
			{
				View.Model.ShopDoesNotHaveAccessGivenCustomer = true;
			}
			var customer = _customerRepository.Get(customerId);
			View.Model.CustomerName = customer.ParseName(x => x.FirstName, x => x.LastName);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
		}

		public void View_Submit(object sender, SaveSubscriptionEventArgs args)
		{
			TrySaveSubscription(args);
			TryRedirect();
		}

		private void TrySaveSubscription(SaveSubscriptionEventArgs args)
		{
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			if(customerId <= 0) return;
			Func<Customer, SaveSubscriptionEventArgs, Subscription> converter = (customer,eventArgs) => new Subscription
			{
				CreatedDate = DateTime.Now,
				Customer = customer,
				PaymentInfo = new SubscriptionPaymentInfo
				{
					AccountNumber = eventArgs.AccountNumber, 
					ClearingNumber = eventArgs.ClearingNumber, 
					MonthlyAmount = eventArgs.MonthlyAmount.ToDecimalOrDefault()
				},
				Active = false,
				Notes = eventArgs.Notes
			};
			var customerToSave = _customerRepository.Get(customerId);
			var subscriptionToSave = converter.Invoke(customerToSave,args);
			_subscriptionRepository.Save(subscriptionToSave);
		}

		private bool ShopHasAccessToCustomer(int customerId)
		{
			var shop = _customerRepository.Get(customerId).Shop;
			return _synologenMemberService.GetCurrentShopId().Equals(shop.Id);
		}

		private void TryRedirect()
		{
			if(View.RedirectOnSavePageId <= 0)
			{
				var currentpage = HttpContext.Request.Url.PathAndQuery;
				HttpContext.Response.Redirect(currentpage);
				return;
			}
			var redirectPageUrl = _routingService.GetPageUrl(View.RedirectOnSavePageId);
			if(String.IsNullOrEmpty(redirectPageUrl)) return;
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			HttpContext.Response.Redirect(String.Concat(redirectPageUrl, "?customer=", customerId));
		}
	}
}