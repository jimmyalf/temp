using System;
using System.Collections.Generic;
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

		internal class MockedFrameRepository : IFrameRepository
		{
			public Frame Get(int id) { throw new NotImplementedException(); }
			public IEnumerable<Frame> GetAll() { throw new NotImplementedException(); }
			public IEnumerable<Frame> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria
			{
				var returnList = new List<Frame>();
				for(var id=0; id < 10; id++)
				{
					returnList.Add(GetMockedFrame(id));
				}
				return new SortedPagedList<Frame>(returnList, 20, 1, 10, null, false);
			}

			

			public void Save(Frame entity) { throw new NotImplementedException(); }
			public void Delete(Frame entity) { throw new NotImplementedException(); }
		}

		public static Frame GetMockedFrame(int id)
		{
			return new Frame
			{
				AllowOrders = true,
				ArticleNumber = "123456",
				//Axis = 50,
				Brand = new FrameBrand {Id = 1, Name = "Björn Borg"},
				Color = new FrameColor {Id = 1, Name = "Blå"},
				Id = id,
				Name = "Bra båge",
			}
				//.SetInterval(x => x.Index, 1.5m, 1.6m, 0.1m)
				//.SetInterval(x => x.Cylinder, -2, 0, 0.25m)
				//.SetInterval(x => x.Sphere, -6, 6, 0.25m)
				.SetInterval(x => x.PupillaryDistance, 20, 40, 0.5m);
					
		}

		internal class MockedFrameColorRepository : IFrameColorRepository
		{
			public FrameColor Get(int id) { throw new NotImplementedException(); }
			public IEnumerable<FrameColor> GetAll() { throw new NotImplementedException(); }
			public IEnumerable<FrameColor> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException(); }
			public void Save(FrameColor entity) { throw new NotImplementedException(); }
			public void Delete(FrameColor entity) { throw new NotImplementedException(); }
		}

		internal class MockedFrameBrandRepository : IFrameBrandRepository
		{
			public FrameBrand Get(int id) { throw new NotImplementedException(); }
			public IEnumerable<FrameBrand> GetAll() { throw new NotImplementedException(); }
			public IEnumerable<FrameBrand> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException(); }
			public void Save(FrameBrand entity) { throw new NotImplementedException(); }
			public void Delete(FrameBrand entity) { throw new NotImplementedException(); }
		}

		internal class MockedFrameGlassTypeRepository : IFrameGlassTypeRepository
		{
			public FrameGlassType Get(int id) { throw new NotImplementedException(); }
			public IEnumerable<FrameGlassType> GetAll() { throw new NotImplementedException(); }
			public IEnumerable<FrameGlassType> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException(); }
			public void Save(FrameGlassType entity) { throw new NotImplementedException(); }
			public void Delete(FrameGlassType entity) { throw new NotImplementedException(); }
		}
	}
}