namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IContractSalesCommandService
	{
		void CancelOrder(int orderId);
		void AddArticle(Business.Domain.Entities.Article article);
		void UpdateArticle(Business.Domain.Entities.Article article);
		void AddContractArticle(Core.Domain.Model.ContractSales.ContractArticleConnection contractArticleConnection);
		void UpdateContractArticle(Core.Domain.Model.ContractSales.ContractArticleConnection contractArticleConnection);
	}
}