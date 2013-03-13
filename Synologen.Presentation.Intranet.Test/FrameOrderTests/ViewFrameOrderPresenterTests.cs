using System;
using System.Collections.Specialized;
using System.Web;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.FrameOrderTests
{
	[TestFixture, Category("ViewFrameOrderPresenterTests")]
	public class Given_a_FrameOrderViewPresenter : AssertionHelper
	{
		private ViewFrameOrderPresenter _presenter;
		private IViewFrameOrderView<ViewFrameOrderModel> _view;
		private IFrameOrderRepository _frameOrderRepository;
		private ISynologenMemberService _synologenMemberService;
		private IFrameOrderService _frameOrderService;
		private IRoutingService _routingService;

		[SetUp]
		public void Context()
		{
			_frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			_synologenMemberService = ServiceFactory.GetSynologenMemberService();
			_frameOrderService = ServiceFactory.GetFrameOrderSettingsService();
			_view = A.Fake<IViewFrameOrderView<ViewFrameOrderModel>>();
			_routingService = A.Fake<IRoutingService>();
				//ViewsFactory.GetViewFrameOrderView();
			_presenter = new ViewFrameOrderPresenter(_view, _frameOrderRepository, _synologenMemberService, _frameOrderService, _routingService);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			var expectedFrameOrder = _frameOrderRepository.Get(5);
			const int expectedShopId = 5;
			const string expectedEditRedirectUrl = "/test/url/";
			var expectedEditRedirectUrlWithQueryString = String.Concat(expectedEditRedirectUrl, "?frameorder=", 5);
			

			//Act
			A.CallTo(() => _routingService.GetPageUrl(A<int>.Ignored)).Returns(expectedEditRedirectUrl);
			//((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedPageUrl(expectedEditRedirectUrl);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetShopHasAccess(true);
			_presenter.HttpContext = httpContext.Object;
			_presenter.View.EditPageId = 1;
			_presenter.View_Load(null, eventArgs);

			//Assert

			Expect(_view.Model.AdditionLeft, Is.EqualTo(expectedFrameOrder.Addition.Left));
			Expect(_view.Model.AdditionRight, Is.EqualTo(expectedFrameOrder.Addition.Right));
			Expect(_view.Model.AxisSelectionLeft, Is.EqualTo(expectedFrameOrder.Axis.Left));
			Expect(_view.Model.AxisSelectionRight, Is.EqualTo(expectedFrameOrder.Axis.Right));
			Expect(_view.Model.CreatedDate, Is.EqualTo(expectedFrameOrder.Created.ToString("yyyy-MM-dd HH:mm")));
			Expect(_view.Model.CylinderLeft, Is.EqualTo(expectedFrameOrder.Cylinder.Left));
			Expect(_view.Model.CylinderRight, Is.EqualTo(expectedFrameOrder.Cylinder.Right));
			Expect(_view.Model.DisplayOrder, Is.True);
			Expect(_view.Model.FrameArticleNumber, Is.EqualTo(expectedFrameOrder.Frame.ArticleNumber));
			Expect(_view.Model.FrameColor, Is.EqualTo(expectedFrameOrder.Frame.Color.Name));
			Expect(_view.Model.FrameBrand, Is.EqualTo(expectedFrameOrder.Frame.Brand.Name));
			Expect(_view.Model.FrameName, Is.EqualTo(expectedFrameOrder.Frame.Name));
            Expect(_view.Model.SupplierName, Is.EqualTo(expectedFrameOrder.Supplier.Name));
			Expect(_view.Model.GlassTypeName, Is.EqualTo(expectedFrameOrder.GlassType.Name));
			Expect(_view.Model.HeightLeft, Is.EqualTo(expectedFrameOrder.Height.Left));
			Expect(_view.Model.HeightRight, Is.EqualTo(expectedFrameOrder.Height.Right));
			Expect(_view.Model.Reference, Is.EqualTo(expectedFrameOrder.Reference));
			Expect(_view.Model.OrderDoesNotExist, Is.False);
			Expect(_view.Model.PupillaryDistanceLeft, Is.EqualTo(expectedFrameOrder.PupillaryDistance.Left));
			Expect(_view.Model.PupillaryDistanceRight, Is.EqualTo(expectedFrameOrder.PupillaryDistance.Right));
			Expect(_view.Model.SentDate, Is.EqualTo(null));
			Expect(_view.Model.ShopCity, Is.EqualTo(expectedFrameOrder.OrderingShop.Address.City));
			Expect(_view.Model.ShopName, Is.EqualTo(expectedFrameOrder.OrderingShop.Name));
			Expect(_view.Model.SphereLeft, Is.EqualTo(expectedFrameOrder.Sphere.Left));
			Expect(_view.Model.SphereRight, Is.EqualTo(expectedFrameOrder.Sphere.Right));
			Expect(_view.Model.UserDoesNotHaveAccessToThisOrder, Is.False);
			Expect(_view.Model.EditPageUrl, Is.EqualTo(expectedEditRedirectUrlWithQueryString));
			Expect(_view.Model.OrderHasBeenSent, Is.EqualTo(expectedFrameOrder.Sent.HasValue));
		}

		[Test]
		public void When_View_Is_Loaded_And_User_Belong_To_Another_Shop_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			const int expectedShopId = 10;

			//Act
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			_presenter.HttpContext = httpContext.Object;
			_presenter.View_Load(null, eventArgs);

			//Assert

			Expect(_view.Model.UserDoesNotHaveAccessToThisOrder, Is.True);
			Expect(_view.Model.DisplayOrder, Is.False);
		}

		[Test]
		public void When_View_Is_Loaded_With_A_NonExisting_Order_Id_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			var requestParams = new NameValueCollection();
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			

			//Act
			_presenter.HttpContext = httpContext.Object;
			_presenter.View_Load(null, eventArgs);

			//Assert
			Expect(_view.Model.OrderDoesNotExist, Is.True);
			Expect(_view.Model.DisplayOrder, Is.False);
		}

		[Test]
		public void When_Form_Is_Sent_Saved_Item_Has_Expected_Values()
		{
			//Arrange
			_frameOrderRepository.Get(10);
			var eventArgs = new EventArgs();
			const string expectedRedirectUrl = "/test/url/";
			var mockedHttpContext = new Mock<HttpContextBase>();
			var mockedHttpResponse = new Mock<HttpResponseBase>();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);

			//Act
			mockedHttpContext.SetupGet(x => x.Response).Returns(mockedHttpResponse.Object);
			A.CallTo(() => _routingService.GetPageUrl(A<int>.Ignored)).Returns(expectedRedirectUrl);
			//((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedPageUrl(expectedRedirectUrl);
			_presenter.HttpContext = mockedHttpContext.Object;
			_presenter.View.RedirectAfterSentOrderPageId = 5;
			_presenter.View_Load(null, eventArgs);
			_presenter.View_SendOrder(null, eventArgs);
			var savedEntity = ((RepositoryFactory.MockedFrameOrderRepository) _frameOrderRepository).SavedItem;
			var sentFrameOrder = ((ServiceFactory.MockFrameOrderSettingsService) _frameOrderService).SentFrameOrder;

			//Assert
			Expect(savedEntity.Sent, Is.Not.Null);
			Expect(savedEntity.Sent.Value.ToString("yyyy-MM-dd HH:mm"), Is.EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
			Expect(sentFrameOrder.Id, Is.EqualTo(5));
			mockedHttpResponse.Verify(x => x.Redirect(expectedRedirectUrl),Times.Once());

		}

		[Test]
		public void When_Shop_Has_Slim_Jim_Access_Ensure_Model_Has_Expected_values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			const int expectedShopId = 5;
			const string expectedEditRedirectUrl = "/test/url/";
			

			//Act
			A.CallTo(() => _routingService.GetPageUrl(A<int>.Ignored)).Returns(expectedEditRedirectUrl);
			//((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedPageUrl(expectedEditRedirectUrl);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetShopHasAccess(true);
			_presenter.HttpContext = httpContext.Object;
			_presenter.View.EditPageId = 1;
			_presenter.View_Load(null, eventArgs);

			//Assert
			Expect(_view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.False);
			Expect(_view.Model.DisplayOrder, Is.True);
		}

		[Test]
		public void When_Shop_Does_Not_Have_Slim_Jim_Access_Ensure_Model_Has_Expected_values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			const int expectedShopId = 5;
			const string expectedEditRedirectUrl = "/test/url/";
			

			//Act
			//((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedPageUrl(expectedEditRedirectUrl);
			A.CallTo(() => _routingService.GetPageUrl(A<int>.Ignored)).Returns(expectedEditRedirectUrl);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetMockedShopId(expectedShopId);
			((ServiceFactory.MockedSessionProviderService) _synologenMemberService).SetShopHasAccess(false);
			_presenter.HttpContext = httpContext.Object;
			_presenter.View.EditPageId = 1;
			_presenter.View_Load(null, eventArgs);

			//Assert
			Expect(_view.Model.ShopDoesNotHaveAccessToFrameOrders, Is.True);
			Expect(_view.Model.DisplayOrder, Is.False);
		}
	}
}