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
					IncludeAdditionParametersInOrder = true,
					IncludeHeightParametersInOrder = false,
					Name = "Närprogressiva"
				};
			}
		}

		internal class MockedBaseClass<TEntity> : IRepository<TEntity> where TEntity : class
		{
			public virtual TEntity Get(int id){ throw new NotImplementedException();}
			public IEnumerable<TEntity> GetAll() { return GenerateItems<TEntity>(Get); }
			public IEnumerable<TEntity> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { return GetAll(); }
			public void Save(TEntity entity) { throw new NotImplementedException(); }
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