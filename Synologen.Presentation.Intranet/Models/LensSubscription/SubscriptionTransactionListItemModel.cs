namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class SubscriptionTransactionListItemModel
	{
		public decimal Amount { get; set; }
		public string Type { get; set; }
		public string Reason { get; set; }
		public string CreatedDate { get; set; }
		public string HasSettlement { get; set; }
	}
}
