using System;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers;
using Customer=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_loading_edit_customer_view
	{
		private readonly int _customerId;
		private readonly Country[] _countryList;
		private readonly Customer _expectedCustomer;
		private readonly Mock<IEditCustomerView> _mockedView;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;
		private readonly Mock<ICountryRepository> _mockedCountryRepository;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly string _editPageUrl;
		private readonly string _createPageUrl;

		public When_loading_edit_customer_view()
		{
			// Arrange
			
			const int shopId = 5;
			const int countryId = 1;
			const int editSubscriptionPageId = 55;
			const int createSubscriptionPageId = 155;
			_customerId = 5;
			_editPageUrl = "/testPage/edit/";
			_createPageUrl = "/testPage/create/";

			_expectedCustomer = CustomerFactory.Get(_customerId, countryId, shopId);

			_mockedView = MvpHelpers.GetMockedView<IEditCustomerView, EditCustomerModel>();
			_mockedView.SetupGet(x => x.EditSubscriptionPageId).Returns(editSubscriptionPageId);
			_mockedView.SetupGet(x => x.CreateSubscriptionPageId).Returns(createSubscriptionPageId);

			Func<Country, CountryListItemModel> countryConverter = (country) => new CountryListItemModel { Value = country.Id.ToString(), Text = country.Name };
			_countryList = CountryFactory.GetList().ToArray();
			 _mockedView.SetupGet(x => x.Model).Returns(new EditCustomerModel { List = _countryList.Select(countryConverter) });
		
			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);
			
			_mockedCountryRepository = new Mock<ICountryRepository>();
			_mockedCountryRepository.Setup(x => x.GetAll()).Returns(_countryList);

			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.Is<int>(id => id.Equals(createSubscriptionPageId)))).Returns(_createPageUrl);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.Is<int>(id => id.Equals(editSubscriptionPageId)))).Returns(_editPageUrl);
			
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("customer", _customerId.ToString());

			var presenter = new EditCustomerPresenter(
													_mockedView.Object,
													_mockedCustomerRepository.Object,
													_mockedCountryRepository.Object,
													_mockedSynologenMemberService.Object) { HttpContext = _mockedHttpContext.Object};

			// Act
			presenter.View_Load(null, new EventArgs());
			
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.AddressLineOne.ShouldBe(_expectedCustomer.Address.AddressLineOne);
			view.Model.AddressLineTwo.ShouldBe(_expectedCustomer.Address.AddressLineTwo);
			view.Model.City.ShouldBe(_expectedCustomer.Address.City);
			view.Model.CountryId.ShouldBe(_expectedCustomer.Address.Country.Id);

			view.Model.Email.ShouldBe(_expectedCustomer.Contact.Email);
			view.Model.FirstName.ShouldBe(_expectedCustomer.FirstName);
			view.Model.LastName.ShouldBe(_expectedCustomer.LastName);

			view.Model.MobilePhone.ShouldBe(_expectedCustomer.Contact.MobilePhone);
			view.Model.PersonalIdNumber.ShouldBe(_expectedCustomer.PersonalIdNumber);
			view.Model.Phone.ShouldBe(_expectedCustomer.Contact.Phone);
			view.Model.PostalCode.ShouldBe(_expectedCustomer.Address.PostalCode);
			view.Model.Notes.ShouldBe(_expectedCustomer.Notes);

			view.Model.List.Count().ShouldBe(4);
			view.Model.Subscriptions.Count().ShouldBe(4);
			for (var i = 0; i < _expectedCustomer.Subscriptions.Count(); i++)
			{
				view.Model.Subscriptions.ToArray()[i].CreatedDate.Equals(_expectedCustomer.Subscriptions.ToArray()[i].CreatedDate);
				view.Model.Subscriptions.ToArray()[i].Status.Equals(_expectedCustomer.Subscriptions.ToArray()[i].Status);
				view.Model.Subscriptions.ToArray()[i].EditSubscriptionPageUrl.ShouldBe(_editPageUrl + "?subscription=" + _expectedCustomer.Subscriptions.ToArray()[i].Id);
			}


			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			view.Model.DisplayForm.ShouldBe(true);
			view.Model.CreateSubscriptionPageUrl.ShouldBe(String.Concat(_createPageUrl, "?customer=", _customerId));
		}

		
		[Test]
		public void Presenter_should_ask_for_expected_customer_shop_id_and_access()
		{
			_mockedCustomerRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_customerId))));
			_mockedSynologenMemberService.Verify(x => x.GetCurrentShopId());
			_mockedSynologenMemberService.Verify(x => x.ShopHasAccessTo(It.Is<ShopAccess>(access => access.Equals(ShopAccess.LensSubscription))));
		}

	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_submitting_edit_customer_view
	{
		private readonly Customer _expectedCustomer;
		private readonly Mock<IEditCustomerView> _mockedView;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;
		private readonly Mock<ICountryRepository> _mockedCountryRepository;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly HttpContextMock _mockedHttpContext;
		private readonly int _customerId;
		private readonly SaveCustomerEventArgs _saveEventArgs;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;

		public When_submitting_edit_customer_view()
		{
			// Arrange
			_customerId = 12;
			const int shopId = 6;
			const int countryId = 1;
			_redirectPageId = 22;
			_redirectUrl = "/test/redirect/";
			_expectedCustomer = CustomerFactory.Get(_customerId, countryId, shopId);
			_mockedView = MvpHelpers.GetMockedView<IEditCustomerView, EditCustomerModel>();
			_mockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);

			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);

			_mockedCountryRepository = new Mock<ICountryRepository>();
			_mockedCountryRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CountryFactory.Get(countryId));

			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);

			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("customer", _customerId.ToString());
			var presenter = new EditCustomerPresenter(_mockedView.Object, _mockedCustomerRepository.Object, _mockedCountryRepository.Object, _mockedSynologenMemberService.Object) { HttpContext = _mockedHttpContext.Object };
			_saveEventArgs = new SaveCustomerEventArgs
			{
				AddressLineOne = _expectedCustomer.Address.AddressLineOne,
				AddressLineTwo = _expectedCustomer.Address.AddressLineTwo,
				City = _expectedCustomer.Address.City,
				CountryId = _expectedCustomer.Address.Country.Id,
				Email = _expectedCustomer.Contact.Email,
				FirstName = _expectedCustomer.FirstName,
				LastName = _expectedCustomer.LastName,
				MobilePhone = _expectedCustomer.Contact.MobilePhone,
				PersonalIdNumber = _expectedCustomer.PersonalIdNumber,
				Phone = _expectedCustomer.Contact.Phone,
				Notes = _expectedCustomer.Notes
			};

			// Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_Submit(null, _saveEventArgs);
		}

		[Test]
		public void Presenter_saves_customer_with_expected_values()
		{
			_mockedCustomerRepository.Verify(x => x.Save(It.Is<Customer>( customer => 
				customer.Address.AddressLineOne.Equals(_expectedCustomer.Address.AddressLineOne) &&
				customer.Address.AddressLineTwo.Equals(_expectedCustomer.Address.AddressLineTwo) && 
				customer.Address.City.Equals(_expectedCustomer.Address.City) &&
				customer.Address.PostalCode.Equals(_expectedCustomer.Address.PostalCode) &&
				customer.Address.Country.Id.Equals(_expectedCustomer.Address.Country.Id) &&
				customer.Contact.Email.Equals(_expectedCustomer.Contact.Email) &&
				customer.Contact.MobilePhone.Equals(_expectedCustomer.Contact.MobilePhone) &&
				customer.Contact.Phone.Equals(_expectedCustomer.Contact.Phone) &&
				customer.FirstName.Equals(_expectedCustomer.FirstName) &&
				customer.LastName.Equals(_expectedCustomer.LastName) &&
				customer.PersonalIdNumber.Equals(_expectedCustomer.PersonalIdNumber) &&
				customer.Notes.Equals(_expectedCustomer.Notes) &&
				customer.Shop.Id.Equals(_expectedCustomer.Shop.Id)
			 )));

		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			_mockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>(pageId => pageId.Equals(_redirectPageId))));
			_mockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_redirectUrl))));
		}
	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_submitting_edit_customer_view_with_no_set_redirect_on_save_page_id
	{
		private readonly Customer _expectedCustomer;
		private readonly Mock<IEditCustomerView> _mockedView;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;
		private readonly Mock<ICountryRepository> _mockedCountryRepository;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly HttpContextMock _mockedHttpContext;
		private readonly int _customerId;
		private readonly SaveCustomerEventArgs _saveEventArgs;
		private readonly string _currentPageUrl;
		private readonly int _redirectPageId;

		public When_submitting_edit_customer_view_with_no_set_redirect_on_save_page_id()
		{
			_customerId = 12;
			const int shopId = 6;
			const int countryId = 1;
			_redirectPageId = 0;	// No redirect page id
			_currentPageUrl = "/test/redirect/"; 
			_expectedCustomer = CustomerFactory.Get(_customerId, countryId, shopId);
			_mockedView = MvpHelpers.GetMockedView<IEditCustomerView, EditCustomerModel>();
			_mockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);

			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);

			_mockedCountryRepository = new Mock<ICountryRepository>();
			_mockedCountryRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CountryFactory.Get(countryId));

			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_currentPageUrl);

			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("customer", _customerId.ToString()).SetupRelativePathAndQuery(_currentPageUrl);
			var presenter = new EditCustomerPresenter(_mockedView.Object, _mockedCustomerRepository.Object, _mockedCountryRepository.Object, _mockedSynologenMemberService.Object) { HttpContext = _mockedHttpContext.Object };
			_saveEventArgs = new SaveCustomerEventArgs
			{
				AddressLineOne = _expectedCustomer.Address.AddressLineOne,
				AddressLineTwo = _expectedCustomer.Address.AddressLineTwo,
				City = _expectedCustomer.Address.City,
				CountryId = _expectedCustomer.Address.Country.Id,
				Email = _expectedCustomer.Contact.Email,
				FirstName = _expectedCustomer.FirstName,
				LastName = _expectedCustomer.LastName,
				MobilePhone = _expectedCustomer.Contact.MobilePhone,
				PersonalIdNumber = _expectedCustomer.PersonalIdNumber,
				Phone = _expectedCustomer.Contact.Phone,
				Notes = _expectedCustomer.Notes
			};

			// Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_Submit(null, _saveEventArgs);
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			_mockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_currentPageUrl))));
		}
	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_loading_edit_customer_view_with_customer_belonging_to_another_shop
	{

		private const int _customerId = 5;
		private readonly Customer _expectedCustomer;
		private readonly Mock<IEditCustomerView> _mockedView;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;
		private readonly Mock<ICountryRepository> _mockedCountryRepository;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly Mock<HttpContextBase> _mockedHttpContext;

		public When_loading_edit_customer_view_with_customer_belonging_to_another_shop()
		{
			//Arrange
			
			const int shopId = 5;
			const int countryId = 1;
			
			_mockedView = MvpHelpers.GetMockedView<IEditCustomerView, EditCustomerModel>();
			_expectedCustomer = CustomerFactory.Get(_customerId, countryId, shopId);
			
			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);
			
			_mockedCountryRepository = new Mock<ICountryRepository>();

			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId + 1);  // Returns another shop id
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("customer", _customerId.ToString());

			var presenter = new EditCustomerPresenter(
													_mockedView.Object,
													_mockedCustomerRepository.Object,
													_mockedCountryRepository.Object,
													_mockedSynologenMemberService.Object) { HttpContext = _mockedHttpContext.Object};
		

			//Act
			presenter.View_Load(null, new EventArgs());
			
		}
		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(true);
			view.Model.DisplayForm.ShouldBe(false);
		}
	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_loading_edit_customer_view_with_with_shop_not_having_lens_subscription_access
	{

		private const int _customerId = 5;
		private readonly Customer _expectedCustomer;
		private readonly Mock<IEditCustomerView> _mockedView;
		private readonly Mock<ICustomerRepository> _mockedCustomerRepository;
		private readonly Mock<ICountryRepository> _mockedCountryRepository;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly Mock<HttpContextBase> _mockedHttpContext;

		public When_loading_edit_customer_view_with_with_shop_not_having_lens_subscription_access()
		{
			//Arrange

			const int shopId = 5;
			const int countryId = 1;

			_mockedView = MvpHelpers.GetMockedView<IEditCustomerView, EditCustomerModel>();
			_expectedCustomer = CustomerFactory.Get(_customerId, countryId, shopId);

			_mockedCustomerRepository = new Mock<ICustomerRepository>();
			_mockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);

			_mockedCountryRepository = new Mock<ICountryRepository>();

			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false); // Return no lenssubcription access

			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("customer", _customerId.ToString());

			var presenter = new EditCustomerPresenter(
													_mockedView.Object,
													_mockedCustomerRepository.Object,
													_mockedCountryRepository.Object,
													_mockedSynologenMemberService.Object) { HttpContext = _mockedHttpContext.Object };


			//Act
			presenter.View_Load(null, new EventArgs());

		}
		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(true);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			view.Model.DisplayForm.ShouldBe(false);
		}
	}

}
