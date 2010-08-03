using System;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Core.Domain.Model
{
	public class Frame
	{
		public Frame()
		{
			Index = new Interval();
			Sphere = new Interval();
			Cylinder = new Interval();
			PupillaryDistance = new Interval();
		}
		
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ArticleNumber { get; set; }
		public virtual FrameColor Color { get; set; }
		public virtual FrameBrand Brand { get; set; }
		public virtual Interval Index { get; private set; }
		public virtual Interval Sphere { get; private set; }
		public virtual Interval Cylinder { get; private set; }
		public virtual Interval PupillaryDistance { get; private set; }
		public virtual bool AllowOrders { get; set; }
		public virtual decimal Axis { get; set; }
		public virtual Frame SetInterval(Expression<Func<Frame, Interval>> expression, decimal minValue, decimal maxValue, decimal incrementation) 
		{
			expression.Compile().Invoke(this).Increment = incrementation;
			expression.Compile().Invoke(this).Min = minValue;
			expression.Compile().Invoke(this).Max = maxValue;
			return this;
		}
	}
}