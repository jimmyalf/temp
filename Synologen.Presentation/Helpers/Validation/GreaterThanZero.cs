using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Validation
{
	public class GreaterThanZero : RangeAttribute 
	{
		public GreaterThanZero() : base(0.1, int.MaxValue) { }
	}
}