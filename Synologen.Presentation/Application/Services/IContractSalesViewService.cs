using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using ContractArticleConnection = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.ContractArticleConnection;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IContractSalesViewService
	{
		SettlementView GetSettlement(int settlementId);
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
        FtpProfile ParseFtpProfile(FtpProfileView ftpProfileView);
        string GetContractArticleRoute(int contractId);
		Core.Domain.Model.ContractSales.Article GetArticle(int articleId);
	    StatisticsView GetStatisticsView();
        void UpdateStatisticsView(StatisticsView model);
	    FtpProfileView SetFtpProfileViewDefaults(FtpProfileView ftpProfileView, string formLegend);
	    FtpProfileView GetFtpProfileView(FtpProfile ftpProfile, string formLegend);
	}
}