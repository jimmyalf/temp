using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class EyeParameterViewModel
	{
		[DisplayName("V�nster")]
		public decimal Left { get; set; }
		[DisplayName("H�ger")]
		public decimal Right { get; set; }
	}
}