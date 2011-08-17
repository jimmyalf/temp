using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using ContractArticleConnection = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.ContractArticleConnection;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IContractSalesViewService
	{
		SettlementView GetSettlement(int settlementId);
		SettlementListView GetSettlements();
		int CreateSettlement();
		OrderView GetOrder(int orderId);
		Article ParseArticle(ArticleView articleView);
		ArticleView GetArticleView(int articleId, string formLegend);
		ArticleView SetArticleViewDefaults(ArticleView articleView, string formLegend);
		ContractArticleView GetContractArticleView(int contractId);
		ContractArticleView UpdateContractArticleView(ContractArticleView contractArticleView, Func<Core.Domain.Model.ContractSales.Article, ContractArticleView, string> getSPCSAccountNumberFunction);
		ContractArticleConnection ParseContractArticle(ContractArticleView contractArticleView);
	}
}