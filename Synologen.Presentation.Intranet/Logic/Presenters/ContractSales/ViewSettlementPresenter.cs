using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.ContractSales;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.ContractSales
{
	public class ViewSettlementPresenter : Presenter<IViewSettlementView>
	{
		private readonly ISettlementRepository _settlementRepository;
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly ISqlProvider _sqlService;
		private readonly IRoutingService _routingService;
		private const string SettlementRequestParameterName = "settlementId";
		private const string UseDetailedSettlementViewSessionKey = "UseDetailedSettlementView";
		private const string SimpleButtonText = "Visa enkelt";
		private const string DetailedButtonText = "Visa detaljer";

		public ViewSettlementPresenter(
			IViewSettlementView view, 
			ISettlementRepository settlementRepository, 
			ISynologenMemberService synologenMemberService, 
			ISqlProvider sqlService,
			IRoutingService routingService) : base(view)
		{
			_settlementRepository = settlementRepository;
			_synologenMemberService = synologenMemberService;
			_sqlService = sqlService;
			_routingService = routingService;
			View.Load += View_Load;
			View.SwitchView += View_SwitchView;
			View.MarkAllSaleItemsAsPayed += View_MarkAllSaleItemsAsPayed;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var settlementId = HttpContext.Request.Params[SettlementRequestParameterName].ToIntOrDefault();
			var useDetailedView = HttpContext.Session[UseDetailedSettlementViewSessionKey].ToTypeOrDefault(false);
			var shopId = _synologenMemberService.GetCurrentShopId();
			var settlementForShop = _settlementRepository.GetForShop(settlementId, shopId);
			var oldSubscriptionPageUrl = _routingService.GetPageUrl(View.SubscriptionPageId);
			var newSubscriptionPageUrl = _routingService.GetPageUrl(View.NewSubscriptionPageId);

			View.Model.DisplayDetailedView = useDetailedView;
			View.Model.SettlementId = settlementForShop.Id;
			View.Model.ShopNumber = settlementForShop.Shop.Number;
			View.Model.Period = Business.Utility.General.GetSettlementPeriodNumber(settlementForShop.CreatedDate);
			View.Model.ContractSalesValueIncludingVAT = settlementForShop.ContractSalesValueIncludingVAT.ToString("C2");
			View.Model.OldTransactionsValueIncludingVAT = settlementForShop.OldTransactionValueIncludingVAT.ToString("C2");
			View.Model.NewTransactionsValueIncludingVAT = settlementForShop.NewTransactionValueIncludingVAT.ToString("C2");
			View.Model.OldTransactionsCount = settlementForShop.OldTransactions.Count().ToString();
			View.Model.NewTransactionCount = settlementForShop.NewTransactions.Count().ToString();
			View.Model.OldTransactions = settlementForShop.OldTransactions.Select(transaction => ConvertDetailedOldTransactions(transaction, oldSubscriptionPageUrl));
			View.Model.NewTransactions = settlementForShop.NewTransactions.Select(transaction => ConvertDetailedNewTransactions(transaction, newSubscriptionPageUrl));
			View.Model.DetailedContractSales = settlementForShop.SaleItems.Select(ConvertDetailedSalesItem);
			View.Model.SimpleContractSales = settlementForShop.SaleItems.OrderBy(x => x.Article.Id).GroupBy(x => x.Article.Id).Select(ConvertSimpleSalesItem);
			View.Model.SwitchViewButtonText = (useDetailedView) ? SimpleButtonText : DetailedButtonText;
			View.Model.MarkAsPayedButtonEnabled = !settlementForShop.AllContractSalesHaveBeenMarkedAsPayed;
		}

		public void View_SwitchView(object sender, EventArgs e)
		{
			var newUseDetailedViewSetting = !View.Model.DisplayDetailedView;
			HttpContext.Session[UseDetailedSettlementViewSessionKey] = newUseDetailedViewSetting;
			View.Model.SwitchViewButtonText = (newUseDetailedViewSetting) ? SimpleButtonText : DetailedButtonText;
			View.Model.DisplayDetailedView = newUseDetailedViewSetting;
		}

		public void View_MarkAllSaleItemsAsPayed(object o, EventArgs args)
		{
			var shopId = _synologenMemberService.GetCurrentShopId();
			var settlementId = HttpContext.Request.Params[SettlementRequestParameterName].ToIntOrDefault();
			_sqlService.MarkOrdersInSettlementAsPayedPerShop(settlementId, shopId);
			View.Model.MarkAsPayedButtonEnabled = false;
		}


		private SettlementDetailedContractSaleListItemModel ConvertDetailedSalesItem(SaleItem saleItem)
		{
			return new SettlementDetailedContractSaleListItemModel
			{
				ArticleName = saleItem.Article.Name,
				ArticleNumber = saleItem.Article.Number,
				ContractCompany = saleItem.Sale.ContractCompany.Name,
				ContractSaleId = saleItem.Sale.Id.ToString(),
				IsVATFree = saleItem.IsVATFree ? "Ja" : "Nej",
				Quantity = saleItem.Quantity.ToString(),
				ValueExcludingVAT = saleItem.SingleItemPriceExcludingVAT.ToString("C2")
			};
		}

		private SettlementSimpleContractSaleListItemModel ConvertSimpleSalesItem(IGrouping<int, SaleItem> saleItemGroup)
		{
			return new SettlementSimpleContractSaleListItemModel
			{
				ArticleName = saleItemGroup.FirstOrDefault().Return(x => x.Article.Name, String.Empty),
				ArticleNumber = saleItemGroup.FirstOrDefault().Return(x => x.Article.Number, String.Empty),
				IsVATFree = saleItemGroup.FirstOrDefault().Return(x => x.IsVATFree, false) ? "Ja" : "Nej",
				Quantity = saleItemGroup.Sum(x => x.Quantity).ToString(),
				ValueExcludingVAT = saleItemGroup.Sum(x => x.TotalPriceExcludingVAT()).ToString("C2")
			};
		}

		private SettlementDetailedSubscriptionTransactionsListItemModel ConvertDetailedOldTransactions(OldTransaction transactionItem, string subscriptionPageUrl)
		{
			return new SettlementDetailedSubscriptionTransactionsListItemModel
            {
              	Amount = transactionItem.Amount.ToString("C2"),
              	Date = transactionItem.CreatedDate.ToString("yyyy-MM-dd"),
				SubscriptionLink = String.Format("{0}?subscription={1}", subscriptionPageUrl, transactionItem.Subscription.Id),
				CustomerName = transactionItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName)
            };
		}
		
		private SettlementDetailedSubscriptionTransactionsListItemModel ConvertDetailedNewTransactions (NewTransaction transactionItem, string subscriptionPageUrl)
		{
			return new SettlementDetailedSubscriptionTransactionsListItemModel
            {
              	Amount = transactionItem.Amount.ToString("C2"),
              	Date = transactionItem.CreatedDate.ToString("yyyy-MM-dd"),
				SubscriptionLink = String.Format("{0}?subscription={1}", subscriptionPageUrl, transactionItem.Subscription.Id),
				CustomerName = transactionItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName)
            };
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.SwitchView -= View_SwitchView;
			View.MarkAllSaleItemsAsPayed -= View_MarkAllSaleItemsAsPayed;
		}
	}
}