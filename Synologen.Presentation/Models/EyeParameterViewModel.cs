using System.ComponentModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Extensions;

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
			Left = parameter.Return(x => x.Left, int.MinValue) ?? int.MinValue;
			Right = parameter.Return(x => x.Right, int.MinValue) ?? int.MinValue;
		}

		public EyeParameterViewModel(NullableEyeParameter<int?> parameter)
		{
			Left = parameter.Return(x => x.Left, int.MinValue) ?? int.MinValue;
			Right = parameter.Return(x => x.Right, int.MinValue) ?? int.MinValue;
		}

		public EyeParameterViewModel(){}

		[DisplayName("Vänster")]
		public decimal Left { get; set; }
		[DisplayName("Höger")]
		public decimal Right { get; set; }

		public string Format { get; set; }

		public bool DisplayLeftValue { get { return Left > int.MinValue; } }
		public bool DisplayRightValue { get { return Right > int.MinValue; } }
	}
}