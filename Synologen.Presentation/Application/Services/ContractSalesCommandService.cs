using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Utility.Business;
using ContractArticleConnection = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.ContractArticleConnection;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class ContractSalesCommandService : IContractSalesCommandService
	{
		private readonly ISqlProvider _synologenSqlProvider;
		private readonly IUserContextService _userContextService;
		private const int InvoiceCanceledStatusId = 7;
		private const string OrderCancelHistoryMessage = "Order makulerad manuellt av användare \"{UserName}\".";

		public ContractSalesCommandService(ISqlProvider synologenSqlProvider, IUserContextService userContextService)
		{
			_synologenSqlProvider = synologenSqlProvider;
			_userContextService = userContextService;
		}

		public void CancelOrder(int orderId)
		{
			var order = _synologenSqlProvider.GetOrder(orderId);
			order.StatusId = InvoiceCanceledStatusId;
			_synologenSqlProvider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
			var userName = _userContextService.GetLoggedInUser().User.UserName;
			_synologenSqlProvider.AddOrderHistory(orderId, OrderCancelHistoryMessage.Replace("{UserName}", userName));
		}

		public void AddArticle(Article article)
		{
			_synologenSqlProvider.AddUpdateDeleteArticle(Enumerations.Action.Create, ref article);
		}

		public void UpdateArticle(Article article)
		{
			var articleToUpdate = _synologenSqlProvider.GetArticle(article.Id);
			articleToUpdate.DefaultSPCSAccountNumber = article.DefaultSPCSAccountNumber;
			articleToUpdate.Description = article.Description;
			articleToUpdate.Name = article.Name;
			articleToUpdate.Number = article.Number;
			_synologenSqlProvider.AddUpdateDeleteArticle(Enumerations.Action.Update, ref articleToUpdate);
		}

		public void AddContractArticle(ContractArticleConnection contractArticleConnection)
		{
			var connection = new Business.Domain.Entities.ContractArticleConnection
			{
				Active = contractArticleConnection.Active,
				ArticleId = contractArticleConnection.ArticleId,
				ContractCustomerId = contractArticleConnection.ContractCustomerId,
				EnableManualPriceOverride = contractArticleConnection.EnableManualPriceOverride,
				NoVAT = contractArticleConnection.NoVAT,
				Price = (float) contractArticleConnection.Price,
				SPCSAccountNumber = contractArticleConnection.SPCSAccountNumber,
                CustomerArticleNumber = contractArticleConnection.CustomerArticleNumber,
                DiscountId = contractArticleConnection.DiscountId,

			};

			_synologenSqlProvider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref connection);
		}

		public void UpdateContractArticle(ContractArticleConnection contractArticleConnection)
		{
			var contractArticle = _synologenSqlProvider.GetContractCustomerArticleRow(contractArticleConnection.Id);

			contractArticle.Active = contractArticleConnection.Active;
			contractArticle.ArticleId = contractArticleConnection.ArticleId;
			contractArticle.ContractCustomerId = contractArticleConnection.ContractCustomerId;
			contractArticle.EnableManualPriceOverride = contractArticleConnection.EnableManualPriceOverride;
			contractArticle.NoVAT = contractArticleConnection.NoVAT;
			contractArticle.Price = (float) contractArticleConnection.Price;
			contractArticle.SPCSAccountNumber = contractArticleConnection.SPCSAccountNumber;
			contractArticle.CustomerArticleNumber = contractArticleConnection.CustomerArticleNumber;
			contractArticle.DiscountId = contractArticleConnection.DiscountId;

			_synologenSqlProvider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Update, ref contractArticle);
		}
	}
}