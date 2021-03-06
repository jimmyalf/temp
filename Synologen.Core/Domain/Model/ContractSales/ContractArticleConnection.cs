namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ContractArticleConnection
	{
		public int Id { get; set; }
		public int ContractCustomerId { get; set; }
		public int ArticleId { get; set; }
		public decimal Price { get; set; }
		public bool NoVAT { get; set; }
		public bool Active { get; set; }
		public string SPCSAccountNumber { get; set; }
		public bool EnableManualPriceOverride { get; set; }

	    public int CustomerArticleNumber { get; set; }
	    public int DiscountId { get; set; }
	}
}