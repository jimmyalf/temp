using System.Linq;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Data.Test.FrameData.Factories;
using Spinit.Wpc.Synologen.Integration.Data.Test.FrameData;
using Spinit.Wpc.Synologen.Test.Core;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData
{
	[TestFixture, Category("TestFrameColors")]
	public class Given_a_framecolor : TestBase
	{

		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_persisted_framecolor()
		{
			//Arrange

			//Act
			var savedFrameColor = SavedFrameColors.First();
			var persistedFrameColor = FrameColorValidationRepository.Get(savedFrameColor.Id);
			
			//Assert
			Expect(persistedFrameColor, Is.Not.Null);
			Expect(persistedFrameColor.Id, Is.EqualTo(savedFrameColor.Id));
			Expect(persistedFrameColor.Name, Is.EqualTo(savedFrameColor.Name));
		}

		[Test]
		public void Can_edit_persisted_framecolor()
		{
			//Arrange

			//Act
			var editedFrameColor = FrameColorFactory.ScrabmleFrameColor(SavedFrameColors.First());
			FrameColorRepository.Save(editedFrameColor);
			var persistedFrameColor = FrameColorValidationRepository.Get(SavedFrameColors.First().Id);
			
			//Assert
			Expect(persistedFrameColor, Is.Not.Null);
			Expect(persistedFrameColor.Id, Is.EqualTo(editedFrameColor.Id));
			Expect(persistedFrameColor.Name, Is.EqualTo(editedFrameColor.Name));
		}

		[Test]
		public void Can_delete_persisted_framecolor_without_connections()
		{
			//Arrange
			var frameColorWithoutConnections = FrameColorFactory.GetFrameColor();
			FrameColorRepository.Save(frameColorWithoutConnections);

			//Act
			FrameColorRepository.Delete(frameColorWithoutConnections);
			var persistedFrameColor = FrameColorValidationRepository.Get(frameColorWithoutConnections.Id);
			
			//Assert
			Expect(persistedFrameColor, Is.Null);

		}

		[Test]
		public void Cannot_delete_persisted_framecolor_with_connections()
		{
			//Arrange

			//Act
			
			//Assert
			Expect(() => FrameColorRepository.Delete(SavedFrameColors.First()), Throws.InstanceOf<SynologenDeleteItemHasConnectionsException>());
		}

	}

	[TestFixture, Category("TestFrameColors")]
	public class Given_multiple_framecolors : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_paged()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 5;
			var criteria = new PagedSortedCriteria<FrameColor>
			{
				OrderBy = null,
				Page = 1,
				PageSize = expectedNumberOfItemsMatchingCriteria,
				SortAscending = true
			} as PagedSortedCriteria;

			//Act
			var frameColorsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(frameColorsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_sorted_by_id()
		{
			//Arrange
			var criteria = new PagedSortedCriteria<FrameColor>
			{
				OrderBy = "Id",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
			//Assert
			itemsMatchingCriteria.ShouldBeOrderedAscendingBy(x => x.Id);
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_sorted_by_name()
		{
			//Arrange
			var criteria = new PagedSortedCriteria<FrameColor>
			{
				OrderBy = "Name",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
			//Assert
			itemsMatchingCriteria.ShouldBeOrderedAscendingBy(x => x.Name);
			
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_sorted_descending()
		{
			//Arrange
			var criteria = new PagedSortedCriteria<FrameColor>
			{
				OrderBy = "Id",
				Page = 1,
				PageSize = 100,
				SortAscending = false
			} as PagedSortedCriteria;

			//Act
			var itemsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
			//Assert
			itemsMatchingCriteria.ShouldBeOrderedDescendingBy(x => x.Id);
		}

	}
}