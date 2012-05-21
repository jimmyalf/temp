namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders
{
	public class ViewFrameOrderModel : FrameOrderBaseModel
	{
		public bool OrderHasBeenSent { get; set; }
		public decimal? CylinderLeft { get; set; }
		public decimal? CylinderRight { get; set; }
		public decimal SphereLeft { get; set; }
		public decimal SphereRight { get; set; }
		public decimal PupillaryDistanceLeft { get; set; }
		public decimal PupillaryDistanceRight { get; set; }
		public int? AxisSelectionLeft { get; set; }
		public int? AxisSelectionRight { get; set; }
		public decimal? AdditionLeft { get; set; }
		public decimal? AdditionRight { get; set; }
		public decimal? HeightLeft { get; set; }
		public decimal? HeightRight { get; set; }
		

		public string FrameName { get; set; }
		public string FrameArticleNumber { get; set; }
		public string FrameColor { get; set; }
		public string FrameBrand { get; set; }
		public string GlassTypeName { get; set; }
		public string ShopName { get; set; }
		public string ShopCity { get; set; }
		public string CreatedDate { get; set; }
		public string SentDate { get; set; }
		public string Reference { get; set; }
		public string EditPageUrl { get; set; }
		public bool OrderDoesNotExist { get; set; }
		public bool UserDoesNotHaveAccessToThisOrder { get; set; }
		public bool DisplayOrder
		{
			get { return (
			             	OrderDoesNotExist == false && 
			             	UserDoesNotHaveAccessToThisOrder == false &&
			             	ShopDoesNotHaveAccessToFrameOrders == false); 
			}
		}

		
	}
}