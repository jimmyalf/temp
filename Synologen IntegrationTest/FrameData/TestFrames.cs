using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData
{
	[TestFixture]
	public class Given_a_frame : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_persisted_frame()
		{
			//Arrange
			const int expectedNumberOfFrameConnections = 6;
			const int expectedNumberOfOrderConnections = 4;

			//Act
			var savedFrame = SavedFrames.First();
			var persistedFrame = FrameValidationRepository.Get(savedFrame.Id);
			
			//Assert
			Expect(persistedFrame, Is.Not.Null);
			Expect(persistedFrame.Id, Is.GreaterThan(0));
			Expect(persistedFrame.AllowOrders, Is.EqualTo(savedFrame.AllowOrders));
			Expect(persistedFrame.ArticleNumber, Is.EqualTo(savedFrame.ArticleNumber));
			Expect(persistedFrame.Brand.Id, Is.EqualTo(savedFrame.Brand.Id));
			Expect(persistedFrame.Brand.Name, Is.EqualTo(savedFrame.Brand.Name));
			Expect(persistedFrame.Brand.NumberOfFramesWithThisBrand, Is.EqualTo(expectedNumberOfFrameConnections));
			Expect(persistedFrame.Color.Id, Is.EqualTo(savedFrame.Color.Id));
			Expect(persistedFrame.Color.Name, Is.EqualTo(savedFrame.Color.Name));
			Expect(persistedFrame.Color.NumberOfFramesWithThisColor, Is.EqualTo(expectedNumberOfFrameConnections));
			Expect(persistedFrame.Id, Is.EqualTo(savedFrame.Id));
			Expect(persistedFrame.Name, Is.EqualTo(savedFrame.Name));
			Expect(persistedFrame.PupillaryDistance.Increment, Is.EqualTo(savedFrame.PupillaryDistance.Increment));
			Expect(persistedFrame.PupillaryDistance.Max, Is.EqualTo(savedFrame.PupillaryDistance.Max));
			Expect(persistedFrame.PupillaryDistance.Min, Is.EqualTo(savedFrame.PupillaryDistance.Min));
			Expect(persistedFrame.NumberOfConnectedOrdersWithThisFrame, Is.EqualTo(expectedNumberOfOrderConnections));
		}

		[Test]
		public void Can_edit_persisted_frame()
		{
			//Arrange
			const int expectedNumberOfFrameConnections = 6;
			const int expectedNumberOfOrderConnections = 4;

			//Act
			var editedFrame = Factories.FrameFactory.ScrabmleFrame(SavedFrames.First());
			FrameRepository.Save(editedFrame);
			var persistedFrame = FrameValidationRepository.Get(SavedFrames.First().Id);
			
			//Assert
			Expect(persistedFrame, Is.Not.Null);
			Expect(persistedFrame.Id, Is.GreaterThan(0));
			Expect(persistedFrame.AllowOrders, Is.EqualTo(editedFrame.AllowOrders));
			Expect(persistedFrame.ArticleNumber, Is.EqualTo(editedFrame.ArticleNumber));
			Expect(persistedFrame.Brand.Id, Is.EqualTo(editedFrame.Brand.Id));
			Expect(persistedFrame.Brand.Name, Is.EqualTo(editedFrame.Brand.Name));
			Expect(persistedFrame.Brand.NumberOfFramesWithThisBrand, Is.EqualTo(expectedNumberOfFrameConnections));
			Expect(persistedFrame.Color.Id, Is.EqualTo(editedFrame.Color.Id));
			Expect(persistedFrame.Color.Name, Is.EqualTo(editedFrame.Color.Name));
			Expect(persistedFrame.Color.NumberOfFramesWithThisColor, Is.EqualTo(expectedNumberOfFrameConnections));
			Expect(persistedFrame.Id, Is.EqualTo(editedFrame.Id));
			Expect(persistedFrame.Name, Is.EqualTo(editedFrame.Name));
			Expect(persistedFrame.PupillaryDistance.Increment, Is.EqualTo(editedFrame.PupillaryDistance.Increment));
			Expect(persistedFrame.PupillaryDistance.Max, Is.EqualTo(editedFrame.PupillaryDistance.Max));
			Expect(persistedFrame.PupillaryDistance.Min, Is.EqualTo(editedFrame.PupillaryDistance.Min));
			Expect(persistedFrame.NumberOfConnectedOrdersWithThisFrame, Is.EqualTo(expectedNumberOfOrderConnections));
		}

		[Test]
		public void Can_delete_persisted_frame_without_connections()
		{
		    //Arrange
			var frameWithoutConnections = Factories.FrameFactory.GetFrame(SavedFrameBrands.First(), SavedFrameColors.First());
			FrameRepository.Save(frameWithoutConnections);

		    //Act
		    FrameRepository.Delete(frameWithoutConnections);
		    var persistedFrame = FrameValidationRepository.Get(frameWithoutConnections.Id);
			
		    //Assert
		    Expect(persistedFrame, Is.Null);
		}

		[Test]
		public void Cannot_delete_persisted_frame_with_connections()
		{
			//Arrange

			//Act
			
		    //Assert
			Expect(() => FrameRepository.Delete(SavedFrames.First()), Throws.InstanceOf<SynologenDeleteItemHasConnectionsException>());

		}
	}

	[TestFixture]
	public class Given_multiple_frames : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_filtered_by_frame_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 11;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = "Testbåge 1",
				OrderBy = null,
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var frame in framesMatchingCriteria)
			{
				Expect(frame.Name, ContainsSubstring(criteria.NameLike));
			}
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_filtered_by_color_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 6;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = "Blå",
				OrderBy = null,
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var frame in framesMatchingCriteria)
			{
				Expect(frame.Color.Name, Is.EqualTo(criteria.NameLike));
			}
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_filtered_by_brand_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 6;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = "Quicksilver",
				OrderBy = null,
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var frame in framesMatchingCriteria)
			{
				Expect(frame.Brand.Name, Is.EqualTo(criteria.NameLike));
			}
			
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_filtered_by_articlenumber()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 11;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = "98765-1",
				OrderBy = null,
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var frame in framesMatchingCriteria)
			{
				Expect(frame.ArticleNumber, ContainsSubstring(criteria.NameLike));
			}
			
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_paged()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 5;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = null,
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

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_by_frame_id()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 36;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = null,
				OrderBy = "Id",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 1"));
			Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 36"));
			
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_by_frame_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 36;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = null,
				OrderBy = "Name",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 1"));
			Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 9"));
			
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_by_color_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 36;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = null,
				OrderBy = "Color.Name",
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			Expect(framesMatchingCriteria.First().Color.Name, Is.EqualTo("Blå"));
			Expect(framesMatchingCriteria.Last().Color.Name, Is.EqualTo("Svart"));
			
		}

		[Test]
		public void Can_get_frames_by_PageOfFramesMatchingCriteria_sorted_descending()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 36;
			var criteria = new PageOfFramesMatchingCriteria
			{
				NameLike = null,
				OrderBy = "Id",
				Page = 1,
				PageSize = 100,
				SortAscending = false
			};

			//Act
			var framesMatchingCriteria = FrameValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			Expect(framesMatchingCriteria.First().Name, Is.EqualTo("Testbåge 36"));
			Expect(framesMatchingCriteria.Last().Name, Is.EqualTo("Testbåge 1"));
			
		}

	}
}