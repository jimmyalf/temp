namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class SequenceDefinition
    {
    	public SequenceDefinition() { }

    	public SequenceDefinition(float min, float max, float increment)
    	{
    		Min = min;
    		Max = max;
    		Increment = increment;
    	}
        public virtual float Min { get; set; }
        public virtual float Max { get; set; }
        public virtual float Increment { get; set; }
    }
}