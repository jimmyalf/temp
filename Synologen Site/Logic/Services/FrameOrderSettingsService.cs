using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
	public class FrameOrderSettingsService : IFrameOrderSettingsService
	{
		public Interval Sphere { 
			get
			{
				return new Interval
				{
					Increment = Globals.FrameOrderCylinderIncrement, 
					Max = Globals.FrameOrderSphereMax,
					Min = Globals.FrameOrderSphereMin
				};			
			} 
		}
		public Interval Cylinder { 
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderCylinderIncrement,
					Max = Globals.FrameOrderCylinderMax,
					Min = Globals.FrameOrderCylinderMin
				};
			}
		}
		public Interval Addition
		{
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderAdditionIncrement,
					Max = Globals.FrameOrderAdditionMax,
					Min = Globals.FrameOrderAdditionMin
				};
			}
		}
		public Interval Height
		{
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderHeightIncrement,
					Max = Globals.FrameOrderHeightMax,
					Min = Globals.FrameOrderHeightMin
				};
			}
		}
	}
}