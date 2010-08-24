using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	public partial class Given_A_FrameController : AssertionHelper
	{
		private IFrameRepository frameRepository;
		private IFrameColorRepository frameColorRepository;
		private IFrameBrandRepository frameBrandRepository;
		private IFrameGlassTypeRepository frameGlassTypeRepository;
		private ISettingsService settingsService;
		private FrameController controller;
		

		[SetUp]
		public void Context()
		{
			frameRepository = RepositoryFactory.GetFrameRepository();
			frameColorRepository = RepositoryFactory.GetFrameColorRepository();
			frameBrandRepository = RepositoryFactory.GetFrameBrandRepository();
			frameGlassTypeRepository = RepositoryFactory.GetFrameGlassTypeRepository();
			settingsService = ServiceFactory.GetSettingsService();
			controller = new FrameController(frameRepository, frameColorRepository, frameBrandRepository, frameGlassTypeRepository, settingsService);
		}

		[Test]
		public void When_Index_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			const string searchword = "TestSearchWord";
			var gridPageSortParameters = new GridPageSortParameters();
			var expectedFirstItem = RepositoryFactory.GetMockedFrame(1);

			//Act
			var result = (ViewResult) controller.Index(searchword, gridPageSortParameters);
			var model = (FrameListView) result.ViewData.Model;

			//Assert
			Expect(model.SearchWord, Is.EqualTo(searchword));
			Expect(model.List.Count(), Is.EqualTo(10));
			Expect(model.List.First().AllowOrders, Is.EqualTo(expectedFirstItem.AllowOrders));
			Expect(model.List.First().ArticleNumber, Is.EqualTo(expectedFirstItem.ArticleNumber));
			Expect(model.List.First().Brand, Is.EqualTo(expectedFirstItem.Brand.Name));
			Expect(model.List.First().Color, Is.EqualTo(expectedFirstItem.Color.Name));
			Expect(model.List.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.List.First().Name, Is.EqualTo(expectedFirstItem.Name));
		}

		[Test]
		public void When_Index_POST_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			const string searchword = "TestSearchWord";
			var viewModel = new FrameListView {SearchWord = searchword};
			var gridPageSortParameters = new GridPageSortParameters();
			var expectedFirstItem = RepositoryFactory.GetMockedFrame(1);

			//Act
			var result = (ViewResult) controller.Index(viewModel, gridPageSortParameters);
			var model = (FrameListView) result.ViewData.Model;

			//Assert
			Expect(model.SearchWord, Is.EqualTo(searchword));
			Expect(model.List.Count(), Is.EqualTo(10));
			Expect(model.List.First().AllowOrders, Is.EqualTo(expectedFirstItem.AllowOrders));
			Expect(model.List.First().ArticleNumber, Is.EqualTo(expectedFirstItem.ArticleNumber));
			Expect(model.List.First().Brand, Is.EqualTo(expectedFirstItem.Brand.Name));
			Expect(model.List.First().Color, Is.EqualTo(expectedFirstItem.Color.Name));
			Expect(model.List.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.List.First().Name, Is.EqualTo(expectedFirstItem.Name));
		}

		[Test]
		public void When_Edit_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var domainItem = RepositoryFactory.GetMockedFrame(1);

			//Act
			var result = (ViewResult) controller.Edit(1);
			var model = (FrameEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.AllowOrders, Is.EqualTo(domainItem.AllowOrders));
			Expect(model.ArticleNumber, Is.EqualTo(domainItem.ArticleNumber));
			Expect(model.AvailableFrameBrands.Count(), Is.EqualTo(10));
			Expect(model.AvailableFrameColors.Count(), Is.EqualTo(10));
			Expect(model.BrandId, Is.EqualTo(domainItem.Brand.Id));
			Expect(model.ColorId, Is.EqualTo(domainItem.Color.Id));
			Expect(model.FormLegend, Is.EqualTo("Redigera båge"));
			Expect(model.Id, Is.EqualTo(domainItem.Id));
			Expect(model.PupillaryDistanceIncrementation, Is.EqualTo(domainItem.PupillaryDistance.Increment));
			Expect(model.PupillaryDistanceMaxValue, Is.EqualTo(domainItem.PupillaryDistance.Max));
			Expect(model.PupillaryDistanceMinValue, Is.EqualTo(domainItem.PupillaryDistance.Min));
		}

		[Test]
		public void When_Edit_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameEditView(3);
			const string expectedActionMessage = "Bågen har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.Edit(viewModel);
			var actionMessages = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<Frame>) frameRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.AllowOrders, Is.EqualTo(viewModel.AllowOrders));
			Expect(savedItem.ArticleNumber, Is.EqualTo(viewModel.ArticleNumber));
			Expect(savedItem.Brand.Id, Is.EqualTo(viewModel.BrandId));
			Expect(savedItem.Color.Id, Is.EqualTo(viewModel.ColorId));
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(savedItem.PupillaryDistance.Increment, Is.EqualTo(viewModel.PupillaryDistanceIncrementation));
			Expect(savedItem.PupillaryDistance.Max, Is.EqualTo(viewModel.PupillaryDistanceMaxValue));
			Expect(savedItem.PupillaryDistance.Min, Is.EqualTo(viewModel.PupillaryDistanceMinValue));
			Expect(result.RouteValues["action"], Is.EqualTo("Index"));
			Expect(actionMessages.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessages.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_Edit_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameEditView(3);

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.Edit(viewModel);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<Frame>) frameRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_Add_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange

			//Act
			var result = (ViewResult) controller.Add();
			var model = (FrameEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.AllowOrders, Is.EqualTo(true));
			Expect(model.ArticleNumber, Is.EqualTo(null));
			Expect(model.AvailableFrameBrands.Count(), Is.EqualTo(10));
			Expect(model.AvailableFrameColors.Count(), Is.EqualTo(10));
			Expect(model.BrandId, Is.EqualTo(0));
			Expect(model.ColorId, Is.EqualTo(0));
			Expect(model.FormLegend, Is.EqualTo("Skapa ny båge"));
			Expect(model.Id, Is.EqualTo(0));
			Expect(model.PupillaryDistanceIncrementation, Is.EqualTo(0.5m));
			Expect(model.PupillaryDistanceMaxValue, Is.EqualTo(40));
			Expect(model.PupillaryDistanceMinValue, Is.EqualTo(20));
		}

		[Test]
		public void When_Add_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameEditView(0);
			const string expectedActionMessage = "Bågen har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.Add(viewModel);
			var actionMessages = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<Frame>) frameRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.AllowOrders, Is.EqualTo(viewModel.AllowOrders));
			Expect(savedItem.ArticleNumber, Is.EqualTo(viewModel.ArticleNumber));
			Expect(savedItem.Brand.Id, Is.EqualTo(viewModel.BrandId));
			Expect(savedItem.Color.Id, Is.EqualTo(viewModel.ColorId));
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(savedItem.PupillaryDistance.Increment, Is.EqualTo(viewModel.PupillaryDistanceIncrementation));
			Expect(savedItem.PupillaryDistance.Max, Is.EqualTo(viewModel.PupillaryDistanceMaxValue));
			Expect(savedItem.PupillaryDistance.Min, Is.EqualTo(viewModel.PupillaryDistanceMinValue));
			Expect(result.RouteValues["action"], Is.EqualTo("Index"));
			Expect(actionMessages.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessages.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_Add_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameEditView(0);

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.Add(viewModel);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<Frame>) frameRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.False);
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_Delete_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			const int itemId = 1;
			const string expectedActionMessage = "Bågen har raderats";

			//Act
			var result = (RedirectToRouteResult) controller.Delete(itemId);
			var actionMessages = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<Frame>) frameRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Not.Null);
			Expect(deletedItem.Id, Is.EqualTo(itemId));
			Expect(result.RouteValues["action"], Is.EqualTo("Index"));
			Expect(actionMessages.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessages.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_Delete_POST_Is_Called_With_An_Item_That_Has_Conncetions_An_ErrorMessage_Is_Registered_And_Redirects()
		{
			//Arrange
			const int itemId = -1;
			const string expectedActionMessage = "Bågen kunde inte raderas då den är knuten till en eller fler beställningar";

			//Act
			var result =  (RedirectToRouteResult) controller.Delete(itemId);
			var actionMessage = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<Frame>) frameRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Null);
			Expect(result.RouteValues["action"], Is.EqualTo("Index"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Error));

		}
	}
}