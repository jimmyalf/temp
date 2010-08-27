using System;
using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories
{
	public static class RepositoryFactory
	{
		public static IFrameRepository GetFrameRepository()
		{
			return new MockedFrameRepository();
		}

		public static IFrameColorRepository GetFrameColorRepository()
		{
			return new MockedFrameColorRepository();
		}

		public static IFrameBrandRepository GetFrameBrandRepository()
		{
			return new MockedFrameBrandRepository();
		}

		public static IFrameGlassTypeRepository GetFrameGlassTypeRepository()
		{
			return new MockedFrameGlassTypeRepository();
		}

		public static IFrameOrderRepository GetFrameOrderRepository()
		{
			return new MockedFrameOrderRepository();
		}

		public static Frame GetMockedFrame(int id)
		{
			var mockedStock = new Mock<FrameStock>();
			mockedStock.SetupGet(x => x.StockAtStockDate).Returns(200);
			mockedStock.SetupGet(x => x.CurrentStock).Returns(196);
			mockedStock.SetupGet(x => x.StockDate).Returns(new DateTime(2010, 08, 01, 0, 0, 0));
		    return new Frame
		    {
		        AllowOrders = true,
		        ArticleNumber = "123456",
		        Brand = new FrameBrand {Id = 1, Name = "Björn Borg"},
		        Color = new FrameColor {Id = 1, Name = "Blå"},
		        Id = id,
		        Name = "Bra båge",
                //Stock = new FrameStock{StockAtStockDate=200, StockDate = new DateTime(2010,08,01,0,0,0)}
				Stock = mockedStock.Object
		    }.SetInterval(x => x.PupillaryDistance, 20, 40, 0.5m);		
		}

		public static FrameColor GetMockedFrameColor(int id)
		{
		    return new FrameColor
		    {
		        Id = id,
		        Name = "Brun",
		    };
					
		}

		public static FrameBrand GetMockedFrameBrand(int id)
		{
		    return new FrameBrand
		    {
		        Id = id,
		        Name = "Björn borg",
		    };
					
		}

		public static FrameGlassType GetMockedFrameGlass(int id)
		{
		    return new FrameGlassType
		    {
		        Id = id,
		        Name = "Närprogressiv",
		        IncludeAdditionParametersInOrder = true,
		        IncludeHeightParametersInOrder = true
		    };
					
		}

		public static FrameOrder GetMockedFrameOrder(int id)
		{
			return new FrameOrder {
				Addition = new EyeParameter { Left = 1.75M, Right = 2.25M },
				Axis = new EyeParameter { Left = 70, Right = 155 },
				Created = new DateTime(2010, 08, 24, 13, 45, 0),
				Cylinder = new EyeParameter { Left = 0.60M, Right = 1.55M },
				Frame = GetMockedFrame(1),
				GlassType = GetMockedFrameGlass(1),
				Height = new EyeParameter { Left = 19, Right = 26 },
				OrderingShop = GetMockedShop(1),
				PupillaryDistance = new EyeParameter { Left = 22, Right = 38 },
				Sent = new DateTime(2010, 08, 24, 13, 45, 0),
				Sphere = new EyeParameter { Left = -5.25M, Right = 2.75M },
                Notes = "Leverans helst innan fredag."
			};
		}

		private static Shop GetMockedShop(int id) {
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


		internal class MockedFrameGlassTypeRepository : GenericMockRepository<FrameGlassType>, IFrameGlassTypeRepository
		{
			public MockedFrameGlassTypeRepository() : base(GetMockedFrameGlass) {}
		}

		internal class MockedFrameBrandRepository : GenericMockRepository<FrameBrand>, IFrameBrandRepository
		{
			public MockedFrameBrandRepository() : base(GetMockedFrameBrand) {}
		}

		internal class MockedFrameColorRepository : GenericMockRepository<FrameColor>, IFrameColorRepository
		{
			public MockedFrameColorRepository() : base(GetMockedFrameColor) {}
		}

		internal class MockedFrameRepository : GenericMockRepository<Frame>, IFrameRepository
		{
			public MockedFrameRepository() : base(GetMockedFrame) {}
		}

		internal class MockedFrameOrderRepository : GenericMockRepository<FrameOrder>, IFrameOrderRepository
		{
			public MockedFrameOrderRepository() : base(GetMockedFrameOrder) {}
		}

		

		public class GenericMockRepository<TModel> : IRepository<TModel> where TModel : class
		{
			public TModel SavedEntity { get; private set; }
			public TModel DeletedEntity { get; private set; }
			private readonly Func<int, TModel> _generateMockFunction;
			public GenericMockRepository(Func<int, TModel> generateMockFunction)
			{
				_generateMockFunction = generateMockFunction;
			}
			public TModel Get(int id) { return _generateMockFunction(id); }
			public IEnumerable<TModel> GetAll() {  return GenerateItems<TModel>(Get); }
			public IEnumerable<TModel> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { return GetAll(); }
			public void Save(TModel entity) { SavedEntity = entity; }
			public void Delete(TModel entity)
			{
				var id = TryGetId(entity);
				if(id.HasValue && id.Value <= 0) throw new SynologenDeleteItemHasConnectionsException("Mocked message");
				DeletedEntity = entity;
			}
		}

		private static int? TryGetId<TModel>(TModel entity)
		{
			try{
				var propertyInfo = typeof(TModel).GetProperty("Id");
				if(propertyInfo == null) return null;
				var value = propertyInfo.GetValue(entity, null);
				return (value is int) ? (int) value : (int?) null;
			}
			catch{ return null; }
		}


		private static ISortedPagedList<TModel> GenerateItems<TModel>(Func<int,TModel> generateFromIdFunction)
		{
			var returnList = new List<TModel>();
			for(var id=1; id <= 10; id++)
			{
				returnList.Add(generateFromIdFunction(id));
			}
			return new SortedPagedList<TModel>(returnList, 20, 1, 10, null, false);
		}

	}

}