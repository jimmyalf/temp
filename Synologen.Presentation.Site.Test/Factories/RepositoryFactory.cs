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

		internal class MockedFrameRepository : IFrameRepository
		{
			public Frame Get(int id)
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
			public IEnumerable<Frame> GetAll() { return new List<Frame>(); }
			public IEnumerable<Frame> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException(); }
			public void Save(Frame entity) { throw new NotImplementedException(); }
			public void Delete(Frame entity) { throw new NotImplementedException(); }
		}

		public static IFrameGlassTypeRepository GetFrameGlassRepository()
		{
			return new MockedFrameGlassTypeRepository();
		}

		internal class MockedFrameGlassTypeRepository : IFrameGlassTypeRepository {
			public FrameGlassType Get(int id)
			{
				return new FrameGlassType
				{
					Id = id,
					IncludeAdditionParametersInOrder = true,
					IncludeHeightParametersInOrder = false,
					Name = "Närprogressiva"
				};
			}
			public IEnumerable<FrameGlassType> GetAll() { return new List<FrameGlassType>(); }
			public IEnumerable<FrameGlassType> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException();}
			public void Save(FrameGlassType entity) { throw new NotImplementedException(); }
			public void Delete(FrameGlassType entity) { throw new NotImplementedException(); }
		}
	}

	
}