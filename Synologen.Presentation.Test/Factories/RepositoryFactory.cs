using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model;
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

		public static Frame GetMockedFrame(int id)
		{
		    return new Frame
		    {
		        AllowOrders = true,
		        ArticleNumber = "123456",
		        Brand = new FrameBrand {Id = 1, Name = "Björn Borg"},
		        Color = new FrameColor {Id = 1, Name = "Blå"},
		        Id = id,
		        Name = "Bra båge",
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