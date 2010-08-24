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


		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_paged()
		{
		    //Arrange
		    const int expectedNumberOfFramesMatchingCriteria = 3;
		    var criteria = new PageOfFrameColorsMatchingCriteria
		    {
		        OrderBy = null,
		        Page = 1,
		        PageSize = expectedNumberOfFramesMatchingCriteria,
		        SortAscending = true
		    };

		    //Act
		    var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
		    Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 1"));
		    Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 5"));
			
		}

		//[Test]
		//public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_by_frame_id()
		//{
		//    //Arrange
		//    const int expectedNumberOfFramesMatchingCriteria = 36;
		//    var criteria = new PageOfFramesMatchingCriteria
		//    {
		//        NameLike = null,
		//        OrderBy = "Id",
		//        Page = 1,
		//        PageSize = 100,
		//        SortAscending = true
		//    };

		//    //Act
		//    var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
		//    //Assert
		//    Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
		//    Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 1"));
		//    Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 36"));
			
		//}

		//[Test]
		//public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_by_frame_name()
		//{
		//    //Arrange
		//    const int expectedNumberOfFramesMatchingCriteria = 36;
		//    var criteria = new PageOfFramesMatchingCriteria
		//    {
		//        NameLike = null,
		//        OrderBy = "Name",
		//        Page = 1,
		//        PageSize = 100,
		//        SortAscending = true
		//    };

		//    //Act
		//    var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
		//    //Assert
		//    Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
		//    Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 1"));
		//    Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 9"));
			
		//}

		//[Test]
		//public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_by_color_name()
		//{
		//    //Arrange
		//    const int expectedNumberOfFramesMatchingCriteria = 36;
		//    var criteria = new PageOfFramesMatchingCriteria
		//    {
		//        NameLike = null,
		//        OrderBy = "Color.Name",
		//        Page = 1,
		//        PageSize = 100,
		//        SortAscending = true
		//    };

		//    //Act
		//    var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
		//    //Assert
		//    Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
		//    Expect(framesMatchingCriteria.First().Color.Name, Is.EqualTo("Blå"));
		//    Expect(framesMatchingCriteria.Last().Color.Name, Is.EqualTo("Svart"));
			
		//}

		//[Test]
		//public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_descending()
		//{
		//    //Arrange
		//    const int expectedNumberOfFramesMatchingCriteria = 36;
		//    var criteria = new PageOfFramesMatchingCriteria
		//    {
		//        NameLike = null,
		//        OrderBy = "Id",
		//        Page = 1,
		//        PageSize = 100,
		//        SortAscending = false
		//    };

		//    //Act
		//    var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
		//    //Assert
		//    Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
		//    Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 36"));
		//    Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 1"));
			
		//}

	}
}