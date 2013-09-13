namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class CreateLensSubscriptionModel 
	{
		public string CustomerName { get; set; }
		public bool ShopDoesNotHaveAccessToLensSubscriptions { get; set; }
		public bool ShopDoesNotHaveAccessGivenCustomer { get; set; }
		public bool DisplayForm
		{
			get { return ShopDoesNotHaveAccessToLensSubscriptions == false && ShopDoesNotHaveAccessGivenCustomer == false; }
		}
	}
}