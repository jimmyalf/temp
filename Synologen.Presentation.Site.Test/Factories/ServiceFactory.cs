using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.Factories
{
	public static class ServiceFactory {
		public static IFrameOrderSettingsService GetFrameOrderSettingsService() 
		{
			return new MockFrameOrderSettingsService();
		}
		internal class MockFrameOrderSettingsService : IFrameOrderSettingsService{
			public MockFrameOrderSettingsService()
			{
				Sphere = new Interval {Increment = 0.25M, Max = 6, Min = -6};
				Cylinder = new Interval {Increment = 0.25M, Max = 2, Min = 0};
				Addition = new Interval {Increment = 0.25M, Max = 3, Min = 1};
				Height = new Interval {Increment = 1, Max = 28, Min = 18};
			}
			public Interval Sphere { get; private set; }
			public Interval Cylinder { get; private set; }
			public Interval Addition { get; private set; }
			public Interval Height { get; private set; }
		}
	}
}