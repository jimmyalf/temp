using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData
{
	[TestFixture]
	public class Given_a_frame_order: TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_persisted_frame_order()
		{
			//Arrange
			var savedFrameOrder = SavedFrameOrders.First();

			//Act
			var persistedFrameColor = FrameOrderValidationRepository.Get(savedFrameOrder.Id);
			
			//Assert
			Expect(persistedFrameColor, Is.Not.Null);
			Expect(persistedFrameColor.Id, Is.EqualTo(savedFrameOrder.Id));
			Expect(persistedFrameColor.Addition.Left, Is.EqualTo(savedFrameOrder.Addition.Left));
			Expect(persistedFrameColor.Addition.Right, Is.EqualTo(savedFrameOrder.Addition.Right));
			Expect(persistedFrameColor.Axis.Left, Is.EqualTo(savedFrameOrder.Axis.Left));
			Expect(persistedFrameColor.Axis.Right, Is.EqualTo(savedFrameOrder.Axis.Right));
			Expect(persistedFrameColor.Created, Is.EqualTo(savedFrameOrder.Created));
			Expect(persistedFrameColor.Cylinder.Left, Is.EqualTo(savedFrameOrder.Cylinder.Left));
			Expect(persistedFrameColor.Cylinder.Right, Is.EqualTo(savedFrameOrder.Cylinder.Right));
			Expect(persistedFrameColor.Frame.Id, Is.EqualTo(savedFrameOrder.Frame.Id));
			Expect(persistedFrameColor.GlassType.Id, Is.EqualTo(savedFrameOrder.GlassType.Id));
			Expect(persistedFrameColor.Height.Left, Is.EqualTo(savedFrameOrder.Height.Left));
			Expect(persistedFrameColor.Height.Right, Is.EqualTo(savedFrameOrder.Height.Right));
			Expect(persistedFrameColor.IsSent, Is.EqualTo(savedFrameOrder.IsSent));
			Expect(persistedFrameColor.OrderingShop.Id, Is.EqualTo(savedFrameOrder.OrderingShop.Id));
			Expect(persistedFrameColor.PupillaryDistance.Left, Is.EqualTo(savedFrameOrder.PupillaryDistance.Left));
			Expect(persistedFrameColor.PupillaryDistance.Right, Is.EqualTo(savedFrameOrder.PupillaryDistance.Right));
			Expect(persistedFrameColor.Sent, Is.EqualTo(savedFrameOrder.Sent));
			Expect(persistedFrameColor.Sphere.Left, Is.EqualTo(savedFrameOrder.Sphere.Left));
			Expect(persistedFrameColor.Sphere.Right, Is.EqualTo(savedFrameOrder.Sphere.Right));
		}

		[Test]
		public void Can_edit_persisted_frame_order()
		{
			//Arrange
			var editedFrameOrder = Factories.FrameOrderFactory.ScrabmleFrameOrder(SavedFrameOrders.First());

			//Act
			
			FrameOrderRepository.Save(editedFrameOrder);
			var persistedFrameOrder = FrameOrderValidationRepository.Get(SavedFrameOrders.First().Id);
			
			//Assert
			Expect(persistedFrameOrder, Is.Not.Null);
			Expect(persistedFrameOrder.Id, Is.EqualTo(editedFrameOrder.Id));
			Expect(persistedFrameOrder.Addition.Left, Is.EqualTo(editedFrameOrder.Addition.Left));
			Expect(persistedFrameOrder.Addition.Right, Is.EqualTo(editedFrameOrder.Addition.Right));
			Expect(persistedFrameOrder.Axis.Left, Is.EqualTo(editedFrameOrder.Axis.Left));
			Expect(persistedFrameOrder.Axis.Right, Is.EqualTo(editedFrameOrder.Axis.Right));
			Expect(persistedFrameOrder.Created, Is.EqualTo(editedFrameOrder.Created));
			Expect(persistedFrameOrder.Cylinder.Left, Is.EqualTo(editedFrameOrder.Cylinder.Left));
			Expect(persistedFrameOrder.Cylinder.Right, Is.EqualTo(editedFrameOrder.Cylinder.Right));
			Expect(persistedFrameOrder.Frame.Id, Is.EqualTo(editedFrameOrder.Frame.Id));
			Expect(persistedFrameOrder.GlassType.Id, Is.EqualTo(editedFrameOrder.GlassType.Id));
			Expect(persistedFrameOrder.Height.Left, Is.EqualTo(editedFrameOrder.Height.Left));
			Expect(persistedFrameOrder.Height.Right, Is.EqualTo(editedFrameOrder.Height.Right));
			Expect(persistedFrameOrder.IsSent, Is.EqualTo(editedFrameOrder.IsSent));
			Expect(persistedFrameOrder.OrderingShop.Id, Is.EqualTo(editedFrameOrder.OrderingShop.Id));
			Expect(persistedFrameOrder.PupillaryDistance.Left, Is.EqualTo(editedFrameOrder.PupillaryDistance.Left));
			Expect(persistedFrameOrder.PupillaryDistance.Right, Is.EqualTo(editedFrameOrder.PupillaryDistance.Right));
			Expect(persistedFrameOrder.Sent, Is.EqualTo(editedFrameOrder.Sent));
			Expect(persistedFrameOrder.Sphere.Left, Is.EqualTo(editedFrameOrder.Sphere.Left));
			Expect(persistedFrameOrder.Sphere.Right, Is.EqualTo(editedFrameOrder.Sphere.Right));
		}

		[Test]
		public void Can_delete_persisted_frame_order()
		{
			//Arrange
			var savedFrameOrderId = SavedFrameOrders.First().Id;

			//Act
			FrameOrderRepository.Delete(SavedFrameOrders.First());
			var persistedFrame = FrameOrderValidationRepository.Get(savedFrameOrderId);
			
			//Assert
			Expect(persistedFrame, Is.Null);

		}
	}

	[TestFixture]
	public class Given_multiple_frame_orders : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_paged()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 9;
			var criteria = new PageOfFrameOrdersMatchingCriteria {
				OrderBy = null,
				Page = 1,
				PageSize = expectedNumberOfItemsMatchingCriteria,
				SortAscending = true
			};

			//Act
			var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);

			//Assert
			Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Frame.Id, Is.EqualTo(1));
			Expect(itemsMatchingCriteria.First().GlassType.Id, Is.EqualTo(1));
			Expect(itemsMatchingCriteria.Last().Frame.Id, Is.EqualTo(3));
			Expect(itemsMatchingCriteria.Last().GlassType.Id, Is.EqualTo(1));

		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_sorted_by_id()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 144;
		    var criteria = new PageOfFrameOrdersMatchingCriteria
		    {
		        OrderBy = "Id",
		        Page = 1,
		        PageSize = 200,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Frame.Id, Is.EqualTo(1));
			Expect(itemsMatchingCriteria.First().GlassType.Id, Is.EqualTo(1));
			Expect(itemsMatchingCriteria.Last().Frame.Id, Is.EqualTo(36));
			Expect(itemsMatchingCriteria.Last().GlassType.Id, Is.EqualTo(4));
			
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_sorted_by_frame_name()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 144;
		    var criteria = new PageOfFrameOrdersMatchingCriteria
		    {
		        OrderBy = "Frame.Name",
		        Page = 1,
		        PageSize = 200,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Frame.Name, Is.EqualTo("Testbåge 1"));
			Expect(itemsMatchingCriteria.Last().Frame.Name, Is.EqualTo("Testbåge 9"));
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_sorted_by_glass_type_name()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 144;
		    var criteria = new PageOfFrameOrdersMatchingCriteria
		    {
		        OrderBy = "GlassType.Name",
		        Page = 1,
		        PageSize = 200,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().GlassType.Name, Is.EqualTo("Enstyrke"));
			Expect(itemsMatchingCriteria.Last().GlassType.Name, Is.EqualTo("Rumprogressiva"));
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_sorted_by_shop_name()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 144;
		    var criteria = new PageOfFrameOrdersMatchingCriteria
		    {
		        OrderBy = "OrderingShop.Name",
		        Page = 1,
		        PageSize = 200,
		        SortAscending = true
		    };

		    //Act
		    var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_sorted_descending()
		{
		    //Arrange
		    const int expectedNumberOfItemsMatchingCriteria = 144;
		    var criteria = new PageOfFrameOrdersMatchingCriteria
		    {
		        OrderBy = "Id",
		        Page = 1,
		        PageSize = 200,
		        SortAscending = false
		    };

		    //Act
		    var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
		    //Assert
		    Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().Frame.Id, Is.EqualTo(36));
			Expect(itemsMatchingCriteria.First().GlassType.Id, Is.EqualTo(4));
			Expect(itemsMatchingCriteria.Last().Frame.Id, Is.EqualTo(1));
			Expect(itemsMatchingCriteria.Last().GlassType.Id, Is.EqualTo(1));
		}


	}
}