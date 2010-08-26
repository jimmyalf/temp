namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameOrderView
	{
		public string Created { get; set; }
		public string Sent { get; set; }
		public string Frame { get; set; }
		public string FrameArticleNumber { get; set; }
		public string GlassType { get; set; }
		public int Id { get; set; }
		public string Shop { get; set; }
		public string ShopCity { get; set; }
		public EyeParameterViewModel Addition { get; set; }
		public EyeParameterViewModel Axis { get; set; }
		public EyeParameterViewModel Cylinder { get; set; }
		public EyeParameterViewModel Height { get; set; }
		public EyeParameterViewModel PupillaryDistance { get; set; }
		public EyeParameterViewModel Sphere { get; set; }
	}
}