using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests
{
	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class  When_loading_viewe_settlement_view : ViewSettlementTestbase
	{
		private readonly string _expectedPeriod;
		private readonly ShopSettlement _expectedSettlement;
		public When_loading_viewe_settlement_view()
		{
			_expectedPeriod = "1048";
			_expectedSettlement = ShopSettlementFactory.Get(50);
			Context = () =>
			{
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedSettlement);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void View_model_has_expected_settment_properties()
		{
			AssertUsing( view =>
			{
				view.Model.SettlementId.ShouldBe(_expectedSettlement.Id);
				view.Model.ShopNumber.ShouldBe(_expectedSettlement.Shop.Number);
				view.Model.Period.ShouldBe(_expectedPeriod);
				view.Model.ContractSalesValueIncludingVAT.ShouldBe(_expectedSettlement.ContractSalesValueIncludingVAT.ToString("C2"));
				view.Model.DetailedContractSales.Count().ShouldBe(_expectedSettlement.SaleItems.Count());
				view.Model.SimpleContractSales.Count().ShouldBe(_expectedSettlement.SaleItems.Select(x => x.Article.Id).Distinct().Count());
			});
		}

		[Test]
		public void View_model_has_expected_detailed_contract_sales_properties()
		{
			AssertUsing( view => view.Model.DetailedContractSales.ForBoth(_expectedSettlement.SaleItems.ToList(), (viewItem, domainItem) =>
			{
				viewItem.ArticleName.ShouldBe(domainItem.Article.Name);
				viewItem.ArticleNumber.ShouldBe(domainItem.Article.Number);
				viewItem.ContractCompany.ShouldBe(domainItem.Sale.ContractCompany.Name);
				viewItem.ContractSaleId.ShouldBe(domainItem.Sale.Id.ToString());
				viewItem.IsVATFree.ShouldBe(domainItem.IsVATFree ? "Ja" : "Nej");
				viewItem.Quantity.ShouldBe(domainItem.Quantity.ToString());
				viewItem.ValueExcludingVAT.ShouldBe(domainItem.SingleItemPriceExcludingVAT.ToString("C2"));
			}));
		}

		[Test]
		public void View_model_has_expected_simple_contract_sales_properties()
		{
			AssertUsing( view => view.Model.SimpleContractSales.ForElementAtIndex( 0, viewItem =>
			{
				viewItem.ArticleName.ShouldBe("Artikel 1");
				viewItem.ArticleNumber.ShouldBe("1231");
				viewItem.IsVATFree.ShouldBe("Nej");
				viewItem.Quantity.ShouldBe("12");
				viewItem.ValueExcludingVAT.ShouldBe("666,60 kr");
			}));
			AssertUsing( view => view.Model.SimpleContractSales.ForElementAtIndex( 1, viewItem =>
			{
				viewItem.ArticleName.ShouldBe("Artikel 2");
				viewItem.ArticleNumber.ShouldBe("1232");
				viewItem.IsVATFree.ShouldBe("Nej");
				viewItem.Quantity.ShouldBe("7");
				viewItem.ValueExcludingVAT.ShouldBe("388,85 kr");
			}));
			AssertUsing( view => view.Model.SimpleContractSales.ForElementAtIndex( 2, viewItem =>
			{
				viewItem.ArticleName.ShouldBe("Artikel 3");
				viewItem.ArticleNumber.ShouldBe("1233");
				viewItem.IsVATFree.ShouldBe("Ja");
				viewItem.Quantity.ShouldBe("9");
				viewItem.ValueExcludingVAT.ShouldBe("499,95 kr");
			}));
		}
	}
}