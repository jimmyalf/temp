using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture, Category("FrameControllerOrderTests")]
	public partial class Given_A_FrameController
	{
		[Test]
		public void When_FrameOrders_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var viewModel = new FrameOrderListView();
			var expectedFirstItem = RepositoryFactory.GetMockedFrameOrder(1);
			var expectedCreatedDate = expectedFirstItem.Created.ToString("yyyy-MM-dd");
			const string searchString = "SearchString≈ƒ÷";
			var encodedSearchString = HttpUtility.UrlEncode(searchString);

			//Act
			var result = (ViewResult) controller.FrameOrders(encodedSearchString, viewModel);
			var model = (FrameOrderListView) result.ViewData.Model;

			//Assert
			Expect(model.SearchTerm, Is.EqualTo(searchString));
			Expect(model.Column, Is.EqualTo(null));
			Expect(model.Direction, Is.EqualTo(SortDirection.Ascending));
			Expect(model.Page, Is.EqualTo(1));
			Expect(model.PageSize, Is.EqualTo(null));
			Expect(model.Direction, Is.EqualTo(SortDirection.Ascending));
			Expect(model.List.Count(), Is.EqualTo(10));
			Expect(model.List.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.List.First().Frame, Is.EqualTo(expectedFirstItem.Frame.Name));
			Expect(model.List.First().GlassType, Is.EqualTo(expectedFirstItem.GlassType.Name));
			Expect(model.List.First().Shop, Is.EqualTo(expectedFirstItem.OrderingShop.Name));
			Expect(model.List.First().Sent, Is.EqualTo(true));
			Expect(model.List.First().Created, Is.EqualTo(expectedCreatedDate));
		}

		[Test]
		public void When_FrameOrders_POST_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			const string searchword = "TestSearchWord";
			var inputQueryString = new NameValueCollection {{"Page", "1"}, {"Column", "Name"}};
			var controllerContext = new Mock<ControllerContext>();
			var viewModel = new FrameOrderListView {SearchTerm = searchword};

			//Act
			controllerContext.SetupGet(x => x.HttpContext.Request.QueryString).Returns(inputQueryString);
			controller.ControllerContext = controllerContext.Object;
			var result = (RedirectToRouteResult) controller.FrameOrders(viewModel);

			//Assert
			Expect(result.RouteValues["action"], Is.EqualTo("FrameOrders"));
			Expect(result.RouteValues["search"], Is.EqualTo("TestSearchWord"));
			Expect(result.RouteValues["Page"], Is.EqualTo("1"));
			Expect(result.RouteValues["Column"], Is.EqualTo("Name"));
		}

		[Test]
		public void When_ViewFrameOrder_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
		    //Arrange
		    const int frameOrderId = 1;
		    var domainItem = RepositoryFactory.GetMockedFrameOrder(frameOrderId);

		    //Act
		    var result = (ViewResult) controller.ViewFrameOrder(frameOrderId);
		    var model = (FrameOrderView) result.ViewData.Model;

		    //Assert
		    Expect(model, Is.Not.Null);
			
		    Expect(model.Addition.Left, Is.EqualTo(domainItem.Addition.Left));
		    Expect(model.Addition.Right, Is.EqualTo(domainItem.Addition.Right));
		    Expect(model.Axis.Left, Is.EqualTo(domainItem.Axis.Left));
		    Expect(model.Axis.Right, Is.EqualTo(domainItem.Axis.Right));
		    Expect(model.Created, Is.EqualTo(domainItem.Created.ToString("yyyy-MM-dd HH:mm")));
		    Expect(model.Cylinder.Left, Is.EqualTo(domainItem.Cylinder.Left));
		    Expect(model.Cylinder.Right, Is.EqualTo(domainItem.Cylinder.Right));
		    Expect(model.Frame, Is.EqualTo(domainItem.Frame.Name));
		    Expect(model.FrameArticleNumber, Is.EqualTo(domainItem.Frame.ArticleNumber));
		    Expect(model.GlassType, Is.EqualTo(domainItem.GlassType.Name));
		    Expect(model.Height.Left, Is.EqualTo(domainItem.Height.Left));
		    Expect(model.Height.Right, Is.EqualTo(domainItem.Height.Right));
		    Expect(model.Id, Is.EqualTo(domainItem.Id));
		    Expect(model.Shop, Is.EqualTo(domainItem.OrderingShop.Name));
		    Expect(model.ShopCity, Is.EqualTo(domainItem.OrderingShop.Address.City));
		    Expect(model.PupillaryDistance.Left, Is.EqualTo(domainItem.PupillaryDistance.Left));
		    Expect(model.PupillaryDistance.Right, Is.EqualTo(domainItem.PupillaryDistance.Right));
		    Expect(model.Sent, Is.EqualTo(domainItem.Sent.Value.ToString("yyyy-MM-dd HH:mm")));
		    Expect(model.Sphere.Left, Is.EqualTo(domainItem.Sphere.Left));
		    Expect(model.Sphere.Right, Is.EqualTo(domainItem.Sphere.Right));
			Expect(model.Notes, Is.EqualTo(domainItem.Reference));

		}

		[Test]
		public void When_ViewFrameOrder_GET_Is_Called_With_FrameOrder_Missing_Cylinder_And_Axis_Returned_ViewModel_Has_Expected_Values()
		{
		    //Arrange
		    const int frameOrderId = 1;
			var expectedFrameOrder = RepositoryFactory.GetMockedFrameOrder(frameOrderId);
			expectedFrameOrder.Axis = null;
			expectedFrameOrder.Cylinder = null;
			var mockedFrameRepository = new Mock<IFrameOrderRepository>();
			mockedFrameRepository.Setup(x => x.Get(It.Is<int>(id => id.Equals(frameOrderId)))).Returns(expectedFrameOrder);
			var testController = new FrameController(frameRepository, frameColorRepository, frameBrandRepository, frameGlassTypeRepository, mockedFrameRepository.Object, _adminSettingsService);

		    //Act
		    var result = (ViewResult) testController.ViewFrameOrder(frameOrderId);
		    var model = (FrameOrderView) result.ViewData.Model;

		    //Assert
		
		    Expect(model.Axis.DisplayLeftValue, Is.False);
		    Expect(model.Axis.DisplayRightValue, Is.False);
		    Expect(model.Cylinder.DisplayLeftValue, Is.False);
		    Expect(model.Cylinder.DisplayRightValue, Is.False);
		}
	}
}