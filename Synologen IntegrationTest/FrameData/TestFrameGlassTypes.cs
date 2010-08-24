using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData
{
	[TestFixture]
	public class Given_a_frameglasstype : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_persisted_frameglasstype()
		{
		    //Arrange

		    //Act
		    var savedFrameGlassType = SavedFrameGlassTypes.First();
		    var persistedFrameGlassType = FrameGlassTypeValidationRepository.Get(savedFrameGlassType.Id);
			
		    //Assert
		    Expect(persistedFrameGlassType, Is.Not.Null);
		    Expect(persistedFrameGlassType.Id, Is.EqualTo(savedFrameGlassType.Id));
		    Expect(persistedFrameGlassType.Name, Is.EqualTo(savedFrameGlassType.Name));
			Expect(persistedFrameGlassType.IncludeAdditionParametersInOrder, Is.EqualTo(savedFrameGlassType.IncludeAdditionParametersInOrder));
			Expect(persistedFrameGlassType.IncludeHeightParametersInOrder, Is.EqualTo(savedFrameGlassType.IncludeHeightParametersInOrder));
		}

		[Test]
		public void Can_edit_persisted_frameglasstype()
		{
		    //Arrange

		    //Act
		    var editedFrameGlassType = Factories.FrameGlassTypeFactory.ScrabmleFrameGlass(SavedFrameGlassTypes.First());
		    FrameGlassTypeRepository.Save(editedFrameGlassType);
		    var persistedFrameGlassType = FrameGlassTypeValidationRepository.Get(SavedFrameGlassTypes.First().Id);
			
		    //Assert
		    Expect(persistedFrameGlassType, Is.Not.Null);
		    Expect(persistedFrameGlassType.Id, Is.EqualTo(editedFrameGlassType.Id));
		    Expect(persistedFrameGlassType.Name, Is.EqualTo(editedFrameGlassType.Name));
			Expect(persistedFrameGlassType.IncludeAdditionParametersInOrder, Is.EqualTo(editedFrameGlassType.IncludeAdditionParametersInOrder));
			Expect(persistedFrameGlassType.IncludeHeightParametersInOrder, Is.EqualTo(editedFrameGlassType.IncludeHeightParametersInOrder));
		}

		[Test]
		public void Can_delete_persisted_frameglasstype()
		{
		    //Arrange
			var frameGlassType = Factories.FrameGlassTypeFactory.GetGlassType();
		    FrameGlassTypeRepository.Save(frameGlassType);

		    //Act
		    FrameGlassTypeRepository.Delete(frameGlassType);
		    var persistedFrameGlassType = FrameGlassTypeValidationRepository.Get(frameGlassType.Id);
			
		    //Assert
		    Expect(persistedFrameGlassType, Is.Null);

		}
	}

	[TestFixture]
	public class Given_multiple_frameglassTypes : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_frameglasstypes_by_PageOfFrameGlassTypesMatchingCriteria_paged()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 3;
		    var criteria = new PageOfFrameGlassTypesMatchingCriteria
		    {
		        OrderBy = null,
		        Page = 1,
		        PageSize = expectedNumberOfItemsMatchingCriteria,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameGlassTypeValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Enstyrke"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Rumprogressiva"));
			
		}

		[Test]
		public void Can_get_frameglasstypes_by_PageOfFrameGlassTypesMatchingCriteria_sorted_by_id()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 4;
		    var criteria = new PageOfFrameGlassTypesMatchingCriteria
		    {
		        OrderBy = "Id",
		        Page = 1,
		        PageSize = 100,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameGlassTypeValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Enstyrke"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Progressiva"));
			
		}

		[Test]
		public void Can_get_brands_by_PageOfFrameGlassTypesMatchingCriteria_sorted_by_name()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 4;
		    var criteria = new PageOfFrameGlassTypesMatchingCriteria
		    {
		        OrderBy = "Name",
		        Page = 1,
		        PageSize = 100,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameGlassTypeValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Enstyrke"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Rumprogressiva"));	
		}

		[Test]
		public void Can_get_brands_by_PageOfFrameGlassTypesMatchingCriteria_sorted_descending()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 4;
		    var criteria = new PageOfFrameGlassTypesMatchingCriteria
		    {
		        OrderBy = "Id",
		        Page = 1,
		        PageSize = 100,
		        SortAscending = false
		    };

		    //Act
		    var itemsMatchingCriteria = FrameGlassTypeValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		    Expect(itemsMatchingCriteria.First().Name, Is.EqualTo("Progressiva"));
		    Expect(itemsMatchingCriteria.Last().Name, Is.EqualTo("Enstyrke"));
		}
	}
}