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
			_expectedSubscription = SubscriptionFactory.Get(CustomerFactory.Get(1));
			_mockedView = MvpHelpers.GetMockedView<IEditLensSubscriptionView, EditLensSubscriptionModel>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(SubscriptionFactory.Get(CustomerFactory.Get(customerId, shopId)));
			_mockedSynologenMemberService = new Mock<ISynologenMemberService>();
			_mockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(shopId);
			_mockedSynologenMemberService.Setup(x => x.ShopHasAccessTo(ShopAccess.LensSubscription)).Returns(true);
			_mockedHttpContext = MvpHelpers.GetMockedHttpContext().SetupSingleQuery("subscription", _subscriptionId.ToString());
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
}