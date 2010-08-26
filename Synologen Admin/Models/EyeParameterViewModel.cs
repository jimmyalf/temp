using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class EyeParameterViewModel
	{
		[DisplayName("Vänster")]
		public decimal Left { get; set; }
		[DisplayName("Höger")]
		public decimal Right { get; set; }
	}
}