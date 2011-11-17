using System;
using System.Collections.Generic;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Factories
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
			return new MockedFrameOrderRepository();
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
					Brand = new FrameBrand { Id = 1, Name = "Björn Borg" },
					Color = new FrameColor { Id = 1, Name = "Blå" },
					Id = id,
					Name = "Bra båge",
				}
				.SetInterval(x => x.PupillaryDistance, 20, 40, 1);
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
				}
				.SetInterval(x => x.Sphere, -6, 6, 0.25M)
				.SetInterval(x => x.Cylinder, -2, 0, 0.25M);
			}
		}

		internal class MockedFrameOrderRepository : MockedBaseClass<FrameOrder>, IFrameOrderRepository
		{
			public override FrameOrder Get(int id)
			{
				if(id<=0) return null;
				return new FrameOrder {
					Axis = new NullableEyeParameter<int?> { Left = 70, Right = 155 },
					Created = new DateTime(2010, 08, 24, 13, 45, 0),
					Cylinder = new NullableEyeParameter { Left = -0.25M, Right = -1.75M },
					Frame = new Frame {
						ArticleNumber = "123987456",
						Brand = new FrameBrand { Name = "Björn Borg" },
						Color = new FrameColor { Name = "Gul" },
						Name = "Testbåge 123",
						PupillaryDistance = { Min = 20, Max = 40, Increment = 1 },
					},
					GlassType = new FrameGlassType { 
						Name = "Progressiv",
                        IncludeAdditionParametersInOrder = true,
                        IncludeHeightParametersInOrder = false,
						Sphere = { Min = -6, Max = 6, Increment = 0.25M },
						Cylinder = { Min = -2, Max = 0, Increment = 0.25M },
					},
					
					OrderingShop = new Shop {
						Id = 5,
						Name = "Bågbutiken AB",
						Address = new ShopAddress { City = "Stockholm" }
					},
					PupillaryDistance = new EyeParameter { Left = 22, Right = 38 },
					Sent = null,
					Sphere = new EyeParameter { Left = -5.25M, Right = 2.75M },
					Reference = "Leverans helst innan fredag.",
					Id = id,
					Addition = new NullableEyeParameter { Left = 1.75M, Right = 2.25M },
					Height = new NullableEyeParameter { Left = null, Right = null },
				};
			}
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
			private int _savedId;
			public TEntity SavedItem { get; private set; }
			public IActionCriteria GivenFindByActionCriteria { get; private set; }
			public virtual TEntity Get(int id){ throw new NotImplementedException();}
			public IEnumerable<TEntity> GetAll() { return GenerateItems<TEntity>(Get); }
			public IEnumerable<TEntity> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria
			{
				GivenFindByActionCriteria = criteria;
				return GetAll();
			}
			public void Save(TEntity entity)
			{
				if(_savedId > 0){
					TrySetId(entity, _savedId);
				}
				SavedItem = entity;
				
			}
			public void Delete(TEntity entity) { throw new NotImplementedException(); }
			public void SetSavedId(int id) { _savedId = id; }
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

		private static void TrySetId<TModel>(TModel entity, int id)
		{
	        var propertyInfo = typeof(TModel).GetProperty("Id");
	        if(propertyInfo == null) return;
	    	propertyInfo.SetValue(entity, id, null);
		}


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