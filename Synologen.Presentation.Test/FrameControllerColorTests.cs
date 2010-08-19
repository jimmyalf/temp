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
		public void When_Colors_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var gridPageSortParameters = new GridPageSortParameters();
			var expectedFirstItem = RepositoryFactory.GetMockedFrameColor(1);
			var expectedAllowDelete = expectedFirstItem.NumberOfFramesWithThisColor <= 0;

			//Act
			var result = (ViewResult) controller.Colors(gridPageSortParameters);
			var model = (IEnumerable<FrameColorListItemView>) result.ViewData.Model;

			//Assert
			Expect(model.Count(), Is.EqualTo(10));
			Expect(model.First().Id, Is.EqualTo(expectedFirstItem.Id));
			Expect(model.First().Name, Is.EqualTo(expectedFirstItem.Name));
			Expect(model.First().NumberOfFramesWithThisColor, Is.EqualTo(expectedFirstItem.NumberOfFramesWithThisColor));
			Expect(model.First().AllowDelete, Is.EqualTo(expectedAllowDelete));
		}

		[Test]
		public void When_EditColor_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange
			var domainItem = RepositoryFactory.GetMockedFrameColor(1);

			//Act
			var result = (ViewResult) controller.EditColor(1);
			var model = (FrameColorEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.FormLegend, Is.EqualTo("Redigera bågfärg"));
			Expect(model.Id, Is.EqualTo(domainItem.Id));
			Expect(model.Name, Is.EqualTo(domainItem.Name));
		}

		[Test]
		public void When_EditColor_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameColorEditView(7);
			const string expectedActionMessage = "Bågfärgen har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.EditColor(viewModel);
			var actionMessages = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(result.RouteValues["action"], Is.EqualTo("Colors"));
			Expect(actionMessages.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessages.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_EditColor_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.EditColor(null);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_AddColor_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
		{
			//Arrange

			//Act
			var result = (ViewResult) controller.AddColor();
			var model = (FrameColorEditView) result.ViewData.Model;

			//Assert
			Expect(model, Is.Not.Null);
			Expect(model.FormLegend, Is.EqualTo("Skapa ny bågfärg"));
			Expect(model.Id, Is.EqualTo(0));
			Expect(model.Name, Is.EqualTo(null));
		}

		[Test]
		public void When_AddColor_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameColorEditView(0);
			const string expectedActionMessage = "Bågfärgen har sparats";

			//Act
			var result = (RedirectToRouteResult) controller.AddColor(viewModel);
			var actionMessages = controller.GetWpcActionMessages();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
			Expect(result.RouteValues["action"], Is.EqualTo("Colors"));
			Expect(actionMessages.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessages.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_AddColor_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
		{
			//Arrange

			//Act
			controller.ModelState.AddModelError("*", "Invalid model state");
			var result = controller.AddColor(null);
			var viewResult = result as ViewResult ?? new ViewResult();
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Null);
			Expect(viewResult.ViewData.ModelState.IsValid, Is.False);
			Expect(result is RedirectToRouteResult, Is.False);
		}

		[Test]
		public void When_DeleteColor_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values_And_Redirects()
		{
			//Arrange
			const int itemId = 1;
			const string expectedActionMessage = "Bågfärgen har raderats";

			//Act
			var result = (RedirectToRouteResult) controller.DeleteColor(itemId);
			var actionMessages = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Not.Null);
			Expect(deletedItem.Id, Is.EqualTo(itemId));
			Expect(result.RouteValues["action"], Is.EqualTo("Colors"));
			Expect(actionMessages.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessages.First().Type, Is.EqualTo(WpcActionMessageType.Success));
		}

		[Test]
		public void When_DeleteColor_POST_Is_Called_With_An_Item_That_Has_Conncetions_An_ErrorMessage_Is_Registered_And_Redirects()
		{
			//Arrange
			const int itemId = -1;
			const string expectedActionMessage = "Bågfärgen kunde inte raderas då den är knuten till en eller fler bågar";

			//Act
			var result =  (RedirectToRouteResult) controller.DeleteColor(itemId);
			var actionMessage = controller.GetWpcActionMessages();
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Null);
			Expect(result.RouteValues["action"], Is.EqualTo("Colors"));
			Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
			Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Error));

		}

	}
}