namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class EditLensSubscriptionModel 
	{
		public string ActivatedDate { get; set; }
		public string CreatedDate { get; set; }
		public string CustomerName { get; set; }
		public string AccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public string MonthlyAmount { get; set; }
		public string Status { get; set; }
		public string ConsentStatus { get; set; }
		public string Notes { get; set; }
		public bool ShopDoesNotHaveAccessToLensSubscriptions { get; set; }
		public bool ShopDoesNotHaveAccessGivenCustomer { get; set; }
		public bool SubscriptionDoesNotExist { get; set; }
		public bool DisplayForm
		{
			get {
				return ShopDoesNotHaveAccessToLensSubscriptions == false
				&& ShopDoesNotHaveAccessGivenCustomer == false
				&& SubscriptionDoesNotExist == false;
			}
		}

		public bool StopButtonEnabled { get; set; }
		public bool StartButtonEnabled { get; set; }
		public string ReturnUrl { get; set; }
	}
}