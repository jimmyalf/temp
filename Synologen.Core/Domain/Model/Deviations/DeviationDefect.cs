namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
	public class DeviationDefect : Entity
	{
        public virtual string Name { get; set; }
        public virtual DeviationCategory Category { get; set; }
	}
}