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
		AddContractArticleView GetAddContractArticleView(int contractId);
		EditContractArticleView GetEditContractArticleView(int contractArticleId);
		AddContractArticleView UpdateContractArticleView(AddContractArticleView addContractArticleView, Func<Core.Domain.Model.ContractSales.Article, AddContractArticleView, string> getSPCSAccountNumberFunction);
		EditContractArticleView UpdateContractArticleView(EditContractArticleView addContractArticleView);
		ContractArticleConnection ParseContractArticle(AddContractArticleView addContractArticleView);
		ContractArticleConnection ParseContractArticle(EditContractArticleView addContractArticleView);
		string GetContractArticleRoute(int contractId);
	}
}