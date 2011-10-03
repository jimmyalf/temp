using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using ContractArticleConnection = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.ContractArticleConnection;
using Settlement=Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Settlement;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
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
			return Mapper.Map<Settlement, SettlementView>(settlement);
		}

		public SettlementListView GetSettlements()
		{
			var settlements = _settlementRepository.GetAll();
			var criteriaForSettlementsReadyForInvoicing = new AllContractSalesMatchingCriteria
			{
				ContractSaleStatus = _settingsService.GetContractSalesReadyForSettlementStatus()
			};

			var transactionsCriteria = new AllTransactionsMatchingCriteria
			{
				SettlementStatus = SettlementStatus.DoesNotHaveSettlement,
                Reason = TransactionReason.Payment,
                Type = TransactionType.Deposit
			};

			var contractSalesReadyForSettlement = _contractSaleRepository.FindBy(criteriaForSettlementsReadyForInvoicing);
			var transactionsReadyForSettlement = _transactionRepository.FindBy(transactionsCriteria);
			return new SettlementListView
			{
				NumberOfContractSalesReadyForInvocing = contractSalesReadyForSettlement.Count(),
				NumberOfLensSubscriptionTransactionsReadyForInvocing = transactionsReadyForSettlement.Count(),
				Settlements = Mapper.Map<IEnumerable<Settlement>, IEnumerable<SettlementListViewItem>>(settlements)
			};
		}

		public int CreateSettlement() 
		{
			var statusFrom = _settingsService.GetContractSalesReadyForSettlementStatus();
			var statusTo = _settingsService.GetContractSalesAfterSettlementStatus();
			var settlementId =  _synologenSqlProvider.AddSettlement(statusFrom, statusTo);
			ConnectTransactionsToSettlement(settlementId);
			return settlementId;
		}

		public OrderView GetOrder(int orderId, RequestContext requestContext)
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
                InvoiceCopyUrl = GetInvoiceCopyUrl(requestContext, orderId),
                BackUrl = ComponentPages.Orders.Replace("~","")
			};
		}

		private string GetInvoiceCopyUrl(RequestContext requestContext, int orderId)
		{
			return new UrlHelper(requestContext)
				.Action("InvoiceCopy","Report",new RouteValueDictionary {{"id", orderId}});
		}

		private bool CanDisplayInvoiceCopyLink(int orderStatusId)
		{
			return (orderStatusId == InvoicedStatusId || 
				orderStatusId == InvoicePayedToShopStatusId || 
				orderStatusId == InvoicePayedToSynologenStatusId);
		}

		public void ConnectTransactionsToSettlement(int settlement)
		{
			var criteria = new AllTransactionsMatchingCriteria
			{
				Reason = TransactionReason.Payment,
				SettlementStatus = SettlementStatus.DoesNotHaveSettlement,
				Type = TransactionType.Deposit
			};

			var transactionsToConnectToSettlement = _transactionRepository.FindBy(criteria);
			transactionsToConnectToSettlement.Each(transaction => 
			{
				transaction.Settlement = new Core.Domain.Model.LensSubscription.Settlement(settlement);
				_transactionRepository.Save(transaction);
			});
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
                ContractArticleListUrl = GetContractArticleRoute(contractId)
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
				ContractArticleListUrl = GetContractArticleRoute(contract.Id)
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
				ContractArticleListUrl = GetContractArticleRoute(contract.Id)
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
				ContractArticleListUrl = GetContractArticleRoute(contract.Id)
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
	}
}