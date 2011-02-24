using System.Linq;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Integration.Data.Test.FrameData;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData
{
	[TestFixture, Category("TestFrameBrands")]
	public class Given_a_framebrand : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_persisted_framebrand()
		{
			//Arrange
			const int expectedNumberOfFrameConnections = 6;

			//Act
			var savedFrameBrand = SavedFrameBrands.First();
			var persistedFrameBrand = FrameBrandValidationRepository.Get(savedFrameBrand.Id);
			
			//Assert
			Expect(persistedFrameBrand, Is.Not.Null);
			Expect(persistedFrameBrand.Id, Is.EqualTo(savedFrameBrand.Id));
			Expect(persistedFrameBrand.Name, Is.EqualTo(savedFrameBrand.Name));
			Expect(persistedFrameBrand.NumberOfFramesWithThisBrand, Is.EqualTo(expectedNumberOfFrameConnections));
		}

		[Test]
		public void Can_edit_persisted_framebrand()
		{
			//Arrange
			const int expectedNumberOfFrameConnections = 6;

			//Act
			var editedFrameBrand = Integration.Data.Test.FrameData.Factories.FrameBrandFactory.ScrabmleFrameBrand(SavedFrameBrands.First());
			FrameBrandRepository.Save(editedFrameBrand);
			var persistedFrameBrand = FrameBrandValidationRepository.Get(SavedFrameBrands.First().Id);
			
			//Assert
			Expect(persistedFrameBrand, Is.Not.Null);
			Expect(persistedFrameBrand.Id, Is.EqualTo(editedFrameBrand.Id));
			Expect(persistedFrameBrand.Name, Is.EqualTo(editedFrameBrand.Name));
			Expect(persistedFrameBrand.NumberOfFramesWithThisBrand, Is.EqualTo(expectedNumberOfFrameConnections));
		}

		[Test]
		public void Can_delete_persisted_framebrand_without_connections()
		{
			//Arrange
			var frameBrandWithoutConnections = Integration.Data.Test.FrameData.Factories.FrameBrandFactory.GetFrameBrand();
			FrameBrandRepository.Save(frameBrandWithoutConnections);

			//Act
			FrameBrandRepository.Delete(frameBrandWithoutConnections);
			var persistedFrameBrand = FrameBrandValidationRepository.Get(frameBrandWithoutConnections.Id);
			
			//Assert
			Expect(persistedFrameBrand, Is.Null);

		}

		[Test]
		public void Cannot_delete_persisted_framebrand_with_connections()
		{
			//Arrange

			//Act
			
			//Assert
			Expect(() => FrameBrandRepository.Delete(SavedFrameBrands.First()), Throws.InstanceOf<SynologenDeleteItemHasConnectionsException>());
		}
	}

	[TestFixture, Category("TestFrameBrands")]
	public class Given_multiple_framebrands : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_framebrands_by_PageOfFrameBrandsMatchingCriteria_paged()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 5;
			var criteria = new PagedSortedCriteria<FrameBrand> {
				OrderBy = null,
				Page = 1,
				PageSize = expectedNumberOfItemsMatchingCriteria,
				SortAscending = true
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameBrandValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Prada"));
			Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Quicksilver"));
			
		}

		[Test]
		public void Can_get_framebrands_by_PageOfFrameBrandsMatchingCriteria_sorted_by_id()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 6;
			var criteria = new PagedSortedCriteria<FrameBrand>
			{
				OrderBy = "Id",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameBrandValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Prada"));
			Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Pilgrim"));
			
		}

		[Test]
		public void Can_get_brands_by_PageOfFrameBrandsMatchingCriteria_sorted_by_name()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 6;
			var criteria = new PagedSortedCriteria<FrameBrand>
			{
				OrderBy = "Name",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameBrandValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Björn borg"));
			Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Red or Dead"));
			
		}

		[Test]
		public void Can_get_brands_by_PageOfFrameBrandsMatchingCriteria_sorted_descending()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 6;
			var criteria = new PagedSortedCriteria<FrameBrand>
			{
				OrderBy = "Id",
				Page = 1,
				PageSize = 100,
				SortAscending = false
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameBrandValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Pilgrim"));
			Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Prada"));
		}
	}
}