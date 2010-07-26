namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameEditView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ArticleNumber { get; set; }
		public string Color { get; set; }
		public string Brand { get; set; }
		public decimal IndexMinValue { get; set; }
		public decimal IndexMaxValue { get; set; }
		public decimal IndexIncrementation { get; set; }
		public decimal SphereMinValue { get; set; }
		public decimal SphereMaxValue { get; set; }
		public decimal SphereIncrementation { get; set; }
		public decimal CylinderMinValue { get; set; }
		public decimal CylinderMaxValue { get; set; }
		public decimal CylinderIncrementation { get; set; }
		public decimal PupillaryDistanceMinValue { get; set; }
		public decimal PupillaryDistanceMaxValue { get; set; }
		public decimal PupillaryDistanceIncrementation { get; set; }
		public bool AllowOrders { get; set; }
	}
}