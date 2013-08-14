using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameBrandEditView
	{
		public int Id { get; set; }
		[DisplayName("Namn")]
		[Required(ErrorMessage = "Namn måste anges")]
		public string Name { get; set; }
		public string FormLegend { get; set; }
		
	}
}