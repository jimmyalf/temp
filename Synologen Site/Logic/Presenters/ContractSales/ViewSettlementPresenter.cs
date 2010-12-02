using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Site.Models.ContractSales;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.ContractSales
{
	public class ViewSettlementPresenter : Presenter<IViewSettlementView>
	{
		private readonly ISettlementRepository _settlementRepository;
		private readonly ISynologenMemberService _synologenMemberService;

		public ViewSettlementPresenter(IViewSettlementView view, ISettlementRepository settlementRepository, ISynologenMemberService synologenMemberService) : base(view)
		{
			_settlementRepository = settlementRepository;
			_synologenMemberService = synologenMemberService;
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var settlementId = HttpContext.Request["settlement"].ToIntOrDefault();
			var shopId = _synologenMemberService.GetCurrentShopId();
			var settlementForShop = _settlementRepository.GetForShop(settlementId, shopId);
			View.Model.SettlementId = settlementForShop.Id;
			View.Model.ShopNumber = settlementForShop.Shop.Number;
			View.Model.Period = Business.Utility.General.GetSettlementPeriodNumber(settlementForShop.CreatedDate);
			View.Model.ContractSalesValueIncludingVAT = settlementForShop.ContractSalesValueIncludingVAT.ToString("C2");
			View.Model.DetailedContractSales = settlementForShop.SaleItems.Select(DetailedSalesItemConverter);
			View.Model.SimpleContractSales = settlementForShop.SaleItems.OrderBy(x => x.Article.Id).GroupBy(x => x.Article.Id).Select(SimpleSalesItemConverter);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
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
	}
}