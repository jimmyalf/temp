using System;
using System.Collections.Generic;
using System.Linq;
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
                BackUrl = ComponentPages.Orders.Replace("~","")
			};
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

		public ContractArticleView GetContractArticleView(int contractId)
		{
			var articleSelectList = _articleRepository
				.GetAll()
				.ToSelectList(x => x.Id, x => x.Name);
			return new ContractArticleView
			{
				ContractId = contractId,
				Articles = articleSelectList
			};
		}

		public ContractArticleView UpdateContractArticleView(ContractArticleView contractArticleView, Func<Core.Domain.Model.ContractSales.Article, ContractArticleView, string> getSPCSAccountNumberFunction)
		{
			var articleSelectList = _articleRepository.GetAll().ToSelectList(x => x.Id, x => x.Name);
			var selectedArticle = _articleRepository.Get(contractArticleView.SelectedArticleId);
			var spcsAccountNumber = getSPCSAccountNumberFunction(selectedArticle, contractArticleView);
			return new ContractArticleView
			{
				ContractId = contractArticleView.ContractId,
				Articles = articleSelectList,
                SPCSAccountNumber = spcsAccountNumber,
                AllowCustomPricing = contractArticleView.AllowCustomPricing,
				IsActive = contractArticleView.IsActive,
				IsVATFreeArticle = contractArticleView.IsVATFreeArticle,
                PriceWithoutVAT = contractArticleView.PriceWithoutVAT,
                SelectedArticleId = contractArticleView.SelectedArticleId,                
			};
		}

		public ContractArticleConnection ParseContractArticle(ContractArticleView contractArticleView)
		{
			return new ContractArticleConnection
			{
				Active = contractArticleView.IsActive,
				ArticleId = contractArticleView.SelectedArticleId,
				ContractCustomerId = contractArticleView.ContractId,
				EnableManualPriceOverride = contractArticleView.AllowCustomPricing,
				NoVAT = contractArticleView.IsVATFreeArticle,
				Price = contractArticleView.PriceWithoutVAT,
				SPCSAccountNumber = contractArticleView.SPCSAccountNumber
			};
		}
	}
}