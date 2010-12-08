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
	public class When_loading_view_settlement_view_in_default_simple_mode : ViewSettlementTestbase
	{
		private readonly string _expectedPeriod;
		private readonly ShopSettlement _expectedSettlement;
		private readonly int _expectedCurrentShopId;
		private readonly string _expectedSubscriptionUrl;
		private readonly int _expectedSubscriptionPageId;
		public When_loading_view_settlement_view_in_default_simple_mode()
		{
			_expectedPeriod = "1048";
			_expectedCurrentShopId = 5;
			_expectedSubscriptionUrl = "/test/url";
			_expectedSubscriptionPageId = 8;
			_expectedSettlement = ShopSettlementFactory.Get(50);
			Context = () =>
			{
				MockedView.SetupGet(x => x.SubscriptionPageId).Returns(_expectedSubscriptionPageId);
				MockedSynologenMemberService.Setup(x => x.GetPageUrl(It.IsAny<int>())).Returns(_expectedSubscriptionUrl);
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedSettlement);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(_expectedCurrentShopId);
				MockedHttpContext.SetupSingleQuery("settlementId", _expectedSettlement.Id.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Switch_view_button_has_expected_text()
		{
			AssertUsing( view => view.Model.SwitchViewButtonText.ShouldBe("Visa detaljer"));
		}

		[Test]
		public void Simple_view_is_displayed()
		{
			AssertUsing( view => view.Model.DisplaySimpleView.ShouldBe(true));
		}

		[Test]
		public void Detailed_view_is_hidden()
		{
			AssertUsing( view => view.Model.DisplayDetailedView.ShouldBe(false));
		}

		[Test]
		public void Mark_as_payed_button_is_enabled()
		{
			AssertUsing( view => view.Model.MarkAsPayedButtonEnabled.ShouldBe(true));
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
				view.Model.SwitchViewButtonText.ShouldBe("Visa detaljer");
				view.Model.LensSubscriptionsValueIncludingVAT.ShouldBe(_expectedSettlement.LensSubscriptionsValueIncludingVAT.ToString("C2"));
				view.Model.LensSubscriptionTransactionsCount.ShouldBe(_expectedSettlement.LensSubscriptionTransactions.Count().ToString());
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

		[Test]
		public void View_model_has_expected_detailed_transaction_properties()
		{
			AssertUsing(view => view.Model.DetailedSubscriptionTransactions.ForBoth(_expectedSettlement.LensSubscriptionTransactions.ToList(), (viewItem, domainItem) =>
			{
				viewItem.CustomerName.ShouldBe(domainItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewItem.SubscriptionLink.ShouldBe(String.Format("{0}?subscription={1}", _expectedSubscriptionUrl, domainItem.Subscription.Id));
				viewItem.Amount.ShouldBe(domainItem.Amount.ToString("C2"));
				viewItem.Date.ShouldBe(domainItem.CreatedDate.ToString("yyyy-MM-dd"));
			}));
		}

		[Test]
		public void Presenter_fetches_current_shop_id_from_member_service()
		{
			MockedSynologenMemberService.Verify(x => x.GetCurrentShopId(), Times.Once());
		}

		[Test]
		public void Presenter_fetches_shop_settlement_from_settlement_respository()
		{
			MockedSettlementRepository.Verify(x => x.GetForShop(
				It.Is<int>( settlementId => settlementId.Equals(_expectedSettlement.Id)),
				It.Is<int>( shopId => shopId.Equals(_expectedCurrentShopId))
			));
		}
	}

	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_loading_view_settlement_view_in_detailed_mode : ViewSettlementTestbase
	{
		private readonly ShopSettlement _expectedSettlement;
		public When_loading_view_settlement_view_in_detailed_mode()
		{
			_expectedSettlement = ShopSettlementFactory.Get(50);
			Context = () =>
			{
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedSettlement);
				MockedHttpContext.SetupSessionValue("UseDetailedSettlementView", true);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Switch_view_button_has_expected_text()
		{
			AssertUsing( view => view.Model.SwitchViewButtonText.ShouldBe("Visa enkelt"));
		}

		[Test]
		public void Detailed_view_is_displayed()
		{
			AssertUsing( view => view.Model.DisplayDetailedView.ShouldBe(true));
		}

		[Test]
		public void Simple_view_is_hidden()
		{
			AssertUsing( view => view.Model.DisplaySimpleView.ShouldBe(false));
		}
	}

	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_loading_view_settlement_where_orders_have_beed_marked_as_payed : ViewSettlementTestbase
	{
		private readonly ShopSettlement _expectedSettlement;
		public When_loading_view_settlement_where_orders_have_beed_marked_as_payed()
		{
			_expectedSettlement = ShopSettlementFactory.Get(50);
			_expectedSettlement.AllContractSalesHaveBeenMarkedAsPayed = true;
			Context = () =>
			{
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedSettlement);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Mark_as_payed_button_is_disabled()
		{
			AssertUsing( view => view.Model.MarkAsPayedButtonEnabled.ShouldBe(false));
		}
	}

	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_switching_settlement_view_mode_to_detailed_mode : ViewSettlementTestbase
	{
		private readonly ShopSettlement _expectedSettlement;
		public When_switching_settlement_view_mode_to_detailed_mode()
		{
			_expectedSettlement = ShopSettlementFactory.Get(50);
			Context = () =>
			{
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedSettlement);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_SwitchView(null, new EventArgs());
			};
		}

		[Test]
		public void Switch_view_button_has_expected_text()
		{
			AssertUsing( view => view.Model.SwitchViewButtonText.ShouldBe("Visa enkelt"));
		}

		[Test]
		public void Detailed_view_is_displayed()
		{
			AssertUsing( view => view.Model.DisplayDetailedView.ShouldBe(true));
		}

		[Test]
		public void Simple_view_is_hidden()
		{
			AssertUsing( view => view.Model.DisplaySimpleView.ShouldBe(false));
		}
	}

	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_clicking_mark_as_payed : ViewSettlementTestbase
	{
		private readonly ShopSettlement _expectedSettlement;
		private readonly int _expectedCurrentShopId;

		public When_clicking_mark_as_payed()
		{
			_expectedSettlement = ShopSettlementFactory.Get(50);
			_expectedCurrentShopId = 5;
			Context = () =>
			{
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedSettlement);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(_expectedCurrentShopId);
				MockedHttpContext.SetupSingleQuery("settlementId", _expectedSettlement.Id.ToString());
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_MarkAllSaleItemsAsPayed(null, new EventArgs());
			};
		}

		[Test]
		public void Mark_as_payed_button_is_disabled()
		{
			AssertUsing( view => view.Model.MarkAsPayedButtonEnabled.ShouldBe(false));
		}

		[Test]
		public void Presenter_marks_all_orders_as_payed()
		{
			MockedSqlProvider.Verify(x => x.MarkOrdersInSettlementAsPayedPerShop(
				It.Is<int>(settlementId => settlementId.Equals(_expectedSettlement.Id)),
				It.Is<int>(settlementId => settlementId.Equals(_expectedCurrentShopId))
			));
		}

	}
}