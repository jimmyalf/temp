using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models
{
	public class ViewFrameOrderModel
	{
		
		public EyeParameterIntervalListAndSelection Cylinder { get; set; }
		public EyeParameterIntervalListAndSelection Sphere { get; set; }
		public EyeParameterIntervalListAndSelection PupillaryDistance { get; set; }
		public EyeParameterIntervalListAndSelection Addition { get; set; }
		public EyeParameterIntervalListAndSelection Height { get; set; }
		public EyeParameter AxisSelection { get; set; }

		public string FrameName { get; set; }
		public string FrameArticleNumber { get; set; }
		public string FrameColor { get; set; }
		public string GlassTypeName { get; set; }
		public string ShopName { get; set; }
		public string ShopCity { get; set; }
		public string CreatedDate { get; set; }
		public string SentDate { get; set; }
		public string Notes { get; set; }
	}
}