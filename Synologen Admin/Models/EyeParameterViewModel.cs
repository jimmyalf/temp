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

		public EyeParameterViewModel(NullableEyeParameter parameter)
		{
			Left = parameter.Left ?? int.MinValue;
			Right = parameter.Right ?? int.MinValue;
		}

		public EyeParameterViewModel(NullableEyeParameter<int?> parameter)
		{
			Left = parameter.Left ?? int.MinValue;
			Right = parameter.Right ?? int.MinValue;
		}

		public EyeParameterViewModel(){}

		[DisplayName("Vänster")]
		public decimal Left { get; set; }
		[DisplayName("Höger")]
		public decimal Right { get; set; }

		public string Format { get; set; }
	}
}