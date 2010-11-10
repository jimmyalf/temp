namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class ShopSettlementItem
	{
		public string ShopDescription { get; set; }
		public string BankGiroNumber { get; set; }
		public int NumberOfContractSalesInSettlement { get; set; }
		public string SumAmountIncludingVAT { get; set; }
		public string SumAmountExcludingVAT { get; set; }
	}
}