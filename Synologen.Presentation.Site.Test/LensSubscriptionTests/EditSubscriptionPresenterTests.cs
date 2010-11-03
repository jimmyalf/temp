using System;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
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
		}

		[Test]
		public void Presenter_should_ask_for_expected_subscription_shop_id_and_access()
		{
			_mockedSubscriptionRepository.Verify(x => x.Get(It.Is<int>(id => id.Equals(_subscriptionId))));
			_mockedSynologenMemberService.Verify(x => x.GetCurrentShopId());
			_mockedSynologenMemberService.Verify(x => x.ShopHasAccessTo(It.Is<ShopAccess>( access => access.Equals(ShopAccess.LensSubscription))));
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_stopped_subscription 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_stopped_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_expectedSubscription.Status = SubscriptionStatus.Stopped;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.Status.ShouldBe("Stoppad");
			view.Model.StopButtonEnabled.ShouldBe(false);
			view.Model.StartButtonEnabled.ShouldBe(true);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_created_subscription 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_created_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_expectedSubscription.Status = SubscriptionStatus.Created;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.Status.ShouldBe("Skapad");
			view.Model.StopButtonEnabled.ShouldBe(false);
			view.Model.StartButtonEnabled.ShouldBe(false);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_expired_subscription 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_expired_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_expectedSubscription.Status = SubscriptionStatus.Expired;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.Status.ShouldBe("Utgången");
			view.Model.StopButtonEnabled.ShouldBe(false);
			view.Model.StartButtonEnabled.ShouldBe(false);
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_submitting_edit_subscription_view 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly HttpContextMock _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
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
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};
			_saveEventArgs = new SaveSubscriptionEventArgs
			{
				AccountNumber = _expectedSubscription.PaymentInfo.AccountNumber.Reverse(),
				ClearingNumber = _expectedSubscription.PaymentInfo.ClearingNumber.Reverse(),
				MonthlyAmount = _expectedSubscription.PaymentInfo.MonthlyAmount + 255.21M
			};


			//Act
			presenter.View_Submit(null, _saveEventArgs);
		}

		[Test]
		public void Presenter_saves_subscription_with_expected_values()
		{
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.ActivatedDate.Value.IsSameDay(_expectedSubscription.ActivatedDate.Value))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.CreatedDate.IsSameDay(_expectedSubscription.CreatedDate))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Customer.Id.Equals(_expectedSubscription.Customer.Id))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Id.Equals(_expectedSubscription.Id))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.AccountNumber.Equals(_saveEventArgs.AccountNumber))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.ClearingNumber.Equals(_saveEventArgs.ClearingNumber))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.PaymentInfo.MonthlyAmount.Equals(_saveEventArgs.MonthlyAmount))));
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(_expectedSubscription.Status))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			_mockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			_mockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_expectedRedirectUrl))));
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_stopping_subscription_view 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly HttpContextMock _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private string _expectedRedirectUrl;

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
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_StopSubscription(null, new EventArgs());
		}

		[Test]
		public void Presenter_saves_subscription_with_expected_values()
		{
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(SubscriptionStatus.Stopped))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			_mockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			_mockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_expectedRedirectUrl))));
		}

	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_starting_subscription_view 
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly HttpContextMock _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;
		private readonly string _redirectUrl;
		private readonly int _redirectPageId;
		private string _expectedRedirectUrl;

		public When_starting_subscription_view()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_redirectPageId = 55;
			_redirectUrl = "/test/redirect/";
			_expectedRedirectUrl = String.Concat(_redirectUrl, "?customer=", customerId);
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId));
			_expectedSubscription.Status = SubscriptionStatus.Stopped;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(_redirectPageId);
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_redirectUrl);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_StartSubscription(null, new EventArgs());
		}

		[Test]
		public void Presenter_saves_subscription_with_expected_values()
		{
			_mockedSubscriptionRepository.Verify(x => x.Save(It.Is<Subscription>(c => c.Status.Equals(SubscriptionStatus.Active))));
		}

		[Test]
		public void Presenter_get_expected_page_url_and_perfoms_redirect()
		{
			_mockedSynologenMemberService.Verify(x => x.GetPageUrl(It.Is<int>( pageId => pageId.Equals(_redirectPageId))));
			_mockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_expectedRedirectUrl))));
		}

	}

	[TestFixture]
	[Category("CreateLensSubscriptionPresenterTester")]
	public class When_submitting_edit_subscription_view_with_no_set_redirect_on_save_page_id
	{
		private readonly Subscription _expectedSubscription;
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly HttpContextMock _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
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
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedView.SetupGet(x => x.RedirectOnSavePageId).Returns(0);
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(_expectedSubscription);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = new HttpContextMock()
				.SetupSingleQuery("subscription", _subscriptionId.ToString())
				.SetupRelativePathAndQuery(_currentPageUrl);
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};
			_saveEventArgs = new SaveSubscriptionEventArgs
			{
				AccountNumber = _expectedSubscription.PaymentInfo.AccountNumber.Reverse(),
				ClearingNumber = _expectedSubscription.PaymentInfo.ClearingNumber.Reverse(),
				MonthlyAmount = _expectedSubscription.PaymentInfo.MonthlyAmount + 255.21M
			};


			//Act
			presenter.View_Submit(null, _saveEventArgs);
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			_mockedHttpContext.MockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_currentPageUrl))));
		}
	}

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_customer_belonging_to_another_shop 
	{

		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_customer_belonging_to_another_shop()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId)));
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId + 1);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
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
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_shop_not_having_lens_subscription_access
	{
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_shop_not_having_lens_subscription_access()
		{
			//Arrange
			_subscriptionId = 1;
			const int customerId = 2;
			const int shopId = 3;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId)));
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
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

	[TestFixture]
	[Category("EditLensSubscriptionPresenterTester")]
	public class When_loading_edit_subscription_view_with_a_non_existing_subscription
	{
		private readonly Mock<IEditLensSubscriptionView> _mockedView;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly Mock<HttpContextBase> _mockedHttpContext;
		private readonly Mock<ISynologenMemberService> _mockedSynologenMemberService;
		private readonly int _subscriptionId;

		public When_loading_edit_subscription_view_with_a_non_existing_subscription()
		{
			//Arrange
			_subscriptionId = 1;
			const int shopId = 3;
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((Subscription)null);
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(false);
			_mockedHttpContext = new HttpContextMock().SetupSingleQuery("subscription", _subscriptionId.ToString());
			var presenter = new EditLensSubscriptionPresenter(_mockedView.Object, _mockedSubscriptionRepository.Object, _mockedSynologenMemberService.Object){HttpContext = _mockedHttpContext.Object};

			//Act
			presenter.View_Load(null,new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			var view = _mockedView.Object;
			view.Model.ShopDoesNotHaveAccessToLensSubscriptions.ShouldBe(false);
			view.Model.ShopDoesNotHaveAccessGivenCustomer.ShouldBe(false);
			view.Model.SubscriptionDoesNotExist.ShouldBe(true);
			view.Model.DisplayForm.ShouldBe(false);
		}
	}
}