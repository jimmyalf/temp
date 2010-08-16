using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model;
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
			public IEnumerable<Frame> GetAll() { return new List<Frame>(); }
			public IEnumerable<Frame> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria { throw new NotImplementedException(); }
			public void Save(Frame entity) { throw new NotImplementedException(); }
			public void Delete(Frame entity) { throw new NotImplementedException(); }
		}
	}
}