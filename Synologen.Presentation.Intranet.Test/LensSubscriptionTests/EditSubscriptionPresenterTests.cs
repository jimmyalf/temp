using System;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.TestHelpers;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view : EditSubscriptionTestbase
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
			_expectedReturnUrl = String.Format("{0}?customer={1}", url, customerId);
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));

			Context = () =>
			{
				A.CallTo(() => View.ReturnPageId).Returns(_returnUrlPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				HttpContext.SetupRequestParameter("subscription", _subscriptionId.ToString());
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(url);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.ActivatedDate.ShouldBe(_expectedSubscription.ActivatedDate.Value.ToString("yyyy-MM-dd"));
			View.Model.CreatedDate.ShouldBe(_expectedSubscription.CreatedDate.ToString("yyyy-MM-dd"));
			View.Model.CustomerName.ShouldBe(_expectedSubscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
			View.Model.AccountNumber.ShouldBe(_expectedSubscription.PaymentInfo.AccountNumber);
			View.Model.ClearingNumber.ShouldBe(_expectedSubscription.PaymentInfo.ClearingNumber);
			View.Model.MonthlyAmount.ShouldBe(_expectedSubscription.PaymentInfo.MonthlyAmount.ToString());
			View.Model.Status.ShouldBe(_expectedSubscription.Active ? SubscriptionStatus.Started.GetEnumDisplayName() : SubscriptionStatus.Stopped.GetEnumDisplayName());
			View.Model.Status.ShouldBe(SubscriptionStatus.Started.GetEnumDisplayName());
			View.Model.StopButtonEnabled.ShouldBe(true);
			View.Model.StartButtonEnabled.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			View.Model.DisplayForm.ShouldBe(true);
			View.Model.ReturnUrl.ShouldBe(_expectedReturnUrl);
			View.Model.ConsentStatus.ShouldBe(_expectedSubscription.ConsentStatus.GetEnumDisplayName());
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
			A.CallTo(() => RoutingService.GetPageUrl(_returnUrlPageId)).MustHaveHappened();
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_no_set_return_page_id : EditSubscriptionTestbase
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
				HttpContext.SetupRequestParameter("subscription", _subscriptionId.ToString());
			};
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.ReturnUrl.ShouldBe("#");
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_stopped_subscription : EditSubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_stopped_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), false);
			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				HttpContext.SetupRequestParameter("subscription", _subscriptionId.ToString());
			};
			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.Status.ShouldBe(SubscriptionStatus.Stopped.GetEnumDisplayName());
			View.Model.StopButtonEnabled.ShouldBe(false);
			View.Model.StartButtonEnabled.ShouldBe(true);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_created_subscription : EditSubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_created_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), false);
			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				HttpContext.SetupRequestParameter("subscription", _subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.Status.ShouldBe(SubscriptionStatus.Stopped.GetEnumDisplayName());
			View.Model.StopButtonEnabled.ShouldBe(false);
			View.Model.StartButtonEnabled.ShouldBe(true);				
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_submitting_edit_subscription_view : EditSubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		//private readonly int _subscriptionId;
		private readonly SaveSubscriptionEventArgs _saveEventArgs;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly string _expectedRedirectUrl;

		public When_submitting_edit_subscription_view()
		{
			//Arrange
			//_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId, "&subscription=",_expectedSubscription.Id);
			_saveEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs(_expectedSubscription);
			Context = () =>
			{
				A.CallTo(() => View.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_redirectUrl);
				HttpContext.SetupRequestParameter("subscription", _expectedSubscription.Id.ToString());
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
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(subscription => 
				subscription.ActivatedDate.Value.IsSameDay(_expectedSubscription.ActivatedDate.Value) && 
				subscription.CreatedDate.IsSameDay(_expectedSubscription.CreatedDate) &&
				subscription.Customer.Id.Equals(_expectedSubscription.Customer.Id) &&
				subscription.Id.Equals(_expectedSubscription.Id) &&
				subscription.PaymentInfo.AccountNumber.Equals(_saveEventArgs.AccountNumber) &&
				subscription.PaymentInfo.ClearingNumber.Equals(_saveEventArgs.ClearingNumber) &&
				subscription.PaymentInfo.MonthlyAmount.Equals(_saveEventArgs.MonthlyAmount.ToDecimal()) &&
				subscription.Active.Equals(_expectedSubscription.Active)
			)));

		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			A.CallTo(() => RoutingService.GetPageUrl(_redirectPageId)).MustHaveHappened();
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_expectedRedirectUrl);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_stopping_subscription_view : EditSubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		//private readonly int _subscriptionId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly string _expectedRedirectUrl;

		public When_stopping_subscription_view()
		{
			//Arrange
			//_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId, "&subscription=",_expectedSubscription.Id);
			
			Context = () =>
			{
				A.CallTo(() => View.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_redirectUrl);
				HttpContext.SetupRequestParameter("subscription", _expectedSubscription.Id.ToString());	
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
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Active.Equals(false))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			A.CallTo(() => RoutingService.GetPageUrl(_redirectPageId)).MustHaveHappened();
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_expectedRedirectUrl);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_starting_subscription_view : EditSubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		//private readonly int _subscriptionId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private readonly string _expectedRedirectUrl;

		public When_starting_subscription_view()
		{
			//Arrange
			//_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId), false);
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId, "&subscription=", _expectedSubscription.Id);

			Context = () =>
			{
				A.CallTo(() => View.RedirectOnSavePageId).Returns(_redirectPageId);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				A.CallTo(() => RoutingService.GetPageUrl(A<int>.Ignored)).Returns(_redirectUrl);
				HttpContext.SetupRequestParameter("subscription", _expectedSubscription.Id.ToString());
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
			MockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Active.Equals(true))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			A.CallTo(() => RoutingService.GetPageUrl(_redirectPageId)).MustHaveHappened();
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_expectedRedirectUrl);
		}

	}

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_submitting_edit_subscription_view_with_no_set_redirect_on_save_page_id : EditSubscriptionTestbase
	{
		private readonly Subscription _expectedSubscription;
		//private readonly int _subscriptionId;
		private readonly SaveSubscriptionEventArgs _saveEventArgs;
		private readonly string _currentPageUrl;

		public When_submitting_edit_subscription_view_with_no_set_redirect_on_save_page_id()
		{
			//Arrange
			//_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_currentPageUrl = "/test/redirect/";
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_saveEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs(_expectedSubscription);

			Context = () =>
			{
				A.CallTo(() => View.RedirectOnSavePageId).Returns(0);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				HttpContext.SetupRequestParameter("subscription", _expectedSubscription.Id.ToString());
				HttpContext.SetupVirtualPathAndQuery(_currentPageUrl);
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
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_currentPageUrl);
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_customer_belonging_to_another_shop : EditSubscriptionTestbase
	{
		//private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_customer_belonging_to_another_shop()
		{
			//Arrange
			//_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			var subscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));

			Context = () =>
			{
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId + 1);
				MockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
				HttpContext.SetupRequestParameter("subscription", subscription.Id.ToString());
			};

			Because = presenter => presenter.View_Load(null,new EventArgs());
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
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_shop_not_having_lens_subscription_access : EditSubscriptionTestbase
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
				HttpContext.SetupRequestParameter("subscription", _subscriptionId.ToString());
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

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_a_non_existing_subscription : EditSubscriptionTestbase
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
				HttpContext.SetupRequestParameter("subscription", _subscriptionId.ToString());
			};

			Because = presenter => presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			View.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			View.Model.SubscriptionDoesNotExist.ShouldBe(true);
			View.Model.DisplayForm.ShouldBe(false);	
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_updating_subscription_view : EditSubscriptionTestbase
	{
		private readonly SaveSubscriptionEventArgs _updateEventArgs;

		public When_updating_subscription_view()
		{
			_updateEventArgs = SubscriptionFactory.GetSaveSubscriptionEventArgs();
			Context = () => { };

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_UpdateForm(null, _updateEventArgs);
			};
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			View.Model.AccountNumber.ShouldBe(_updateEventArgs.AccountNumber);
			View.Model.ClearingNumber.ShouldBe(_updateEventArgs.ClearingNumber);
			View.Model.MonthlyAmount.ShouldBe(_updateEventArgs.MonthlyAmount);
			View.Model.Notes.ShouldBe(_updateEventArgs.Notes);
		}
	}
}