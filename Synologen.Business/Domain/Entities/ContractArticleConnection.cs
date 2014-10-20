using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class ContractArticleConnection : IContractArticleConnection{
		public int Id { get; set; }
		public int ContractCustomerId { get; set; }
		public int ArticleId { get; set; }
		public string ArticleName { get; set; }
		public float Price { get; set; }
		public bool Active { get; set; }
		public string ArticleNumber { get; set; }
		public string ArticleDescription { get; set; }
		public bool NoVAT { get; set; }
		public string SPCSAccountNumber { get; set; }
		public bool EnableManualPriceOverride { get; set; }
		public int CustomerArticleId { get; set; }
		public int DiscountId { get; set; }
	}
}