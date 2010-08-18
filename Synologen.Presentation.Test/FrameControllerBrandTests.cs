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
		public void When_EditBrand_POST_Is_Called_Saved_DomainItem_Has_Expected_Values()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameBrandEditView(9);

			//Act
			controller.EditBrand(viewModel);
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));

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
		public void When_AddBrand_POST_Is_Called_Saved_DomainItem_Has_Expected_Values()
		{
			//Arrange
			var viewModel = ViewModelFactory.GetFrameBrandEditView(0);

			//Act
			controller.AddBrand(viewModel);
			var savedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).SavedEntity;

			//Assert
			Expect(savedItem, Is.Not.Null);
			Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
			Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
		}

		[Test]
		public void When_DeleteBrand_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values()
		{
			//Arrange
			const int itemId = 1;

			//Act
			controller.DeleteBrand(itemId);
			var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameBrand>) frameBrandRepository).DeletedEntity;

			//Assert
			Expect(deletedItem, Is.Not.Null);
			Expect(deletedItem.Id, Is.EqualTo(itemId));

		}
	}
}