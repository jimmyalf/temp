using System;
using System.Linq;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests
{
	[TestFixture]
	[Category("CreateCustomerPresenterTester")]
	public class When_loading_view_settlement_view_in_default_simple_mode : ViewSettlementTestbase
	{
		private readonly string _period;
		private readonly ShopSettlement _settlement;
		private readonly int _currentShopId;
		private readonly string _oldSubscriptionUrl;
		private readonly string _newSubscriptionUrl;
		private readonly int _newSubscriptionPageId;
		private readonly int _oldSubscriptionPageId;


		public When_loading_view_settlement_view_in_default_simple_mode()
		{
			_period = "1048";
			_currentShopId = 5;
			_oldSubscriptionUrl = "/test/url/old";
			_newSubscriptionUrl = "/test/url/new";
			_oldSubscriptionPageId = 8;
			_newSubscriptionPageId = 9;
			_settlement = ShopSettlementFactory.Get(50);
			Context = () =>
			{
				A.CallTo(() => View.SubscriptionPageId).Returns(_oldSubscriptionPageId);
				A.CallTo(() => View.NewSubscriptionPageId).Returns(_newSubscriptionPageId);
				A.CallTo(() => RoutingService.GetPageUrl(_oldSubscriptionPageId)).Returns(_oldSubscriptionUrl);
				A.CallTo(() => RoutingService.GetPageUrl(_newSubscriptionPageId)).Returns(_newSubscriptionUrl);
				MockedSettlementRepository.Setup(x => x.GetForShop(It.IsAny<int>(), It.IsAny<int>())).Returns(_settlement);
				MockedSynologenMemberService.Setup(x => x.GetCurrentShopId()).Returns(_currentShopId);
				HttpContext.SetupRequestParameter("settlementId", _settlement.Id.ToString());
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Switch_view_button_has_expected_text()
		{
			View.Model.SwitchViewButtonText.ShouldBe("Visa detaljer");
		}

		[Test]
		public void Simple_view_is_displayed()
		{
			View.Model.DisplaySimpleView.ShouldBe(true);
		}

		[Test]
		public void Detailed_view_is_hidden()
		{
			View.Model.DisplayDetailedView.ShouldBe(false);
		}

		[Test]
		public void Mark_as_payed_button_is_enabled()
		{
			View.Model.MarkAsPayedButtonEnabled.ShouldBe(true);
		}

		[Test]
		public void View_model_has_expected_settment_properties()
		{
			View.Model.SettlementId.ShouldBe(_settlement.Id);
			View.Model.ShopNumber.ShouldBe(_settlement.Shop.Number);
			View.Model.Period.ShouldBe(_period);
			View.Model.ContractSalesValueIncludingVAT.ShouldBe(_settlement.ContractSalesValueIncludingVAT.ToString("C2"));
			View.Model.DetailedContractSales.Count().ShouldBe(_settlement.SaleItems.Count());
			View.Model.SimpleContractSales.Count().ShouldBe(_settlement.SaleItems.Select(x => x.Article.Id).Distinct().Count());
			View.Model.SwitchViewButtonText.ShouldBe("Visa detaljer");
			View.Model.OldTransactionsValueIncludingVAT.ShouldBe(_settlement.OldTransactionValueIncludingVAT.ToString("C2"));
			View.Model.NewTransactionsValueIncludingVAT.ShouldBe(_settlement.NewTransactionValueIncludingVAT.ToString("C2"));
			View.Model.OldTransactionsCount.ShouldBe(_settlement.OldTransactions.Count().ToString());
			View.Model.NewTransactionCount.ShouldBe(_settlement.NewTransactions.Count().ToString());
			View.Model.NewTransactionTaxedValue.ShouldBe(_settlement.NewTransactionTaxedValue.ToString("C2"));
			View.Model.NewTransactionTaxFreeValue.ShouldBe(_settlement.NewTransactionTaxFreeValue.ToString("C2"));
		}

		[Test]
		public void View_model_has_expected_detailed_contract_sales_properties()
		{
			View.Model.DetailedContractSales.And(_settlement.SaleItems).Do( (viewItem, domainItem) =>
			{
				viewItem.ArticleName.ShouldBe(domainItem.Article.Name);
				viewItem.ArticleNumber.ShouldBe(domainItem.Article.Number);
				viewItem.ContractCompany.ShouldBe(domainItem.Sale.ContractCompany.Name);
				viewItem.ContractSaleId.ShouldBe(domainItem.Sale.Id.ToString());
				viewItem.IsVATFree.ShouldBe(domainItem.IsVATFree ? "Ja" : "Nej");
				viewItem.Quantity.ShouldBe(domainItem.Quantity.ToString());
				viewItem.ValueExcludingVAT.ShouldBe(domainItem.SingleItemPriceExcludingVAT.ToString("C2"));
			});
		}

		[Test]
		public void View_model_has_expected_simple_contract_sales_properties()
		{
			View.Model.SimpleContractSales.ForElementAtIndex( 0, viewItem =>
			{
				viewItem.ArticleName.ShouldBe("Artikel 1");
				viewItem.ArticleNumber.ShouldBe("1231");
				viewItem.IsVATFree.ShouldBe("Nej");
				viewItem.Quantity.ShouldBe("12");
				viewItem.ValueExcludingVAT.ShouldBe("666,60 kr");
			});
			View.Model.SimpleContractSales.ForElementAtIndex( 1, viewItem =>
			{
				viewItem.ArticleName.ShouldBe("Artikel 2");
				viewItem.ArticleNumber.ShouldBe("1232");
				viewItem.IsVATFree.ShouldBe("Nej");
				viewItem.Quantity.ShouldBe("7");
				viewItem.ValueExcludingVAT.ShouldBe("388,85 kr");
			});
			View.Model.SimpleContractSales.ForElementAtIndex( 2, viewItem =>
			{
				viewItem.ArticleName.ShouldBe("Artikel 3");
				viewItem.ArticleNumber.ShouldBe("1233");
				viewItem.IsVATFree.ShouldBe("Ja");
				viewItem.Quantity.ShouldBe("9");
				viewItem.ValueExcludingVAT.ShouldBe("499,95 kr");
			});
		}

		[Test]
		public void View_model_has_expected_detailed_old_transaction_properties()
		{
			View.Model.OldTransactions.And(_settlement.OldTransactions).Do((viewItem, domainItem) =>
			{
				viewItem.CustomerName.ShouldBe(domainItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewItem.SubscriptionLink.ShouldBe(String.Format("{0}?subscription={1}", _oldSubscriptionUrl, domainItem.Subscription.Id));
				viewItem.Amount.ShouldBe(domainItem.Amount.ToString("C2"));
				viewItem.Date.ShouldBe(domainItem.CreatedDate.ToString("yyyy-MM-dd"));
			});
		}

		[Test]
		public void View_model_has_expected_detailed_new_transaction_properties()
		{
			View.Model.NewTransactions.And(_settlement.NewTransactions).Do((viewItem, domainItem) =>
			{
				viewItem.CustomerName.ShouldBe(domainItem.Subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
				viewItem.SubscriptionLink.ShouldBe(String.Format("{0}?subscription={1}", _newSubscriptionUrl, domainItem.Subscription.Id));
				viewItem.Amount.ShouldBe(domainItem.GetAmount().Total.ToString("C2"));
				viewItem.TaxedAmount.ShouldBe(domainItem.PendingPayment.TaxedAmount.ToString("C2"));
				viewItem.TaxFreeAmount.ShouldBe(domainItem.PendingPayment.TaxFreeAmount.ToString("C2"));
				viewItem.Date.ShouldBe(domainItem.CreatedDate.ToString("yyyy-MM-dd"));
			});
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
				It.Is<int>( settlementId => settlementId.Equals(_settlement.Id)),
				It.Is<int>( shopId => shopId.Equals(_currentShopId))
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
				HttpContext.SetupSessionValue("UseDetailedSettlementView", true);
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Switch_view_button_has_expected_text()
		{
			View.Model.SwitchViewButtonText.ShouldBe("Visa enkelt");
		}

		[Test]
		public void Detailed_view_is_displayed()
		{
			View.Model.DisplayDetailedView.ShouldBe(true);
		}

		[Test]
		public void Simple_view_is_hidden()
		{
			View.Model.DisplaySimpleView.ShouldBe(false);
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
			View.Model.MarkAsPayedButtonEnabled.ShouldBe(false);
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
			View.Model.SwitchViewButtonText.ShouldBe("Visa enkelt");
		}

		[Test]
		public void Detailed_view_is_displayed()
		{
			View.Model.DisplayDetailedView.ShouldBe(true);
		}

		[Test]
		public void Simple_view_is_hidden()
		{
			View.Model.DisplaySimpleView.ShouldBe(false);
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
				HttpContext.SetupRequestParameter("settlementId", _expectedSettlement.Id.ToString());
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
			View.Model.MarkAsPayedButtonEnabled.ShouldBe(false);
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