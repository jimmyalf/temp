namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public interface IContractArticleView
	{
		string SPCSAccountNumber { get; set; }
		string PriceWithoutVAT { get; set; }
		bool IsVATFreeArticle { get; set; }
		bool AllowCustomPricing { get; set; }
		bool IsActive { get; set; }
		string ContractArticleListUrl { get; set; }
	}
}