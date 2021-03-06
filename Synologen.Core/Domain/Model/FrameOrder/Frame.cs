using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class Frame : IntervalContainer<Frame>
	{
		public Frame()
		{
			PupillaryDistance = new Interval();
		}
		
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ArticleNumber { get; set; }
        public virtual FrameSupplier Supplier { get; set; }
		public virtual FrameColor Color { get; set; }
		public virtual FrameBrand Brand { get; set; }
		public virtual Interval PupillaryDistance { get; private set; }
		public virtual bool AllowOrders { get; set; }
		public virtual int NumberOfConnectedOrdersWithThisFrame { get; set; }
		public virtual FrameStock Stock { get; set; }
		public override Frame SetInterval(Func<Frame, Interval> intervalProperty, decimal minValue, decimal maxValue, decimal incrementation) 
		{
			return base.SetInterval(this, intervalProperty, minValue, maxValue, incrementation);
		}
	}
}