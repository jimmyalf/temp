using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class SequenceDefinition
    {
    	public SequenceDefinition() { }

    	public SequenceDefinition(decimal min, decimal max, decimal increment)
    	{
    		Min = min;
    		Max = max;
    		Increment = increment;
    	}
        public virtual decimal Min { get; set; }
        public virtual decimal Max { get; set; }
        public virtual decimal Increment { get; set; }
		public IEnumerable<decimal> Enumerate()
		{
			if (Increment <= 0)
			{
				yield return Min;
				yield break;
			}
            for (var value = Min; value <= Max; value += Increment)
            {
            	yield return value;
            }
		}
    }
}