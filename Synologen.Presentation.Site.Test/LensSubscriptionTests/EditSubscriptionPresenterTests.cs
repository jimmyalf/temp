using System;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Subscription=Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;

		private readonly int _subscriptionId;
		private readonly int _returnUrlPageId;
		private readonly string _expectedReturnUrl;

		public When_loading_edit_subscription_view()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			const string url = "/test/url/";
			_returnUrlPageId = 99;
			_expectedReturnUrl = String.Format("{0}?{1}", url, customerId);
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));

			Context = () =>
			{
				MockedView.SetupGet(x => x.ReturnPageId).Returns(_returnUrlPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.Is<int>(pageId => pageId.Equals(_returnUrlPageId)))).Returns(url);
			};
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.ActivatedDate.ShouldBe(_expectedSubscription.ActivatedDate.Value.ToString("yyyy-MM-dd"));
			view.Model.CreatedDate.ShouldBe(_expectedSubscription.CreatedDate.ToString("yyyy-MM-dd"));
			view.Model.CustomerName.ShouldBe(_expectedSubscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
			view.Model.AccountNumber.ShouldBe(_expectedSubscription.PaymentInfo.AccountNumber);
			view.Model.ClearingNumber.ShouldBe(_expectedSubscription.PaymentInfo.ClearingNumber);
			view.Model.MonthlyAmount.ShouldBe(_expectedSubscription.PaymentInfo.MonthlyAmount);
			view.Model.Status.ShouldBe(_expectedSubscription.Status.GetEnumDisplayName());
			view.Model.Status.ShouldBe("Aktiv");
			view.Model.StopButtonEnabled.ShouldBe(true);
			view.Model.StartButtonEnabled.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			view.Model.DisplayForm.ShouldBe(true);
			view.Model.ReturnUrl.ShouldBe(_expectedReturnUrl);
		}

		[Test]
		public void Presenter_should_ask_for_expected_subscription_shop_id_and_access()
		{
			MockedSubscriptionRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_subscriptionId))));
			MockedSynologenMemberService.Verify(x => x.GetCurrentShopId());
			MockedSynologenMemberService.Verify(x => x.ShopHasAccessTo(It.Is<ShopAccess>( access => access.Equals(ShopAccess.LensSubscription))));
		}

		[Test]
		public void Presenter_fetches_return_url_from_synologen_member_service()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_returnUrlPageId))));
		}
	}

		[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_no_set_return_page_id : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;

		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_no_set_return_page_id()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));

			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.ReturnUrl.ShouldBe("#");
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_stopped_subscription : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_stopped_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), SubscriptionStatus.Stopped);
			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};
			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.Status.ShouldBe("Stoppad");
			view.Model.StopButtonEnabled.ShouldBe(false);
			view.Model.StartButtonEnabled.ShouldBe(true);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_created_subscription : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_created_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), SubscriptionStatus.Created);
			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.Status.ShouldBe("Skapad");
			view.Model.StopButtonEnabled.ShouldBe(false);
			view.Model.StartButtonEnabled.ShouldBe(false);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_expired_subscription : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_expired_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), SubscriptionStatus.Expired);
			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			//Act
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.Status.ShouldBe("Utg�ngen");
			view.Model.StopButtonEnabled.ShouldBe(false);
			view.Model.StartButtonEnabled.ShouldBe(false);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_submitting_edit_subscription_view : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;
		private readonly SaveSubscriptionEventArgs _saveEventArgs;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly string _expectedRedirectUrl;

		public When_submitting_edit_subscription_view()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId);
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_saveEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs(_expectedSubscription);
			Context = () =>
			{
				MockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, _saveEventArgs);
			};
		}

		[Test]
		public void Presenter_saves_subscription_with_expected_values()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.ActivatedDate.Value.IsSameDay(_expectedSubscription.ActivatedDate.Value))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.CreatedDate.IsSameDay(_expectedSubscription.CreatedDate))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Customer.Id.Equals(_expectedSubscription.Customer.Id))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Id.Equals(_expectedSubscription.Id))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.AccountNumber.Equals(_saveEventArgs.AccountNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.ClearingNumber.Equals(_saveEventArgs.ClearingNumber))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.MonthlyAmount.Equals(_saveEventArgs.MonthlyAmount))));
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(_expectedSubscription.Status))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			MockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_expectedRedirectUrl))));
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_stopping_subscription_view : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly string _expectedRedirectUrl;

		public When_stopping_subscription_view()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId);
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			Context = () =>
			{
				MockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());	
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_StopSubscription(null, new EventArgs());
			};
		}

		[Test]
		public void Presenter_saves_subscription_with_expected_values()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(SubscriptionStatus.Stopped))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			MockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_expectedRedirectUrl))));
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_starting_subscription_view : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly string _expectedRedirectUrl;

		public When_starting_subscription_view()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId);
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), SubscriptionStatus.Stopped);

			Context = () =>
			{
				MockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_StartSubscription(null, new EventArgs());	
			};
		}

		[Test]
		public void Presenter_saves_subscription_with_expected_values()
		{
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(SubscriptionStatus.Active))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			MockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			MockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_expectedRedirectUrl))));
		}

	}

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_submitting_edit_subscription_view_with_no_set_redirect_on_save_page_id : SubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;
		private readonly SaveSubscriptionEventArgs _saveEventArgs;
		private readonly string _currentPageUrl;

		public When_submitting_edit_subscription_view_with_no_set_redirect_on_save_page_id()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_currentPageUrl = "/test/redirect/";
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_saveEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs(_expectedSubscription);

			Context = () =>
			{
				MockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(0);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext
					.SetupSingleQuery("subscription", _subscriptionId.ToString())
					.SetupRelativePathAndQuery(_currentPageUrl);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, _saveEventArgs);
			};
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			MockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_currentPageUrl))));
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_customer_belonging_to_another_shop : SubscriptionTestbase
	{
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_customer_belonging_to_another_shop()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			var subscriptions = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));

			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscriptions);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId + 1);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(true);
			view.Model.DisplayForm.ShouldBe(false);
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_shop_not_having_lens_subscription_access : SubscriptionTestbase
	{
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_shop_not_having_lens_subscription_access()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId)));
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(true);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			view.Model.DisplayForm.ShouldBe(false);
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_a_non_existing_subscription : SubscriptionTestbase
	{
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_a_non_existing_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int shopId = 3;

			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((Subscription) null);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = MockedView.Object;
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			view.Model.SubscriptionDoesNotExist.ShouldBe(true);
			view.Model.DisplayForm.ShouldBe(false);
		}
	}
}