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
			var subscriptionPageUrl = SubscriptionPageUrlResolver(View.SubscriptionPageId);

			View.Model.DisplayDetailedView = useDetailedView;
			View.Model.SettlementId = settlementForShop.Id;
			View.Model.ShopNumber = settlementForShop.Shop.Number;
			View.Model.Period = Business.Utility.General.GetSettlementPeriodNumber(settlementForShop.CreatedDate);
			View.Model.ContractSalesValueIncludingVAT = settlementForShop.ContractSalesValueIncludingVAT.ToString("C2");
			View.Model.LensSubscriptionsValueIncludingVAT = settlementForShop.LensSubscriptionsValueIncludingVAT.ToString("C2");
			View.Model.LensSubscriptionTransactionsCount = settlementForShop.LensSubscriptionTransactions.Count().ToString();
			View.Model.DetailedSubscriptionTransactions = settlementForShop.LensSubscriptionTransactions.Select(transaction => DetailedTransactionsConverter(transaction, subscriptionPageUrl)); 
			View.Model.DetailedContractSales = settlementForShop.SaleItems.Select(DetailedSalesItemConverter);
			View.Model.SimpleContractSales = settlementForShop.SaleItems.OrderBy(x => x.Article.Id).GroupBy(x => x.Article.Id).Select(SimpleSalesItemConverter);
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


		private static Func<SaleItem,SettlementDetailedContractSaleListItemModel> DetailedSalesItemConverter
		{
			get
			{
				return saleItem => new SettlementDetailedContractSaleListItemModel
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
		}

		private static Func<IGrouping<int,SaleItem>,SettlementSimpleContractSaleListItemModel> SimpleSalesItemConverter
		{
			get
			{
				return saleItemGroup => new SettlementSimpleContractSaleListItemModel
				{
					ArticleName = saleItemGroup.FirstOrDefault().Return(x => x.Article.Name, String.Empty),
					ArticleNumber = saleItemGroup.FirstOrDefault().Return(x => x.Article.Number, String.Empty),
					IsVATFree = saleItemGroup.FirstOrDefault().Return(x => x.IsVATFree, false) ? "Ja" : "Nej",
					Quantity = saleItemGroup.Sum(x => x.Quantity).ToString(),
					ValueExcludingVAT = saleItemGroup.Sum(x => x.TotalPriceExcludingVAT()).ToString("C2")
				};
			}
		}

		private static Func<OldTransaction, string, SettlementDetailedSubscriptionTransactionsListItemModel> DetailedTransactionsConverter
		{
			get
			{
				return (transactionItem, subscriptionPageUrl) => new SettlementDetailedSubscriptionTransactionsListItemModel
              	{
              		Amount = transactionItem.Amount.ToString("C2"),
              		Date = transactionItem.CreatedDate.ToString("yyyy-MM-dd"),
					SubscriptionLink = String.Format("{0}?subscription={1}", subscriptionPageUrl, transactionItem.Subscription.Id),
					CustomerName = transactionItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName)
              	};
			}
		}


		private Func<int, string> SubscriptionPageUrlResolver
		{
			get
			{
				return pageId => (pageId <= 0) ? "#" : _routingService.GetPageUrl(pageId);
			}
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.SwitchView -= View_SwitchView;
			View.MarkAllSaleItemsAsPayed -= View_MarkAllSaleItemsAsPayed;
		}
	}
}