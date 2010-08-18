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
		public void When_EditColor_POST_Is_Called_Saved_DomainItem_Has_Expected_Values()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameColorEditView(7);

			//Act
			controller.EditColor(viewModel);
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));

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
		public void When_AddColor_POST_Is_Called_Saved_DomainItem_Has_Expected_Values()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameColorEditView(0);

			//Act
			controller.AddColor(viewModel);
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
		}

		[Test]
		public void When_DeleteColor_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values()
		{
			//Arrange
			const int itemId = 1;

			//Act
			controller.DeleteColor(itemId);
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameColor>) frameColorRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Not.Null);
			Expect(deletedItem.Id, Is.EqualTo(itemId));

		}

	}
}