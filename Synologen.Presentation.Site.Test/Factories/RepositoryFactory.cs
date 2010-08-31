using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.Factories
{
	public static class RepositoryFactory
	{
		
		public static IFrameRepository GetFrameRepository()
		{
			return new MockedFrameRepository();
		}

		public static IFrameGlassTypeRepository GetFrameGlassRepository()
		{
			return new MockedFrameGlassTypeRepository();
		}

		public static IFrameOrderRepository GetFramOrderRepository()
		{
			return new MockedFramOrderRepository();
		}

		public static IShopRepository GetShopRepository() 
		{
			return new MockedShopRepository();
		}

		internal class MockedFrameRepository : MockedBaseClass<Frame>, IFrameRepository
		{
			public override Frame Get(int id)
			{
				return new Frame
				{
					AllowOrders = true,
					ArticleNumber = "123456",
					Brand = new FrameBrand {Id = 1, Name = "Björn Borg"},
					Color = new FrameColor {Id = 1, Name = "Blå"},
					Id = id,
					Name = "Bra båge",
				}.SetInterval(x => x.PupillaryDistance, 20, 40, 1);
			}
		}

		internal class MockedFrameGlassTypeRepository : MockedBaseClass<FrameGlassType>,IFrameGlassTypeRepository {
			public override FrameGlassType Get(int id)
			{
				return new FrameGlassType
				{
					Id = id,
					IncludeAdditionParametersInOrder = (id%2==0),
					IncludeHeightParametersInOrder = (id%4==0),
					Name = "Närprogressiva",
				};
			}
		}

		internal class MockedFramOrderRepository : MockedBaseClass<FrameOrder>, IFrameOrderRepository
		{
			//public override FrameOrder Get(int id)
			//{
			//    return new FrameOrder {
			//        Addition = new EyeParameter { Left = 1.75M, Right = 2.25M },
			//        Axis = new EyeParameter { Left = 70, Right = 155 },
			//        Created = new DateTime(2010, 08, 24, 13, 45, 0),
			//        Cylinder = new EyeParameter { Left = 0.60M, Right = 1.55M },
			//        Frame = GetMockedFrame(1),
			//        GlassType = GetMockedFrameGlass(1),
			//        Height = new EyeParameter { Left = 19, Right = 26 },
			//        OrderingShop = GetMockedShop(1),
			//        PupillaryDistance = new EyeParameter { Left = 22, Right = 38 },
			//        Sent = new DateTime(2010, 08, 24, 13, 45, 0),
			//        Sphere = new EyeParameter { Left = -5.25M, Right = 2.75M },
			//        Notes = "Leverans helst innan fredag."
			//    };
			//}
		}

		internal class MockedShopRepository : MockedBaseClass<Shop>,IShopRepository
		{
			public override Shop Get(int id)
			{
				return new Shop {
				Address = new ShopAddress {
					AddressLineOne = null,
					AddressLineTwo = "Datavägen 2",
					City = "Askim",
					PostalCode = "43632"
				},
				Id = id,
				Name = "Testbutiken i Askim"
			};
			}
		}

		internal class MockedBaseClass<TEntity> : IRepository<TEntity> where TEntity : class
		{
			public TEntity SavedItem { get; private set; }
			public virtual TEntity Get(int id){ throw new NotImplementedException();}
			public IEnumerable<TEntity> GetAll() { return GenerateItems<TEntity>(Get); }
			public IEnumerable<TEntity> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { return GetAll(); }
			public void Save(TEntity entity) { SavedItem = entity; }
			public void Delete(TEntity entity) { throw new NotImplementedException(); }
		}


		//private static int? TryGetId<TModel>(TModel entity)
		//{
		//    try{
		//        var propertyInfo = typeof(TModel).GetProperty("Id");
		//        if(propertyInfo == null) return null;
		//        var value = propertyInfo.GetValue(entity, null);
		//        return (value is int) ? (int) value : (int?) null;
		//    }
		//    catch{ return null; }
		//}


		private static IEnumerable<TModel> GenerateItems<TModel>(Func<int,TModel> generateFromIdFunction)
		{
			var returnList = new List<TModel>();
			for(var id=1; id <= 10; id++)
			{
				returnList.Add(generateFromIdFunction(id));
			}
			return returnList;
		}
	}

	
}