namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class OptionalSequenceDefinition
	{
		public OptionalSequenceDefinition() { }
		public OptionalSequenceDefinition(float? min, float? max, float? increment, bool disable)
		{
    		Min = min;
    		Max = max;
    		Increment = increment;
			DisableDefinition = disable;
		}
		public virtual bool DisableDefinition { get; set; }
        public virtual float? Min { get; set; }
        public virtual float? Max { get; set; }
        public virtual float? Increment { get; set; }
	}
}