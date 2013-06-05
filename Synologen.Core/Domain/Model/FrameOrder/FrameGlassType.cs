using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class FrameGlassType : IntervalContainer<FrameGlassType>
	{

		public FrameGlassType()
		{
			Sphere = new Interval();
			Cylinder = new Interval();
		}
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual bool IncludeAdditionParametersInOrder { get; set; }
		public virtual bool IncludeHeightParametersInOrder { get; set; }
		public virtual int NumberOfConnectedOrdersWithThisGlassType { get; private set; }
		public virtual Interval Sphere { get; private set; }
		public virtual Interval Cylinder { get; private set; }

		public override FrameGlassType SetInterval(Func<FrameGlassType, Interval> intervalProperty, decimal minValue, decimal maxValue, decimal incrementation) 
		{ 
			return base.SetInterval(this, intervalProperty, minValue, maxValue, incrementation);
		}
	}
}