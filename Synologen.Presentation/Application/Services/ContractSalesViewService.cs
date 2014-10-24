using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using ContractArticleConnection = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.ContractArticleConnection;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
    using Spinit.Wpc.Synologen.Core.Extensions;
    using Spinit.Wpc.Utility.Business;

    public class ContractSalesViewService : IContractSalesViewService 
	{
		private readonly ISettlementRepository _settlementRepository;
		private readonly IContractSaleRepository _contractSaleRepository;
		private readonly IAdminSettingsService _settingsService;
		private readonly ITransactionRepository _transactionRepository;
		private readonly ISqlProvider _synologenSqlProvider;
		private readonly IArticleRepository _articleRepository;
		private const int InvoicedStatusId = 5;
		private const int InvoicePayedToSynologenStatusId = 6;
		private const int InvoicePayedToShopStatusId = 8;

		public ContractSalesViewService(
			ISettlementRepository settlementRepository, 
			IContractSaleRepository contractSaleRepository, 
			IAdminSettingsService settingsService,
			ITransactionRepository transactionRepository,
			ISqlProvider synologenSqlProvider,
			IArticleRepository articleRepository)
		{
			_settlementRepository = settlementRepository;
			_contractSaleRepository = contractSaleRepository;
			_settingsService = settingsService;
			_transactionRepository = transactionRepository;
			_synologenSqlProvider = synologenSqlProvider;
			_articleRepository = articleRepository;
		}

		public SettlementView GetSettlement(int settlementId) 
		{
			var settlement = _settlementRepository.Get(settlementId);
			return new SettlementView(settlement);
			//return Mapper.Map<Settlement, SettlementView>(settlement);
		}

		//public SettlementListView GetSettlements()
		//{

		//}

		public int CreateSettlement() 
		{
			var statusFrom = _settingsService.GetContractSalesReadyForSettlementStatus();
			var statusTo = _settingsService.GetContractSalesAfterSettlementStatus();
			var settlementId =  _synologenSqlProvider.AddSettlement(statusFrom, statusTo);
			//ConnectTransactionsToSettlement(settlementId);
			return settlementId;
		}

		public OrderView GetOrder(int orderId)
		{
			var order = _synologenSqlProvider.GetOrder(orderId);
			var orderStatus = _synologenSqlProvider.GetOrderStatusRow(order.StatusId);
			return new OrderView
			{
				Id = order.Id,
                Status = orderStatus.Name,
				VISMAInvoiceNumber = order.InvoiceNumber.ToString(),
                DisplayCancelButton = (order.StatusId == InvoicedStatusId),
                DisplayInvoiceCopyLink = CanDisplayInvoiceCopyLink(order.StatusId),
                BackUrl = ComponentPages.Orders.Replace("~","")
			};
		}

		private bool CanDisplayInvoiceCopyLink(int orderStatusId)
		{
			return (orderStatusId == InvoicedStatusId || 
				orderStatusId == InvoicePayedToShopStatusId || 
				orderStatusId == InvoicePayedToSynologenStatusId);
		}

		public Article ParseArticle(ArticleView articleView)
		{
			return new Article
			{
				DefaultSPCSAccountNumber = articleView.DefaultSPCSAccountNumber,
                Description = articleView.Description,
                Id = articleView.Id,
                Name = articleView.Name,
                Number = articleView.ArticleNumber
			};
		}

		public ArticleView GetArticleView(int articleId, string formLegend)
		{
			var article = _synologenSqlProvider.GetArticle(articleId);
			return new ArticleView
			{
				ArticleNumber = article.Number,
				DefaultSPCSAccountNumber = article.DefaultSPCSAccountNumber,
				Description = article.Description,
				Id = article.Id,
				Name = article.Name,
                FormLegend = formLegend,
				ArticleListUrl = ComponentPages.Articles.Replace("~","")
			};
		}

		public ArticleView SetArticleViewDefaults(ArticleView articleView, string formLegend)
		{
			articleView.FormLegend = formLegend;
			articleView.ArticleListUrl = ComponentPages.Articles.Replace("~", "");
			return articleView;
		}

		public AddContractArticleView GetAddContractArticleView(int contractId)
		{
			var articleSelectList = _articleRepository.GetAll().ToSelectList(x => x.Id, x => x.Name);
			var contract = _synologenSqlProvider.GetContract(contractId);
			return new AddContractArticleView
			{
				ContractId = contractId,
				ContractName = contract.Name,
				Articles = articleSelectList,
                IsActive = true,
                ContractArticleListUrl = GetContractArticleRoute(contractId),
			};
		}

		public EditContractArticleView GetEditContractArticleView(int contractArticleId)
		{
			var contractArticle = _synologenSqlProvider.GetContractCustomerArticleRow(contractArticleId);
			var contract = _synologenSqlProvider.GetContract(contractArticle.ContractCustomerId);
			return new EditContractArticleView
			{
				Id = contractArticleId,
				ContractName = contract.Name,
                AllowCustomPricing = contractArticle.EnableManualPriceOverride,
				IsActive = contractArticle.Active,
				IsVATFreeArticle = contractArticle.NoVAT,
				PriceWithoutVAT = contractArticle.Price.ToString(),
				SPCSAccountNumber = contractArticle.SPCSAccountNumber,
                ArticleName = contractArticle.ArticleName,
				ContractArticleListUrl = GetContractArticleRoute(contract.Id),
                CustomerArticelNumber = contractArticle.CustomerArticleNumber > 0 ? contractArticle.CustomerArticleNumber.ToString() : string.Empty,
                DiscountId = contractArticle.DiscountId > 0 ? contractArticle.DiscountId.ToString() : string.Empty,
			};
		}

		public AddContractArticleView UpdateContractArticleView(AddContractArticleView contractArticleView, Func<Core.Domain.Model.ContractSales.Article, AddContractArticleView, string> getSPCSAccountNumberFunction)
		{
			var articleSelectList = _articleRepository.GetAll().ToSelectList(x => x.Id, x => x.Name);
			var selectedArticle = _articleRepository.Get(contractArticleView.ArticleId);
			var spcsAccountNumber = getSPCSAccountNumberFunction(selectedArticle, contractArticleView);
			var contract = _synologenSqlProvider.GetContract(contractArticleView.ContractId);
			return new AddContractArticleView
			{
				ContractId = contractArticleView.ContractId,
				ContractName = contract.Name,
				Articles = articleSelectList,
                SPCSAccountNumber = spcsAccountNumber,
                AllowCustomPricing = contractArticleView.AllowCustomPricing,
				IsActive = contractArticleView.IsActive,
				IsVATFreeArticle = contractArticleView.IsVATFreeArticle,
                PriceWithoutVAT = contractArticleView.PriceWithoutVAT,
                ArticleId = contractArticleView.ArticleId,
				ContractArticleListUrl = GetContractArticleRoute(contract.Id),
			};
		}

		public EditContractArticleView UpdateContractArticleView(EditContractArticleView contractArticleView)
		{
			var contractArticle = _synologenSqlProvider.GetContractCustomerArticleRow(contractArticleView.Id);
			var contract = _synologenSqlProvider.GetContract(contractArticle.ContractCustomerId);
			return new EditContractArticleView
			{
				Id = contractArticle.Id,
				ContractName = contract.Name,
                SPCSAccountNumber = contractArticleView.SPCSAccountNumber,
                AllowCustomPricing = contractArticleView.AllowCustomPricing,
				IsActive = contractArticleView.IsActive,
				IsVATFreeArticle = contractArticleView.IsVATFreeArticle,
                PriceWithoutVAT = contractArticleView.PriceWithoutVAT,   
				ArticleName = contractArticle.ArticleName,
				ContractArticleListUrl = GetContractArticleRoute(contract.Id),
                CustomerArticelNumber = contractArticleView.CustomerArticelNumber,
                DiscountId = contractArticleView.DiscountId,
			};
		}


		public ContractArticleConnection ParseContractArticle(AddContractArticleView addContractArticleView)
		{
			return new ContractArticleConnection
			{
				Active = addContractArticleView.IsActive,
				ArticleId = addContractArticleView.ArticleId,
				ContractCustomerId = addContractArticleView.ContractId,
				EnableManualPriceOverride = addContractArticleView.AllowCustomPricing,
				NoVAT = addContractArticleView.IsVATFreeArticle,
				Price = Convert.ToInt32(addContractArticleView.PriceWithoutVAT),
				SPCSAccountNumber = addContractArticleView.SPCSAccountNumber,
                CustomerArticleNumber = Convert.ToInt32(addContractArticleView.CustomerArticelId),
				DiscountId = Convert.ToInt32(addContractArticleView.DiscountId),
			};
		}

		public ContractArticleConnection ParseContractArticle(EditContractArticleView contractArticleView)
		{
			var contractArticle = _synologenSqlProvider.GetContractCustomerArticleRow(contractArticleView.Id);
			return new ContractArticleConnection
			{
				Id = contractArticleView.Id,
				Active = contractArticleView.IsActive,
				ArticleId = contractArticle.ArticleId,
				ContractCustomerId = contractArticle.ContractCustomerId,
				EnableManualPriceOverride = contractArticleView.AllowCustomPricing,
				NoVAT = contractArticleView.IsVATFreeArticle,
				Price = Convert.ToInt32(contractArticleView.PriceWithoutVAT),
				SPCSAccountNumber = contractArticleView.SPCSAccountNumber,
                CustomerArticleNumber = Convert.ToInt32(contractArticleView.CustomerArticelNumber),
                DiscountId = Convert.ToInt32(contractArticleView.DiscountId),
			};
		}

		public string GetContractArticleRoute(int contractId)
		{
			return "{Url}?contractId={ContractId}"
				.Replace("{Url}", ComponentPages.ContractArticles.Replace("~", "").ToLower())
				.Replace("{ContractId}", contractId.ToString());
		}

		public Core.Domain.Model.ContractSales.Article GetArticle(int articleId)
		{
			return _articleRepository.Get(articleId);
		}

	    public StatisticsView GetStatisticsView()
	    {
	        return new StatisticsView
	        {
	            Contracts = GetAllContracts(), 
                Companies = GetAllCompanies(),
                ReportTypes = GetAllReportTypes(),
	        };
	    }

	    public void UpdateStatisticsView(StatisticsView model)
	    {
	        model.Contracts = GetAllContracts();
            model.Companies = GetAllCompanies();
	        model.ReportTypes = GetAllReportTypes();
	    }

	    protected List<ContractListItem> GetAllContracts()
        {
            return _synologenSqlProvider.GetContracts(FetchCustomerContract.All, 0, 0, null)
                .Tables[0].AsEnumerable()
                .Select(x => new ContractListItem
                {
                    Name = x.Field<string>("cName"),
                    Id = x.Field<int>("cId")
                }).ToList();
        }

        protected List<CompanyListItem> GetAllCompanies()
        {
            return _synologenSqlProvider.GetCompanies(0, 0, null, ActiveFilter.Both)
                .Tables[0].AsEnumerable()
                .Select(x => new CompanyListItem
                {
                    Name = x.Field<string>("cName"),
                    Id = x.Field<int>("cId"),
                    ContractId = x.Field<int>("cContractCustomerId")
                }).ToList();
        }

        protected List<ReportTypeListItem> GetAllReportTypes()
        {
            var reportTypes = 
                EnumExtensions.Enumerate<StatisticsReportTypes>()
                .Select(reportType => new ReportTypeListItem
                                  {
                                      Id = (int)reportType, 
                                      Name = reportType.GetEnumDisplayName()
                                  }).ToList();
            return reportTypes;
        }
	}
}