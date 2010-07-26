using System;
using System.Linq.Expressions;

namespace Spinit.Wpc.Synologen.Core.Domain.Model
{
	public class Frame
	{
		
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ArticleNumber { get; set; }
		public virtual string Color { get; set; }
		public virtual string Brand { get; set; }
		public virtual Interval Index { get; set; }
		public virtual Interval Sphere { get; set; }
		public virtual Interval Cylinder { get; set; }
		public virtual Interval PupillaryDistance { get; set; }
		public virtual bool AllowOrders { get; set; }
		
		public virtual Frame SetInterval(Expression<Func<Frame, Interval>> expression, decimal minValue, decimal maxValue, decimal incrementation)
		{
			return this;
		}
	}
}