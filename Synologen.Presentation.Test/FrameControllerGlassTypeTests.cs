using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	public partial class Given_A_FrameController
	{

		[Test]
		public void When_GlassTypes_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var gridPageSortParameters = new GridPageSortParameters();
			var expectedFirstItem = RepositoryFactory.GetMockedFrameGlass(1);

			//Act
			var result = (ViewResult) controller.GlassTypes(gridPageSortParameters);
			var model = (IEnumerable<FrameGlassTypeListItemView>) result.ViewData.Model;

			//Assert
			Expect(model.Count(), Is.EqualTo(10));
			Expect(model.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.First().Name, Is.EqualTo(expectedFirstItem.Name));
			Expect(model.First().IncludeAddition, Is.EqualTo(expectedFirstItem.IncludeAdditionParametersInOrder));
			Expect(model.First().IncludeHeight, Is.EqualTo(expectedFirstItem.IncludeHeightParametersInOrder));

		}

		[Test]
		public void When_EditGlassType_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var domainItem = RepositoryFactory.GetMockedFrameGlass(1);

			//Act
			var result = (ViewResult) controller.EditGlassType(1);
			var model = (FrameGlassTypeEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.FormLegend, Is.EqualTo("Redigera glastyp"));
			Expect(model.Id, Is.EqualTo(domainItem.Id));
			Expect(model.Name, Is.EqualTo(domainItem.Name));
			Expect(model.IncludeAdditionParametersInOrder, Is.EqualTo(domainItem.IncludeAdditionParametersInOrder));
			Expect(model.IncludeHeightParametersInOrder, Is.EqualTo(domainItem.IncludeHeightParametersInOrder));
		}

		[Test]
		public void When_EditGlassType_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetGlassTypeEditView(4);
			const string expectedActionMessage = "Glastypen har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.EditGlassType(viewModel);
			var actionMessage = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameGlassType>) frameGlassTypeRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(savedItem.IncludeAdditionParametersInOrder, Is.EqualTo(viewModel.IncludeAdditionParametersInOrder));
			Expect(savedItem.IncludeHeightParametersInOrder, Is.EqualTo(viewModel.IncludeHeightParametersInOrder));
			Expect(result.RouteValues["action"], Is.EqualTo("GlassTypes"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_EditGlassType_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.EditGlassType(null);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameGlassType>) frameGlassTypeRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_AddGlassType_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange

			//Act
			var result = (ViewResult) controller.AddGlassType();
			var model = (FrameGlassTypeEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.FormLegend, Is.EqualTo("Skapa ny glastyp"));
			Expect(model.Id, Is.EqualTo(0));
			Expect(model.Name, Is.EqualTo(null));
			Expect(model.IncludeAdditionParametersInOrder, Is.EqualTo(false));
			Expect(model.IncludeHeightParametersInOrder, Is.EqualTo(false));
		}

		[Test]
		public void When_AddGlassType_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetGlassTypeEditView(0);
			const string expectedActionMessage = "Glastypen har sparats";

			//Act
			var result = (RedirectToRouteResult)controller.AddGlassType(viewModel);
			var actionMessage = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameGlassType>) frameGlassTypeRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(savedItem.IncludeAdditionParametersInOrder, Is.EqualTo(viewModel.IncludeAdditionParametersInOrder));
			Expect(savedItem.IncludeHeightParametersInOrder, Is.EqualTo(viewModel.IncludeHeightParametersInOrder));
			Expect(result.RouteValues["action"], Is.EqualTo("GlassTypes"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_AddGlassType_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.AddGlassType(null);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameGlassType>) frameGlassTypeRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.False);
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_DeleteGlassType_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			const int itemId = 1;
			const string expectedActionMessage = "Glastypen har raderats";

			//Act
			var result =  (RedirectToRouteResult) controller.DeleteGlassType(itemId);
			var actionMessage = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameGlassType>) frameGlassTypeRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Not.Null);
			Expect(deletedItem.Id, Is.EqualTo(itemId));
			Expect(result.RouteValues["action"], Is.EqualTo("GlassTypes"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));

		}

		[Test]
		public void When_DeleteGlassType_POST_Is_Called_With_An_Item_That_Has_Conncetions_An_ErrorMessage_Is_Registered_And_Redirects()
		{
			//Arrange
			const int itemId = -1;
			const string expectedActionMessage = "Glastypen kunde inte raderas då den är knuten till en eller fler beställningar";

			//Act
			var result =  (RedirectToRouteResult) controller.DeleteGlassType(itemId);
			var actionMessage = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameGlassType>) frameGlassTypeRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Null);
			Expect(result.RouteValues["action"], Is.EqualTo("GlassTypes"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Error));

		}

	}
}