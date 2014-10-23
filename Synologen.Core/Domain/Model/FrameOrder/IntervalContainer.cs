using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public abstract class IntervalContainer<TImplementingClass> where TImplementingClass : IntervalContainer<TImplementingClass>
	{
		protected virtual TImplementingClass SetInterval(TImplementingClass implementingClass, Func<TImplementingClass, Interval> intervalProperty, decimal minValue, decimal maxValue, decimal incrementation) 
		{
			intervalProperty.Invoke(implementingClass).Increment = incrementation;
			intervalProperty.Invoke(implementingClass).Min = minValue;
			intervalProperty.Invoke(implementingClass).Max = maxValue;
			return implementingClass;
		}

		public abstract TImplementingClass SetInterval(Func<TImplementingClass, Interval> intervalProperty, decimal minValue, decimal maxValue, decimal incrementation);
	}
}