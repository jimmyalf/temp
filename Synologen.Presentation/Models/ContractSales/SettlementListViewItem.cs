namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class SettlementListViewItem
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; }
		public int NumberOfContractSalesInSettlement { get; set; }
		public int NumberOfLensSubscriptionTransactionsInSettlement { get; set; }
	}
}