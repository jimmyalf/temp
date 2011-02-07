using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using Customer=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;
using Subscription=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class EditCustomerPresenter : Presenter<IEditCustomerView>
	{
		private readonly ICountryRepository _countryRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private const int SwedenCountryId = 1;

		public EditCustomerPresenter(IEditCustomerView view, ICustomerRepository customerRepository, ICountryRepository countryRepository, ISynologenMemberService synologenMemberService)
			: base(view)
		{
			_customerRepository = customerRepository;
			_countryRepository = countryRepository;
			_synologenMemberService = synologenMemberService;

			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var editUrl = View.EditSubscriptionPageId == 0 ? "#" : _synologenMemberService.GetPageUrl(View.EditSubscriptionPageId);
			var createUrl = View.CreateSubscriptionPageId == 0 ? "#" : _synologenMemberService.GetPageUrl(View.CreateSubscriptionPageId);
			Func<Subscription, SubscriptionListItemModel> subscriptionConverter = subscription => new SubscriptionListItemModel
			{
				CreatedDate = subscription.CreatedDate.ToString(("yyyy-MM-dd")),
				Status = subscription.Active ? SubscriptionStatus.Started.GetEnumDisplayName() : SubscriptionStatus.Stopped.GetEnumDisplayName(),
				EditSubscriptionPageUrl = String.Format("{0}?subscription={1}", editUrl, subscription.Id)
			};
			
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			var customer = _customerRepository.Get(customerId);
			CheckAccess(customer);
			View.Model.CreateSubscriptionPageUrl = String.Format("{0}?customer={1}", createUrl, customerId);
			View.Model.AddressLineOne = customer.Address.AddressLineOne;
			View.Model.AddressLineTwo = customer.Address.AddressLineTwo;
			View.Model.City = customer.Address.City;
			View.Model.CountryId = customer.Address.Country.Id;
			View.Model.Email = customer.Contact.Email;
			View.Model.FirstName = customer.FirstName;
			View.Model.LastName = customer.LastName;
			View.Model.MobilePhone = customer.Contact.MobilePhone;
			View.Model.PersonalIdNumber = customer.PersonalIdNumber;
			View.Model.Phone = customer.Contact.Phone;
			View.Model.PostalCode = customer.Address.PostalCode;
			View.Model.Subscriptions = customer.Subscriptions.Select(subscriptionConverter);
			View.Model.Notes = customer.Notes;
		}

		private void CheckAccess(Customer customer)
		{
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions = !_synologenMemberService.ShopHasAccessTo(ShopAccess.LensSubscription);
			View.Model.ShopDoesNotHaveAccessGivenCustomer = !_synologenMemberService.GetCurrentShopId().Equals(customer.Shop.Id);
		}

		public void View_Submit(object sender, SaveCustomerEventArgs args)
		{
			TrySaveCustomer(args);
			TryRedirect();
		}

		private void TrySaveCustomer(SaveCustomerEventArgs args)
		{

			var countryToUse = _countryRepository.Get(SwedenCountryId);
			var customerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
			if (customerId <= 0) return;
			var customer = _customerRepository.Get(customerId);

			customer.Address.AddressLineOne = args.AddressLineOne;
			customer.Address.AddressLineTwo = args.AddressLineTwo;
			customer.Address.City = args.City;
			customer.Address.Country = countryToUse;
			customer.Address.PostalCode = args.PostalCode;
			
			customer.Contact.Email = args.Email;
			customer.Contact.MobilePhone = args.MobilePhone;
			customer.Contact.Phone = args.Phone;

			customer.FirstName = args.FirstName;
			customer.LastName = args.LastName;
			customer.PersonalIdNumber = args.PersonalIdNumber;
			customer.Notes = args.Notes;

			_customerRepository.Save(customer);
		}

		private void TryRedirect()
		{
			if (View.RedirectOnSavePageId <= 0)
			{
				var currentpage = HttpContext.Request.Url.PathAndQuery;
				HttpContext.Response.Redirect(currentpage);
				return;
			}
			var redirectPageUrl = _synologenMemberService.GetPageUrl(View.RedirectOnSavePageId);
			if (String.IsNullOrEmpty(redirectPageUrl)) return;
			HttpContext.Response.Redirect(redirectPageUrl);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
		}
	}
}
