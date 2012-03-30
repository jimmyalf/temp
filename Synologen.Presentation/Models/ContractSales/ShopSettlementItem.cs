namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ShopSettlementItem
	{
		public string ShopDescription { get; set; }
		public string BankGiroNumber { get; set; }
		public int NumberOfContractSalesInSettlement { get; set; }
		public int NumberOfOldTransactionsInSettlement { get; set; }
		public int NumberOfNewTransactionsInSettlement { get; set; }
		public string SumAmountIncludingVAT { get; set; }
	}
}