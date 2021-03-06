using System;
using System.Linq;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.MockHelpers;
using Customer = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_loading_edit_customer_view : EditCustomerTestbase
	{
		private readonly int _customerId;
		private readonly Customer _expectedCustomer;
		private readonly string _editPageUrl;
		private readonly string _createPageUrl;

		public When_loading_edit_customer_view()
		{
			// Arrange
			
			const int shopId = 5;
			const int swedenCountryId = 1;
			const int editSubscriptionPageId = 55;
			const int createSubscriptionPageId = 155;
			_customerId = 5;
			_editPageUrl = "/testPage/edit/";
			_createPageUrl = "/testPage/create/";
			_expectedCustomer = CustomerFactory.Get(_customerId, swedenCountryId, shopId);
			Context = () =>
			{
				A.CallTo(() => View.EditSubscriptionPageId).Returns(editSubscriptionPageId);
				A.CallTo(() => View.CreateSubscriptionPageId).Returns(createSubscriptionPageId);
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				A.CallTo(() => RoutingService.GetPageUrl(createSubscriptionPageId)).Returns(_createPageUrl);
				A.CallTo(() => RoutingService.GetPageUrl(editSubscriptionPageId)).Returns(_editPageUrl);
				HttpContext.SetupRequestParameter("customer", _customerId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.AddressLineOne.ShouldBe(_expectedCustomer.Address.AddressLineOne);
			View.Model.AddressLineTwo.ShouldBe(_expectedCustomer.Address.AddressLineTwo);
			View.Model.City.ShouldBe(_expectedCustomer.Address.City);
			View.Model.CountryId.ShouldBe(_expectedCustomer.Address.Country.Id);
			
			View.Model.Email.ShouldBe(_expectedCustomer.Contact.Email);
			View.Model.FirstName.ShouldBe(_expectedCustomer.FirstName);
			View.Model.LastName.ShouldBe(_expectedCustomer.LastName);
			
			View.Model.MobilePhone.ShouldBe(_expectedCustomer.Contact.MobilePhone);
			View.Model.PersonalIdNumber.ShouldBe(_expectedCustomer.PersonalIdNumber);
			View.Model.Phone.ShouldBe(_expectedCustomer.Contact.Phone);
			View.Model.PostalCode.ShouldBe(_expectedCustomer.Address.PostalCode);
			View.Model.Notes.ShouldBe(_expectedCustomer.Notes);

			View.Model.Subscriptions.Count().ShouldBe(4);
			for (var i = 0; i < _expectedCustomer.Subscriptions.Count(); i++)
			{
				View.Model.Subscriptions.ToArray()[i].CreatedDate.Equals(_expectedCustomer.Subscriptions.ToArray()[i].CreatedDate);
				View.Model.Subscriptions.ToArray()[i].Status.Equals(_expectedCustomer.Subscriptions.ToArray()[i].Active ? SubscriptionStatus.Started.GetEnumDisplayName() : SubscriptionStatus.Stopped.GetEnumDisplayName());
				View.Model.Subscriptions.ToArray()[i].EditSubscriptionPageUrl.ShouldBe(_editPageUrl + "?subscription=" + _expectedCustomer.Subscriptions.ToArray()[i].Id);
			}

			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			View.Model.DisplayForm.ShouldBe(true);
			View.Model.CreateSubscriptionPageUrl.ShouldBe(String.Concat(_createPageUrl, "?customer=", _customerId));
		}

		
		[Test]
		public void Presenter_should_ask_for_expected_customer_shop_id_and_access()
		{
			MockedCustomerRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_customerId))));
			MockedSynologenMemberService.Verify(x => x.GetCurrentShopId());
			MockedSynologenMemberService.Verify(x => x.ShopHasAccessTo(It.Is<ShopAccess>(access => access.Equals(ShopAccess.LensSubscription))));
		}

	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_submitting_edit_customer_view : EditCustomerTestbase
	{
		private readonly Customer _expectedCustomer;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;

		public When_submitting_edit_customer_view()
		{
			// Arrange
			const int customerId = 12;
			const int shopId = 6;
			const int swedenCountryId = 1;
			_redirectPageId = 22;
			_redirectUrl = "/test/redirect/";
			_expectedCustomer = CustomerFactory.Get(customerId, swedenCountryId, shopId);
			var saveEventArgs = CustomerFactory.GetSaveCustomerEventArgs(_expectedCustomer);
			Context = () =>
			{
				A.CallTo(() => View.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedCustomer);
				MockedCountryRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CountryFactory.Get(swedenCountryId));
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_redirectUrl);
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, saveEventArgs);
			};
		}

		[Test]
		public void Presenter_saves_customer_with_expected_values()
		{
				MockedCustomerRepository.Verify(x => x.Save(It.Is<Customer>( customer => 
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
		public void Presenter_get_expected_page_url_for_redirect()
		{
			//MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>(pageId => pageId.Equals(_redirectPageId))));
			A.CallTo(() => RoutingService.GetPageUrl(_redirectPageId)).MustHaveHappened();
		}

		[Test]
		public void Presenter_Perfoms_redirect()
		{
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_redirectUrl);
		}
	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_submitting_edit_customer_view_with_no_set_redirect_on_save_page_id : EditCustomerTestbase
	{
		private readonly string _currentPageUrl;

		public When_submitting_edit_customer_view_with_no_set_redirect_on_save_page_id()
		{
			const int customerId = 12;
			const int shopId = 6;
			const int swedenCountryId = 1;
			const int noRedirectPageId = 0;
			_currentPageUrl = "/test/redirect/"; 
			var expectedCustomer = CustomerFactory.Get(customerId, swedenCountryId, shopId);
			var saveEventArgs = CustomerFactory.GetSaveCustomerEventArgs(expectedCustomer);
			var sweden = CountryFactory.Get(swedenCountryId);

			Context = () =>
			{
				A.CallTo(() => View.RedirectOnSavePageId).Returns(noRedirectPageId);
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(expectedCustomer);
				MockedCountryRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(sweden);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_currentPageUrl);
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
				HttpContext.SetupVirtualPathAndQuery(_currentPageUrl);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, saveEventArgs);
			};
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_currentPageUrl);
		}
	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_loading_edit_customer_view_with_customer_belonging_to_another_shop : EditCustomerTestbase
	{
		public When_loading_edit_customer_view_with_customer_belonging_to_another_shop()
		{
			const int shopId = 5;
			const int swedenCountryId = 1;
			const int customerId = 5;
			var expectedCustomer = CustomerFactory.Get(customerId, swedenCountryId, shopId);

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(expectedCustomer);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId + 1); // Returns another shop id
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
			
		}
		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(true);
			View.Model.DisplayForm.ShouldBe(false);	
		}
	}

	[TestFixture]
	[Category("EditCustomerPresenterTester")]
	public class When_loading_edit_customer_view_with_with_shop_not_having_lens_subscription_access : EditCustomerTestbase
	{
		public When_loading_edit_customer_view_with_with_shop_not_having_lens_subscription_access()
		{
			const int shopId = 5;
			const int swedenCountryId = 1;
			const int customerId = 5;
			var expectedCustomer = CustomerFactory.Get(customerId, swedenCountryId, shopId);

			Context = () =>
			{
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(expectedCustomer);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(true);
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			View.Model.DisplayForm.ShouldBe(false);	
		}
	}
}