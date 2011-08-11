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
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
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
		private const int InvoicedStatusId = 5;

		public ContractSalesViewService(
			ISettlementRepository settlementRepository, 
			IContractSaleRepository contractSaleRepository, 
			IAdminSettingsService settingsService,
			ITransactionRepository transactionRepository,
			ISqlProvider synologenSqlProvider)
		{
			_settlementRepository = settlementRepository;
			_contractSaleRepository = contractSaleRepository;
			_settingsService = settingsService;
			_transactionRepository = transactionRepository;
			_synologenSqlProvider = synologenSqlProvider;
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

		public ArticleView GetArticle(int articleId)
		{
			var article = _synologenSqlProvider.GetArticle(articleId);
			return new ArticleView
			{
				ArticleNumber = article.Number,
				DefaultSPCSAccountNumber = article.DefaultSPCSAccountNumber,
				Description = article.Description,
				Id = article.Id,
				Name = article.Name
			};
		}
	}
}