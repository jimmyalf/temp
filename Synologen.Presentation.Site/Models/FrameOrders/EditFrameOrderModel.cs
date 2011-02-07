using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.FrameOrders
{
	public class EditFrameOrderModel : FrameOrderBaseModel
	{
		
		public IEnumerable<FrameListItem> FramesList { get; set; }
		public IEnumerable<FrameGlassTypeListItem> GlassTypesList { get; set; }
		public EyeParameterIntervalListAndSelection Cylinder { get; set; }
		public EyeParameterIntervalListAndSelection Sphere { get; set; }
		public EyeParameterIntervalListAndSelection PupillaryDistance { get; set; }
		public EyeParameterIntervalListAndSelection Addition { get; set; }
		public EyeParameterIntervalListAndSelection Height { get; set; }
		public int? AxisSelectionRight { get; set; }
		public int? AxisSelectionLeft { get; set; }

		public string FrameRequiredErrorMessage { get; set; }
		public string PupillaryDistanceRequiredErrorMessage { get; set; }
		public string GlassTypeRequiredErrorMessage { get; set; }
		public string SphereRequiredErrorMessage { get; set; }
		//public string CylinderRequiredErrorMessage { get; set; }
		public string AdditionRequiredErrorMessage { get; set; }
		public string HeightRequiredMessage { get; set; }
		public string AxisRequiredMessage { get; set; }
		public string AxisRangeMessage { get; set; }

		public int SelectedFrameId { get; set; }
		public int SelectedGlassTypeId { get; set; }

		public bool AdditionParametersEnabled{ get; set;}
		public bool HeightParametersEnabled{ get; set;}

		public int NotSelectedIntervalValue { get { return int.MinValue; } }
		public string Reference { get; set; }

		public bool OrderHasBeenSent{ get; set; }
		public bool UserDoesNotHaveAccessToThisOrder{ get; set; }
		public bool OrderDoesNotExist { get; set; }
		public bool DisplayForm
		{
			get { return (
				OrderHasBeenSent == false && 
				UserDoesNotHaveAccessToThisOrder == false &&
				OrderDoesNotExist == false &&
				ShopDoesNotHaveAccessToFrameOrders == false);
			}
		}

		public bool AxisValueLeftIsRequired { get; set; }
		public bool AxisValueRightIsRequired { get; set; }
	}
}