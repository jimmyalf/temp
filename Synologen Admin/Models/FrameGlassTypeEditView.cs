using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameGlassTypeEditView
	{
		public int Id { get; set; }
		public string FormLegend { get; set; }

		[DisplayName("Namn")]
		[Required(ErrorMessage = "Namn m�ste anges")]
		public string Name { get; set; }

		[DisplayName("Inkludera Additionparameter i order")]
		public bool IncludeAdditionParametersInOrder { get; set; }

		[DisplayName("Inkludera H�jdparameter i order")]
		public bool IncludeHeightParametersInOrder { get; set; }
	}
}