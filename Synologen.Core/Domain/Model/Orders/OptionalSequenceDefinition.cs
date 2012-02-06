namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class OptionalSequenceDefinition
	{
		public OptionalSequenceDefinition() { }
		public OptionalSequenceDefinition(decimal? min, decimal? max, decimal? increment, bool disable)
		{
    		Min = min;
    		Max = max;
    		Increment = increment;
			DisableDefinition = disable;
		}
		public virtual bool DisableDefinition { get; set; }
        public virtual decimal? Min { get; set; }
        public virtual decimal? Max { get; set; }
        public virtual decimal? Increment { get; set; }
	}
}