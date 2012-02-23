using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using WebFormsMvp;
using Customer = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription
{
	public class CreateCustomerPresenter : Presenter<ICreateCustomerView>
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IShopRepository _shopRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly IRoutingService _routingService;
		private const int SwedenCountryId = 1;

		public CreateCustomerPresenter(
			ICreateCustomerView view, 
			ICustomerRepository customerRepository, 
			IShopRepository shopRepository, 
			ICountryRepository countryRepository,
			ISynologenMemberService synologenMemberService,
			IRoutingService routingService)
			: base(view)
		{
			_shopRepository = shopRepository;
			_customerRepository = customerRepository;
			_countryRepository = countryRepository;
			_synologenMemberService = synologenMemberService;
			_routingService = routingService;
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
			if (!_synologenMemberService.ShopHasAccessTo(ShopAccess.LensSubscription))
			{
				View.Model.ShopDoesNotHaveAccessToLensSubscriptions = true;
			}
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
		}

		public void View_Submit(object o, SaveCustomerEventArgs args)
		{
			var shopToUse = _shopRepository.Get(_synologenMemberService.GetCurrentShopId());
			var countryToUse = _countryRepository.Get(SwedenCountryId);

			Func<Shop, SaveCustomerEventArgs, Customer> converter = (shop, eventArgs) => new Customer
			{
				Contact = new CustomerContact
				{
					Email = eventArgs.Email,
					MobilePhone = eventArgs.MobilePhone,
					Phone = eventArgs.Phone
				},
				Address = new CustomerAddress
				{
					AddressLineOne = eventArgs.AddressLineOne,
					AddressLineTwo = eventArgs.AddressLineTwo,
					City = eventArgs.City,
					Country = countryToUse,
					PostalCode = eventArgs.PostalCode
				},
				FirstName = eventArgs.FirstName,
				LastName = eventArgs.LastName,
				PersonalIdNumber = eventArgs.PersonalIdNumber,
				Shop = shop,
				Notes = eventArgs.Notes
			};

			var customerToSave = converter.Invoke(shopToUse, args);
			_customerRepository.Save(customerToSave);
			TryRedirect();
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
			HttpContext.Response.Redirect(redirectPageUrl);
		}
	}
}
