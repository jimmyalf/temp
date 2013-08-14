namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IContractArticleConnection {
		int Id { get; set; }
		int ContractCustomerId { get; set; }
		int ArticleId { get; set; }
		string ArticleName { get; set; }
		float Price { get; set; }
		bool Active { get; set; }
		string ArticleNumber { get; set; }
		string ArticleDescription { get; set; }
		bool NoVAT { get; set; }
		string SPCSAccountNumber { get; set; }
		bool EnableManualPriceOverride { get; set; }
	}
}