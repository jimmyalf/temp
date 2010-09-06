using System;
using System.Collections.Specialized;
using System.Web;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using Spinit.Wpc.Synologen.Presentation.Site.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test
{

	[TestFixture]
	public class Given_a_FrameOrderViewPresenter : AssertionHelper
	{
		private ViewFrameOrderPresenter presenter;
		private IViewFrameOrderView<ViewFrameOrderModel> view;
		private IFrameOrderRepository frameOrderRepository;
		private ISynologenMemberService synologenMemberService;
		private IFrameOrderService _frameOrderService;

		[SetUp]
		public void Context()
		{
			frameOrderRepository = RepositoryFactory.GetFramOrderRepository();
			synologenMemberService = ServiceFactory.GetSynologenMemberService();
			_frameOrderService = ServiceFactory.GetFrameOrderSettingsService();
			view = ViewsFactory.GetViewFrameOrderView();
			presenter = new ViewFrameOrderPresenter(view, frameOrderRepository, synologenMemberService, _frameOrderService);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(x => x.Request.Params).Returns(requestParams);
			var expectedFrameOrder = frameOrderRepository.Get(5);
			const int expectedShopId = 5;
			const string expectedEditRedirectUrl = "/test/url/";
			var expectedEditRedirectUrlWithQueryString = String.Concat(expectedEditRedirectUrl, "?frameorder=", 5);
			

			//Act
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedPageUrl(expectedEditRedirectUrl);
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedShopId(expectedShopId);
			presenter.HttpContext = httpContext.Object;
			presenter.View.EditPageId = 1;
			presenter.View_Load(null, eventArgs);

			//Assert

			Expect(view.Model.AdditionLeft, Is.EqualTo(expectedFrameOrder.Addition.Left));
			Expect(view.Model.AdditionRight, Is.EqualTo(expectedFrameOrder.Addition.Right));
			Expect(view.Model.AxisSelectionLeft, Is.EqualTo(expectedFrameOrder.Axis.Left));
			Expect(view.Model.AxisSelectionRight, Is.EqualTo(expectedFrameOrder.Axis.Right));
			Expect(view.Model.CreatedDate, Is.EqualTo(expectedFrameOrder.Created.ToString("yyyy-MM-dd HH:mm")));
			Expect(view.Model.CylinderLeft, Is.EqualTo(expectedFrameOrder.Cylinder.Left));
			Expect(view.Model.CylinderRight, Is.EqualTo(expectedFrameOrder.Cylinder.Right));
			Expect(view.Model.DisplayOrder, Is.True);
			Expect(view.Model.FrameArticleNumber, Is.EqualTo(expectedFrameOrder.Frame.ArticleNumber));
			Expect(view.Model.FrameColor, Is.EqualTo(expectedFrameOrder.Frame.Color.Name));
			Expect(view.Model.FrameBrand, Is.EqualTo(expectedFrameOrder.Frame.Brand.Name));
			Expect(view.Model.FrameName, Is.EqualTo(expectedFrameOrder.Frame.Name));
			Expect(view.Model.GlassTypeName, Is.EqualTo(expectedFrameOrder.GlassType.Name));
			Expect(view.Model.HeightLeft, Is.EqualTo(expectedFrameOrder.Height.Left));
			Expect(view.Model.HeightRight, Is.EqualTo(expectedFrameOrder.Height.Right));
			Expect(view.Model.Notes, Is.EqualTo(expectedFrameOrder.Notes));
			Expect(view.Model.OrderDoesNotExist, Is.False);
			Expect(view.Model.PupillaryDistanceLeft, Is.EqualTo(expectedFrameOrder.PupillaryDistance.Left));
			Expect(view.Model.PupillaryDistanceRight, Is.EqualTo(expectedFrameOrder.PupillaryDistance.Right));
			Expect(view.Model.SentDate, Is.EqualTo(null));
			Expect(view.Model.ShopCity, Is.EqualTo(expectedFrameOrder.OrderingShop.Address.City));
			Expect(view.Model.ShopName, Is.EqualTo(expectedFrameOrder.OrderingShop.Name));
			Expect(view.Model.SphereLeft, Is.EqualTo(expectedFrameOrder.Sphere.Left));
			Expect(view.Model.SphereRight, Is.EqualTo(expectedFrameOrder.Sphere.Right));
			Expect(view.Model.UserDoesNotHaveAccessToThisOrder, Is.False);
			Expect(view.Model.EditPageUrl, Is.EqualTo(expectedEditRedirectUrlWithQueryString));
			Expect(view.Model.OrderHasBeenSent, Is.EqualTo(expectedFrameOrder.Sent.HasValue));
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
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedShopId(expectedShopId);
			presenter.HttpContext = httpContext.Object;
			presenter.View_Load(null, eventArgs);

			//Assert

			Expect(view.Model.UserDoesNotHaveAccessToThisOrder, Is.True);
			Expect(view.Model.DisplayOrder, Is.False);
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
			presenter.HttpContext = httpContext.Object;
			presenter.View_Load(null, eventArgs);

			//Assert
			Expect(view.Model.OrderDoesNotExist, Is.True);
			Expect(view.Model.DisplayOrder, Is.False);
		}

		[Test]
		public void When_Form_Is_Sent_Saved_Item_Has_Expected_Values()
		{
		    //Arrange
			var frameOrder = frameOrderRepository.Get(10);
		    var eventArgs = new EventArgs();
			const string expectedRedirectUrl = "/test/url/";
			var mockedHttpContext = new Mock<HttpContextBase>();
			var mockedHttpResponse = new Mock<HttpResponseBase>();
			var requestParams = new NameValueCollection {{"frameorder", "5"}};
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(requestParams);

		    //Act
			mockedHttpContext.SetupGet(x => x.Response).Returns(mockedHttpResponse.Object);
			((ServiceFactory.MockedSessionProviderService) synologenMemberService).SetMockedPageUrl(expectedRedirectUrl);
			presenter.HttpContext = mockedHttpContext.Object;
			presenter.View.RedirectAfterSentOrderPageId = 5;
		    presenter.View_Load(null, eventArgs);
			presenter.View_SendOrder(null, eventArgs);
			var savedEntity = ((RepositoryFactory.MockedFrameOrderRepository) frameOrderRepository).SavedItem;
			var sentFrameOrder = ((ServiceFactory.MockFrameOrderSettingsService) _frameOrderService).SentFrameOrder;

		    //Assert
			Expect(savedEntity.Sent, Is.Not.Null);
			Expect(savedEntity.Sent.Value.ToString("yyyy-MM-dd HH:mm"), Is.EqualTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
			Expect(sentFrameOrder.Id, Is.EqualTo(5));
			mockedHttpResponse.Verify(x => x.Redirect(expectedRedirectUrl),Times.Once());

		}
	}
}
