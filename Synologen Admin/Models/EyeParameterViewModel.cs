using System.ComponentModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class EyeParameterViewModel
	{
		public EyeParameterViewModel(EyeParameter parameter)
		{
			Left = parameter.Left;
			Right = parameter.Right;
		}
		public EyeParameterViewModel(){}

		[DisplayName("V�nster")]
		public decimal Left { get; set; }
		[DisplayName("H�ger")]
		public decimal Right { get; set; }
	}
}