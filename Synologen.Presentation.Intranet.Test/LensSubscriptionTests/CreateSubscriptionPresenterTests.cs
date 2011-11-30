using System;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_loading_create_subscription_view : CreateSubscriptionTestbase
	{
		public When_loading_create_subscription_view()
		{
			const int customerId = 5;
			const int shopId = 5;

			Context = () =>
			{
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(customerId,shopId));
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Presenter_gets_expected_customer()
		{
			MockedCustomerRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(5))));
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.CustomerName.ShouldBe("Eva Bergstr�m");
		}

		[Test]
		public void Form_should_be_displayed()
		{
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			View.Model.DisplayForm.ShouldBe(true);
		}
	}

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_submitting_create_subscription_view : CreateSubscriptionTestbase
	{
		private readonly SaveSubscriptionEventArgs _saveEventArgs;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly int _customerId;

		public When_submitting_create_subscription_view()
		{
			//Arrange
			_customerId = 5;
			const int shopId = 5;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_saveEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs();

			Context = () =>
			{
				HttpContext.SetupRequestParameter("customer", _customerId.ToString());
				//HttpContext.SetupRequestParameter("customer", _customerId.ToString());
				A.CallTo(() => View.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(_customerId, shopId));
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, _saveEventArgs);
			};
		}

		[Test]
		public void Presenter_gets_expected_subscription()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.CreatedDate.IsSameDay(DateTime.Now))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Customer.Id.Equals(5))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.AccountNumber.Equals(_saveEventArgs.AccountNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.ClearingNumber.Equals(_saveEventArgs.ClearingNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.MonthlyAmount.Equals(_saveEventArgs.MonthlyAmount.ToDecimal()))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Active.Equals(false))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Notes.Equals(_saveEventArgs.Notes))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(string.Format("{0}?customer={1}",_redirectUrl, _customerId));
		}
	}

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_submitting_create_subscription_view_with_no_set_redirect_on_save_page_id : CreateSubscriptionTestbase
	{
		private readonly string _currentPageUrl;

		public When_submitting_create_subscription_view_with_no_set_redirect_on_save_page_id()
		{
			//Arrange
			const int customerId = 5;
			const int shopId = 5;
			const int noredirectPageId = 0;
			_currentPageUrl = "/test/redirect/";
			var saveEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs();

			Context = () =>
			{
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
				HttpContext.SetupVirtualPathAndQuery(_currentPageUrl);
				A.CallTo(() => View.Model).Returns(new CreateLensSubscriptionModel());
				A.CallTo(() => View.RedirectOnSavePageId).Returns(noredirectPageId);
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(customerId, shopId));
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
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
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_loading_create_subscription_view_with_customer_belonging_to_another_shop : CreateSubscriptionTestbase
	{
		public When_loading_create_subscription_view_with_customer_belonging_to_another_shop()
		{
			const int customerId = 5;
			const int shopId = 5;
			const int otherShopId = 4;

			Context = () => 
			{
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(customerId, otherShopId));
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			};

			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Shop_does_not_have_access_to_given_customer_message_should_be_displayed()
		{
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(true);
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			View.Model.DisplayForm.ShouldBe(false);	
		}
	}

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_loading_create_subscription_view_with_shop_not_having_lens_subscription_access : CreateSubscriptionTestbase
	{
		public When_loading_create_subscription_view_with_shop_not_having_lens_subscription_access()
		{
			const int customerId = 5;
			const int shopId = 11;

			Context = () => 
			{
				HttpContext.SetupRequestParameter("customer", customerId.ToString());
				MockedCustomerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(CustomerFactory.Get(customerId, shopId));
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			};

			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Shop_does_not_have_access_to_lens_subscriptions_message_should_be_displayed()
		{
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(true);
			View.Model.DisplayForm.ShouldBe(false);	
		}
	}
}