using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IContractSalesViewService
	{
		SettlementView GetSettlement(int settlementId);
		SettlementListView GetSettlements();
		int CreateSettlement();
		OrderView GetOrder(int orderId);
		Article ParseArticle(ArticleView articleView);
		ArticleView GetArticle(int articleId);
	}
}