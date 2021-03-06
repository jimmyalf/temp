using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Data.Test.FrameData.Factories;
using Spinit.Wpc.Synologen.Integration.Data.Test.FrameData;
using Spinit.Wpc.Synologen.Test.Core;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData
{
	[TestFixture, Category("TestFrameOrders")]
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
			var persistedFrameOrder = FrameOrderValidationRepository.Get(savedFrameOrder.Id);
			
			//Assert
			Expect(persistedFrameOrder, Is.Not.Null);
			Expect(persistedFrameOrder.Id, Is.EqualTo(savedFrameOrder.Id));
			Expect(persistedFrameOrder.Addition.Left, Is.EqualTo(savedFrameOrder.Addition.Left));
			Expect(persistedFrameOrder.Addition.Right, Is.EqualTo(savedFrameOrder.Addition.Right));
			Expect(persistedFrameOrder.Axis.Left, Is.EqualTo(savedFrameOrder.Axis.Left));
			Expect(persistedFrameOrder.Axis.Right, Is.EqualTo(savedFrameOrder.Axis.Right));
			Expect(persistedFrameOrder.Created, Is.EqualTo(savedFrameOrder.Created));
			Expect(persistedFrameOrder.Cylinder.Left, Is.EqualTo(savedFrameOrder.Cylinder.Left));
			Expect(persistedFrameOrder.Cylinder.Right, Is.EqualTo(savedFrameOrder.Cylinder.Right));
			Expect(persistedFrameOrder.Frame.Id, Is.EqualTo(savedFrameOrder.Frame.Id));
			Expect(persistedFrameOrder.GlassType.Id, Is.EqualTo(savedFrameOrder.GlassType.Id));
			Expect(persistedFrameOrder.Height.Left, Is.EqualTo(savedFrameOrder.Height.Left));
			Expect(persistedFrameOrder.Height.Right, Is.EqualTo(savedFrameOrder.Height.Right));
			Expect(persistedFrameOrder.IsSent, Is.EqualTo(savedFrameOrder.IsSent));
			Expect(persistedFrameOrder.OrderingShop.Id, Is.EqualTo(savedFrameOrder.OrderingShop.Id));
			Expect(persistedFrameOrder.PupillaryDistance.Left, Is.EqualTo(savedFrameOrder.PupillaryDistance.Left));
			Expect(persistedFrameOrder.PupillaryDistance.Right, Is.EqualTo(savedFrameOrder.PupillaryDistance.Right));
			Expect(persistedFrameOrder.Sent, Is.EqualTo(savedFrameOrder.Sent));
			Expect(persistedFrameOrder.Sphere.Left, Is.EqualTo(savedFrameOrder.Sphere.Left));
			Expect(persistedFrameOrder.Sphere.Right, Is.EqualTo(savedFrameOrder.Sphere.Right));
			Expect(persistedFrameOrder.Reference, Is.EqualTo(savedFrameOrder.Reference));            
		}

		[Test]
		public void Can_save_frame_order_with_nullable_values()
		{
			//Arrange
			var orderToSave = FrameOrderFactory.GetFrameOrder(SavedFrames.First(), SavedFrameGlassTypes.First(), SavedShop, SavedFrameSuppliers.First());
			orderToSave.Sent = null;
			orderToSave.Addition = null;
			orderToSave.Height = null;
			orderToSave.Reference = null;

			//Act
			
			//Assert
			Assert.DoesNotThrow(() => FrameOrderValidationRepository.Save(orderToSave));
		}

		[Test]
		public void Can_edit_persisted_frame_order()
		{
			//Arrange
			var editedFrameOrder = FrameOrderFactory.ScrabmleFrameOrder(SavedFrameOrders.First());

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
			Expect(persistedFrameOrder.Reference, Is.EqualTo(editedFrameOrder.Reference));           
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

	[TestFixture, Category("TestFrameOrders")]
	public class Given_multiple_frame_orders : TestBase
	{
		[SetUp]
		public void Context()
		{
			SetupDefaultContext();
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_filtered_by_glass_type()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 36;
			var criteria = new PageOfFrameOrdersMatchingCriteria
			{
				Search = "Enstyrke",
				OrderBy = null,
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var order in framesMatchingCriteria)
			{
				Expect(order.GlassType.Name, ContainsSubstring(criteria.Search));
			}
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_filtered_by_shop_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 144;
			var criteria = new PageOfFrameOrdersMatchingCriteria
			{
				Search = SavedShop.Name,
				OrderBy = null,
				Page = 1,
				PageSize = 200,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var order in framesMatchingCriteria)
			{
				Expect(order.OrderingShop.Name, ContainsSubstring(criteria.Search));
			}
		}

		[Test]
		public void Can_get_frame_orders_by_PageOfFrameOrdersMatchingCriteria_filtered_by_frame_name()
		{
			//Arrange
			const int expectedNumberOfFramesMatchingCriteria = 4;
			var criteria = new PageOfFrameOrdersMatchingCriteria
			{
				Search = "Testb�ge 36",
				OrderBy = null,
				Page = 1,
				PageSize = 100,
				SortAscending = true
			};

			//Act
			var framesMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(framesMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfFramesMatchingCriteria));
			foreach (var order in framesMatchingCriteria)
			{
				Expect(order.Frame.Name, ContainsSubstring(criteria.Search));
			}
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
			itemsMatchingCriteria.ShouldBeOrderedAscendingBy(x => x.Id);
			
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
			itemsMatchingCriteria.ShouldBeOrderedAscendingBy(x => x.Frame.Name);
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
			itemsMatchingCriteria.ShouldBeOrderedAscendingBy(x => x.GlassType.Name);
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
			itemsMatchingCriteria.ShouldBeOrderedAscendingBy(x => x.OrderingShop.Name);
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
			itemsMatchingCriteria.ShouldBeOrderedDescendingBy(x => x.Id);
		}

		[Test]
		public void Can_get_frame_orders_by_AllFrameOrdersForShopCriteria()
		{
			//Arrange
			const int expectedNumberOfItemsMatchingCriteria = 144;
			var criteria = new AllFrameOrdersForShopCriteria
			{
				ShopId = SavedShop.Id
			};

			//Act
			var itemsMatchingCriteria = FrameOrderValidationRepository.FindBy(criteria);
			
			//Assert
			Expect(itemsMatchingCriteria.Count(), Is.EqualTo(expectedNumberOfItemsMatchingCriteria));
			Expect(itemsMatchingCriteria.First().OrderingShop.Id, Is.EqualTo(SavedShop.Id));
			Expect(itemsMatchingCriteria.Last().OrderingShop.Id, Is.EqualTo(SavedShop.Id));
		}


	}
}