﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers;
using Customer=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Customer;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class  When_loading_create_customer_view_with_shop_having_lens_subscription_access : CreateCustomerTestbase
	{
		private readonly IList<Country> _countryList;

		public When_loading_create_customer_view_with_shop_having_lens_subscription_access()
		{
			_countryList = CountryFactory.GetList().ToList();

			Context = () =>
			{
				MockedCountryRepository.Setup(x => x.GetAll()).Returns(_countryList);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Country_list_is_populated()
		{
			AssertUsing( view =>
			{
				view.Model.List.Count().ShouldBe(_countryList.Count);
				view.Model.List.For((index, country) =>
				{
					country.Text.ShouldBe(_countryList.ElementAt(index).Name);
					country.Value.ShouldBe(_countryList.ElementAt(index).Id.ToString());
				});
			});
		}

		[Test]
		public void Form_is_displayed()
		{
			AssertUsing( view =>
			{
				view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
				view.Model.DisplayForm.ShouldBe(true);
			});
		}
	}

	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_loading_create_customer_view_with_shop_not_having_lens_subscription_access : CreateCustomerTestbase
	{
		public When_loading_create_customer_view_with_shop_not_having_lens_subscription_access()
		{
			// Arrange
			const int shopId = 5;
			var shop = ShopFactory.Get(shopId);

			Context = () =>
			{
				MockedShopRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(shop);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Shop_does_not_have_access_to_lens_subscription_message_should_be_displayed()
		{
			AssertUsing( view =>
			{
				view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(true);
				view.Model.DisplayForm.ShouldBe(false);	
			});
		}
	}

	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_submitting_create_customer_view : CreateCustomerTestbase
	{
		private readonly SaveCustomerEventArgs _saveEventArgs;
		private readonly IList<Country> _countryList;
		private readonly Country _selectedCountry;
		private readonly int _shopId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;

		public When_submitting_create_customer_view()
		{
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_shopId = 5;
			const int selectedCountryId = 5;
			var shop = ShopFactory.Get(_shopId);
			_countryList = CountryFactory.GetList().ToList();
			_selectedCountry = CountryFactory.Get(selectedCountryId);

			_saveEventArgs = CustomerFactory.GetSaveCustomerEventArgs(selectedCountryId);
			Context = () =>
			{
				MockedCountryRepository.Setup(x => x.GetAll()).Returns(_countryList);
				MockedCountryRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_selectedCountry);
				MockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedShopRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(shop);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(_shopId);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, _saveEventArgs);
			};
		}

		[Test]
		public void Customer_with_expected_values_is_saved()
		{
			MockedCustomerRepository.Verify(x => x.Save(It.Is<Customer>(customer => 
				customer.FirstName.Equals(_saveEventArgs.FirstName) && 
				customer.LastName.Equals(_saveEventArgs.LastName) && 
				customer.Address.AddressLineOne.Equals(_saveEventArgs.AddressLineOne) && 
				customer.Address.AddressLineTwo.Equals(_saveEventArgs.AddressLineTwo) && 
				customer.Address.City.Equals(_saveEventArgs.City) && 
				customer.Address.Country.Id.Equals(_saveEventArgs.CountryId) && 
				customer.Contact.Email.Equals(_saveEventArgs.Email) && 
				customer.Contact.MobilePhone.Equals(_saveEventArgs.MobilePhone) && 
				customer.PersonalIdNumber.Equals(_saveEventArgs.PersonalIdNumber) && 
				customer.Contact.Phone.Equals(_saveEventArgs.Phone) && 
				customer.Address.PostalCode.Equals(_saveEventArgs.PostalCode) && 
				customer.Shop.Id.Equals(_shopId) && 
				customer.Notes.Equals(_saveEventArgs.Notes)
			)));
		}

		[Test]
		public void Presenter_fetches_expected_country_and_shop()
		{
			MockedCountryRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_selectedCountry.Id))));
			MockedShopRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_shopId))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			MockedHttpContext.VerifyRedirect(_redirectUrl);
		}
	}

}
