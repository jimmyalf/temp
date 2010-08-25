using System.Linq;
using System.Web.Mvc;
using MvcContrib.Sorting;

using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	public partial class Given_A_FrameController
	{
		[Test]
		public void When_FrameOrders_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var viewModel = new FrameOrderListView();
			var expectedFirstItem = RepositoryFactory.GetMockedFrameOrder(1);
			var expectedCreatedDate = expectedFirstItem.Created.ToString("yyyy-MM-dd");

			//Act
			var result = (ViewResult) controller.FrameOrders(viewModel);
			var model = (FrameOrderListView) result.ViewData.Model;

			//Assert
			Expect(model.Search, Is.EqualTo(null));
			Expect(model.Column, Is.EqualTo(null));
			Expect(model.Direction, Is.EqualTo(SortDirection.Ascending));
			Expect(model.Page, Is.EqualTo(1));
			Expect(model.PageSize, Is.EqualTo(null));
			Expect(model.SortAscending, Is.EqualTo(true));
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
			var viewModel = new FrameOrderListView {Search = searchword};
			var expectedFirstItem = RepositoryFactory.GetMockedFrameOrder(1);
			var expectedCreatedDate = expectedFirstItem.Created.ToString("yyyy-MM-dd");

			//Act
			var result = (ViewResult) controller.FrameOrders(viewModel);
			var model = (FrameOrderListView) result.ViewData.Model;

			//Assert
			Expect(model.Search, Is.EqualTo(searchword));
			Expect(model.Column, Is.EqualTo("Frame"));
			Expect(model.Direction, Is.EqualTo(SortDirection.Ascending));
			Expect(model.Page, Is.EqualTo(1));
			Expect(model.PageSize, Is.EqualTo(null));
			Expect(model.SortAscending, Is.EqualTo(true));
			Expect(model.List.Count(), Is.EqualTo(10));
			Expect(model.List.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.List.First().Frame, Is.EqualTo(expectedFirstItem.Frame.Name));
			Expect(model.List.First().GlassType, Is.EqualTo(expectedFirstItem.GlassType.Name));
			Expect(model.List.First().Shop, Is.EqualTo(expectedFirstItem.OrderingShop.Name));
			Expect(model.List.First().Sent, Is.EqualTo(true));
			Expect(model.List.First().Created, Is.EqualTo(expectedCreatedDate));
		}
	}
}