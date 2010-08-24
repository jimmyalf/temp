using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class Interval
	{
		public virtual decimal Min { get; set; }
		public virtual decimal Max { get; set; }
		public virtual decimal Increment { get; set; }

		public IEnumerable<decimal> ToList()
		{
			if (Increment <= 0) yield break;
			var currentValue = Min;
			while (currentValue <= Max)
			{
				yield return currentValue;
				currentValue += Increment;
			}
			yield break;
		}
	}
}