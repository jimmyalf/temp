namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class CreateCustomerModel
	{
		public bool ShopDoesNotHaveAccessToLensSubscriptions { get; set; }
		public bool DisplayForm
		{
			get { return ShopDoesNotHaveAccessToLensSubscriptions == false; }
		}
	}
}
