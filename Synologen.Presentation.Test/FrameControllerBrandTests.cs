using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	public partial class Given_A_FrameController
	{

		[Test]
		public void When_Brands_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var gridPageSortParameters = new GridPageSortParameters();
			var expectedFirstItem = RepositoryFactory.GetMockedFrameBrand(1);
			var expectedAllowDelete = expectedFirstItem.NumberOfFramesWithThisBrand <= 0;

			//Act
			var result = (ViewResult) controller.Brands(gridPageSortParameters);
			var model = (IEnumerable<FrameBrandListItemView>) result.ViewData.Model;

			//Assert
			Expect(model.Count(), Is.EqualTo(10));
			Expect(model.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.First().Name, Is.EqualTo(expectedFirstItem.Name));
			Expect(model.First().NumberOfFramesWithThisBrand, Is.EqualTo(expectedFirstItem.NumberOfFramesWithThisBrand));
			Expect(model.First().AllowDelete, Is.EqualTo(expectedAllowDelete));
		}

		[Test]
		public void When_EditBrand_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var domainItem = RepositoryFactory.GetMockedFrameBrand(1);

			//Act
			var result = (ViewResult) controller.EditBrand(1);
			var model = (FrameBrandEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.FormLegend, Is.EqualTo("Redigera bågmärke"));
			Expect(model.Id, Is.EqualTo(domainItem.Id));
			Expect(model.Name, Is.EqualTo(domainItem.Name));
		}

		[Test]
		public void When_EditBrand_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameBrandEditView(9);
			const string expectedActionMessage = "Bågmärket har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.EditBrand(viewModel);
			var actionMessage = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(result.RouteValues["action"], Is.EqualTo("Brands"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_EditBrand_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.EditBrand(null);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_AddBrand_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange

			//Act
			var result = (ViewResult) controller.AddBrand();
			var model = (FrameBrandEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.FormLegend, Is.EqualTo("Skapa nytt bågmärke"));
			Expect(model.Id, Is.EqualTo(0));
			Expect(model.Name, Is.EqualTo(null));
		}

		[Test]
		public void When_AddBrand_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameBrandEditView(0);
			const string expectedActionMessage = "Bågmärket har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.AddBrand(viewModel);
			var actionMessage = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(result.RouteValues["action"], Is.EqualTo("Brands"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_AddBrand_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.AddBrand(null);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.False);
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_DeleteBrand_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			const int itemId = 1;
			const string expectedActionMessage = "Bågmärket har raderats";

			//Act
			var result =  (RedirectToRouteResult) controller.DeleteBrand(itemId);
			var actionMessage = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Not.Null);
			Expect(deletedItem.Id, Is.EqualTo(itemId));
			Expect(result.RouteValues["action"], Is.EqualTo("Brands"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_DeleteBrand_POST_Is_Called_With_An_Item_That_Has_Conncetions_An_ErrorMessage_Is_Registered_And_Redirects()
		{
			//Arrange
			const int itemId = -1;
			const string expectedActionMessage = "Bågmärket kunde inte raderas då det är knutet till en eller fler bågar";

			//Act
			var result =  (RedirectToRouteResult) controller.DeleteBrand(itemId);
			var actionMessage = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Null);
			Expect(result.RouteValues["action"], Is.EqualTo("Brands"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Error));

		}
	}
}