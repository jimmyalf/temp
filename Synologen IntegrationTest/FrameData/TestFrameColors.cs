using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData
{
	[TestFixture]
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
			const int expectedNumberOfFrameConnections = 6;

			//Act
			var savedFrameColor = SavedFrameColors.First();
			var persistedFrameColor = FrameColorValidationRepository.Get(savedFrameColor.Id);
			
			//Assert
			Expect(persistedFrameColor, Is.Not.Null);
			Expect(persistedFrameColor.Id, Is.EqualTo(savedFrameColor.Id));
			Expect(persistedFrameColor.Name, Is.EqualTo(savedFrameColor.Name));
			Expect(persistedFrameColor.NumberOfFramesWithThisColor, Is.EqualTo(expectedNumberOfFrameConnections));
		}

		[Test]
		public void Can_edit_persisted_framecolor()
		{
		    //Arrange
		    const int expectedNumberOfFrameConnections = 6;

		    //Act
		    var editedFrameColor = Factories.FrameColorFactory.ScrabmleFrameColor(SavedFrameColors.First());
		    FrameColorRepository.Save(editedFrameColor);
		    var persistedFrameColor = FrameColorValidationRepository.Get(SavedFrameColors.First().Id);
			
		    //Assert
		    Expect(persistedFrameColor, Is.Not.Null);
			Expect(persistedFrameColor.Id, Is.EqualTo(editedFrameColor.Id));
			Expect(persistedFrameColor.Name, Is.EqualTo(editedFrameColor.Name));
			Expect(persistedFrameColor.NumberOfFramesWithThisColor, Is.EqualTo(expectedNumberOfFrameConnections));
		}

		[Test]
		public void Can_delete_persisted_framecolor_without_connections()
		{
		    //Arrange
			var frameColorWithoutConnections = Factories.FrameColorFactory.GetFrameColor();
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

	[TestFixture]
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
		    var criteria = new PageOfFrameColorsMatchingCriteria
		    {
		        OrderBy = null,
		        Page = 1,
		        PageSize = expectedNumberOfItemsMatchingCriteria,
		        SortAscending = true
		    };

		    //Act
		    var frameColorsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(frameColorsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(frameColorsMatchingCriteria.First().Name, Is.EqualTo("Svart"));
		    Expect(frameColorsMatchingCriteria.Last().Name, Is.EqualTo("Brun"));
			
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_sorted_by_id()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 6;
		    var criteria = new PageOfFrameColorsMatchingCriteria
		    {
		        OrderBy = "Id",
		        Page = 1,
		        PageSize = 100,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Svart"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Silver"));
			
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_sorted_by_name()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 6;
		    var criteria = new PageOfFrameColorsMatchingCriteria
		    {
		        OrderBy = "Name",
		        Page = 1,
		        PageSize = 100,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Blå"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Svart"));
			
		}

		[Test]
		public void Can_get_colors_by_PageOfFrameColorsMatchingCriteria_sorted_descending()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 6;
		    var criteria = new PageOfFrameColorsMatchingCriteria
		    {
		        OrderBy = "Id",
		        Page = 1,
		        PageSize = 100,
		        SortAscending = false
		    };

		    //Act
		    var itemsMatchingCriteria = FrameColorValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Silver"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Svart"));
		}

	}
}