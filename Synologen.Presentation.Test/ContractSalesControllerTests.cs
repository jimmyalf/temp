using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FakeItEasy;
using Moq;
using NHibernate.Criterion;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Queries;
using Spinit.Wpc.Synologen.Data.Queries.Finance;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.Order;
using Spinit.Wpc.Synologen.Presentation.Test.TestHelpers;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;
using Order = Spinit.Wpc.Synologen.Business.Domain.Entities.Order;
using Settlement=Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Settlement;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Shop;
using SubscriptionFactory = Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales.SubscriptionFactory;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_settlement_view : ContractSalesTestbase<SettlementView>
	{
		private Settlement _settlement;
		private Shop _shop1;
		private Shop _shop2;
		private IEnumerable<ContractSale> _contractSales, _contractSalesForShop1, _contractSalesForShop2;
		private NewSubscription _newSubscription1, _newSubscription2;
		private OldSubscription _oldSubscription1, _oldSubscription2;
		private IEnumerable<OldTransaction> _oldTransactions, _oldTransactionsForShop1, _oldTransactionsForShop2;
		private IEnumerable<NewTransaction> _newTransactions, _newTransactionsForShop1, _newTransactionsForShop2;

		public When_loading_settlement_view()
		{
			Context = () =>
			{
				_shop1 = ShopFactory.GetShop(4, "Göteborgs Ögon", "1020");
				_shop2 = ShopFactory.GetShop(5, "Stockholms Ögon", "2020");
				_newSubscription1 = SubscriptionFactory.GetNew(1, _shop1);
				_newSubscription2 = SubscriptionFactory.GetNew(2, _shop2);

				_oldSubscription1 = SubscriptionFactory.GetOld(1, 1, _shop1);
				_oldSubscription2 = SubscriptionFactory.GetOld(2, 2, _shop2);

				_contractSalesForShop1 = SettlementFactory.GetContractSales(_shop1);
				_contractSalesForShop2 = SettlementFactory.GetContractSales(_shop2);
				_contractSales = _contractSalesForShop1.Append(_contractSalesForShop2);

				_oldTransactionsForShop1 = SettlementFactory.GetOldTransactions(_oldSubscription1);
				_oldTransactionsForShop2 = SettlementFactory.GetOldTransactions(_oldSubscription2);
				_oldTransactions = _oldTransactionsForShop1.Append(_oldTransactionsForShop2);

				_newTransactionsForShop1 = SettlementFactory.GetNewTransactions(_newSubscription1);
				_newTransactionsForShop2 = SettlementFactory.GetNewTransactions(_newSubscription2);
				_newTransactions = _newTransactionsForShop1.Append(_newTransactionsForShop2);
				_settlement = SettlementFactory.Get(6, _contractSales, _oldTransactions, _newTransactions);
				MockedSettlementRepository.Setup(x => x.Get(It.Is<int>(id => id.Equals(_settlement.Id)))).Returns(_settlement);
			};

			Because = controller => controller.ViewSettlement(_settlement.Id);
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			var expectedSum = _contractSales.Sum(x => x.TotalAmountIncludingVAT)
				+ _oldTransactions.Sum(x => x.Amount)
				+ _newTransactions.Select(x => x.GetAmount()).Sum(x => x.Total);
			ViewModel.CreatedDate.ShouldBe(_settlement.CreatedDate.ToString("yyyy-MM-dd HH:mm"));
			ViewModel.Id.ShouldBe(_settlement.Id);
			ViewModel.Period.ShouldBe("1045");
			ViewModel.SumAmountIncludingVAT.ShouldBe(expectedSum.ToString("C2"));
			ViewModel.SettlementItems.Count().ShouldBe(2);

			ViewModel.SettlementItems.ForElementAtIndex( 0, settlementItem =>  
			{
				var expectedSumForShop1 = _contractSalesForShop1.Sum(x => x.TotalAmountIncludingVAT)
				+ _oldTransactionsForShop1.Sum(x => x.Amount)
				+ _newTransactionsForShop1.Select(x => x.GetAmount()).Sum(x => x.Total);
				settlementItem.BankGiroNumber.ShouldBe(_shop1.BankGiroNumber);
				settlementItem.ShopDescription.ShouldBe(_shop1.Number + " - " + _shop1.Name);
			    settlementItem.NumberOfContractSalesInSettlement.ShouldBe(_contractSalesForShop1.Count());
				settlementItem.NumberOfOldTransactionsInSettlement.ShouldBe(_oldTransactionsForShop1.Count());
				settlementItem.NumberOfNewTransactionsInSettlement.ShouldBe(_newTransactionsForShop1.Count());
			    settlementItem.SumAmountIncludingVAT.ShouldBe(expectedSumForShop1.ToString("C2"));
			});
			ViewModel.SettlementItems.ForElementAtIndex( 1, settlementItem =>  
			{
				var expectedSumForShop2 = _contractSalesForShop2.Sum(x => x.TotalAmountIncludingVAT)
				+ _oldTransactionsForShop2.Sum(x => x.Amount)
				+ _newTransactionsForShop2.Select(x => x.GetAmount()).Sum(x => x.Total);
				settlementItem.BankGiroNumber.ShouldBe(_shop2.BankGiroNumber);
				settlementItem.ShopDescription.ShouldBe(_shop2.Number + " - " + _shop2.Name);
			    settlementItem.NumberOfContractSalesInSettlement.ShouldBe(_contractSalesForShop2.Count());
				settlementItem.NumberOfOldTransactionsInSettlement.ShouldBe(_oldTransactionsForShop2.Count());
				settlementItem.NumberOfNewTransactionsInSettlement.ShouldBe(_newTransactionsForShop2.Count());
			    settlementItem.SumAmountIncludingVAT.ShouldBe(expectedSumForShop2.ToString("C2"));
			});
		}
	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_new_settlementable_contract_sales_and_transactions : ContractSalesTestbase<SettlementListView>
	{
		private readonly IList<Settlement> _settlements;
		private readonly IList<ContractSale> _expectedContractSalesReadyForInvocing;
		private readonly int _readyForSettlementStatus;
		private readonly IList<SubscriptionTransaction> _oldTransactionsReadyForInvocing;

		public When_loading_settlements_list_with_new_settlementable_contract_sales_and_transactions()
		{
			_readyForSettlementStatus = 6;
			_expectedContractSalesReadyForInvocing = ContractSaleFactory.GetList(23).ToList();
			_oldTransactionsReadyForInvocing = SubscriptionTransactionFactory.GetList().ToList();
			_settlements = SettlementFactory.GetList().ToList();

			Context = () =>
			{
				Interceptor.SetupQueryResult<All<Settlement>>(_settlements);
				Interceptor.SetupSessionReturning<IList<ContractSale>>(_expectedContractSalesReadyForInvocing);
				Interceptor.SetupQueryResult<GetOldAutogiroTransactionsReadyForSettlement>(_oldTransactionsReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(_readyForSettlementStatus);
			};

			Because = controller => controller.Settlements();
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			ViewModel.NumberOfContractSalesReadyForInvocing.ShouldBe(_expectedContractSalesReadyForInvocing.Count());
			ViewModel.NumberOfOldTransactionsReadyForInvocing.ShouldBe(_oldTransactionsReadyForInvocing.Count());
			ViewModel.Settlements.Count().ShouldBe(_settlements.Count());
			ViewModel.Settlements.For((index, settlement) =>
			{
			    settlement.CreatedDate.ShouldBe(_settlements.ElementAt(index).CreatedDate.ToString("yyyy-MM-dd HH:mm"));
			    settlement.Id.ShouldBe(_settlements.ElementAt(index).Id);
			    settlement.NumberOfContractSalesInSettlement.ShouldBe(_settlements.ElementAt(index).ContractSales.Count());
			    settlement.NumberOfOldTransactionsInSettlement.ShouldBe(_settlements.ElementAt(index).OldTransactions.Count());
			});
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			ViewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

		//[Test]
		//public void Settings_service_is_called_to_get_settlementable_contract_sale_status()
		//{
		//    MockedSettingsService.Verify(x => x.GetContractSalesReadyForSettlementStatus(), Times.Once());
		//}

		//[Test]
		//public void All_contract_sales_with_given_status_are_fetched()
		//{
		//    MockedContractSaleRepository.Verify(x => x.FindBy(It.Is<AllContractSalesMatchingCriteria>(criteria => criteria.ContractSaleStatus.Equals(_readyForSettlementStatus))), Times.Once());
		//}

		//[Test]
		//public void All_settlements_are_fetched()
		//{
		//    MockedSettlementRepository.Verify(x => x.GetAll(), Times.Once());
		//}

		//[Test]
		//public void Expected_transactions_ready_for_settlement_are_fetched()
		//{
		//    MockedTransactionRepository.Verify(x => x.FindBy(It.Is<AllTransactionsMatchingCriteria>( criteria => 
		//        criteria.Reason.Equals(TransactionReason.Payment) &&
		//        criteria.Type.Equals(TransactionType.Deposit) &&
		//        criteria.SettlementStatus.Equals(SettlementStatus.DoesNotHaveSettlement)
		//    )));
		//}
	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_settlementable_contract_sales : ContractSalesTestbase<SettlementListView>
	{
		public When_loading_settlements_list_with_settlementable_contract_sales()
		{
			var expectedContractSalesReadyForInvocing = ContractSaleFactory.GetList(15).ToList();
			const int readyForSettlementStatus = 6;
			Context = () => 
			{
				Interceptor.SetupSessionResult(s => s
				    .CreateCriteria<ContractSale>()
				    .Add(Restrictions.Eq("StatusId", readyForSettlementStatus))
				    .List<ContractSale>(), expectedContractSalesReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(readyForSettlementStatus);
			};

			Because = controller => controller.Settlements();
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			ViewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_settlementable_oldtransactions : ContractSalesTestbase<SettlementListView>
	{
		public When_loading_settlements_list_with_settlementable_oldtransactions()
		{
			const int readyForSettlementStatus = 6;
			var expectedLensSubscriptionTransactionsReadyForInvocing = SubscriptionTransactionFactory.GetList();
			Context = () =>
			{
				Interceptor.SetupQueryResult<GetOldAutogiroTransactionsReadyForSettlement>(expectedLensSubscriptionTransactionsReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(readyForSettlementStatus);
			};
			Because = controller => controller.Settlements();
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			ViewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_settlementable_newtransactions : ContractSalesTestbase<SettlementListView>
	{
		public When_loading_settlements_list_with_settlementable_newtransactions()
		{
			const int readyForSettlementStatus = 6;
			var newTransactionsForSettlement = Factory.GetTransactions();
			Context = () =>
			{
				Interceptor.SetupQueryResult<GetNewAutogiroTransactionsReadyForSettlement>(newTransactionsForSettlement);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(readyForSettlementStatus);
			};
			Because = controller => controller.Settlements();
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			ViewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_no_settlementable_contract_sales_or_transactions : ContractSalesTestbase<SettlementListView>
	{
		public When_loading_settlements_list_with_no_settlementable_contract_sales_or_transactions()
		{
			const int readyForSettlementStatus = 6;
			Context = () => MockedSettingsService
				.Setup(x => x.GetContractSalesReadyForSettlementStatus())
				.Returns(readyForSettlementStatus);

			Because = controller => controller.Settlements();
		}

		[Test]
		public void Create_settlement_button_should_be_hidden()
		{
			ViewModel.DisplayCreateSettlementsButton.ShouldBe(false);
		}

	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_creating_a_settlement : ContractSalesTestbase<RedirectToRouteResult>
	{
		private readonly int _readyForSettlementStatus;
		private readonly int _afterSettlementStatus;
		private readonly int _expectedNewSettlementId;
		//private readonly IEnumerable<SubscriptionTransaction> _oldTransactions;
		//private readonly IList<Core.Domain.Model.Orders.SubscriptionTransaction> _newTransactions;

		public When_creating_a_settlement()
		{
			_readyForSettlementStatus = 6;
			_afterSettlementStatus = 8;
			_expectedNewSettlementId = 33;
			//_oldTransactions = SubscriptionTransactionFactory.GetList();
			//_newTransactions = Factory.GetSettlementableTransactions();
			Context = () => 
			{
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(_readyForSettlementStatus);
				MockedSettingsService.Setup(x => x.GetContractSalesAfterSettlementStatus()).Returns(_afterSettlementStatus);
				MockedSynologenSqlProvider.Setup(x => x.AddSettlement(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedNewSettlementId);
			};
			Because = controller => controller.CreateSettlement();
		}

		[Test]
		public void Controller_redirects_to_view_page_for_newly_created_settlement()
		{
			ViewModel.RouteValues["action"].ShouldBe("ViewSettlement");
			ViewModel.RouteValues["id"].ShouldBe(_expectedNewSettlementId);
		}

		[Test]
		public void Settings_service_was_called_to_get_contract_sale_statuses()
		{
			MockedSettingsService.Verify(x => x.GetContractSalesReadyForSettlementStatus(), Times.Once());
			MockedSettingsService.Verify(x => x.GetContractSalesAfterSettlementStatus(), Times.Once());
		}

		[Test]
		public void Settlement_gets_created_with_expected_contract_sale_filters()
		{
			MockedSynologenSqlProvider.Verify(x => x.AddSettlement(
				It.Is<int>(statusBefore => statusBefore.Equals(_readyForSettlementStatus)),
				It.Is<int>(statusBefore => statusBefore.Equals(_afterSettlementStatus))
			));
		}

		//[Test]
		//public void Transactions_ready_for_settlement_are_fetched()
		//{
		//    MockedTransactionRepository.Verify(x => x.FindBy(It.Is<AllTransactionsMatchingCriteria>( 
		//        criteria => 
		//            criteria.Reason.Equals(TransactionReason.Payment) &&
		//            criteria.Type.Equals(TransactionType.Deposit) &&
		//            criteria.SettlementStatus.Equals(SettlementStatus.DoesNotHaveSettlement)
		//    )));
		//}

		//[Test]
		//public void All_fetched_old_transactions_get_connected_to_settlement()
		//{
		//    foreach (var transaction in _oldTransactions)
		//    {
		//        A.CallTo(() => Session.Save(A<SubscriptionTransaction>.That.Matches(transactionSaved =>
		//            transactionSaved.Settlement.Id.Equals(_expectedNewSettlementId) &&
		//            transactionSaved.Id.Equals(transaction.Id)
		//        ))).MustHaveHappened();
		//    }

		//}

		//[Test]
		//public void All_fetched_new_transactions_get_connected_to_settlement()
		//{
		//    foreach (var transaction in _newTransactions)
		//    {
		//        A.CallTo(() => Session.Save(A<Core.Domain.Model.Orders.SubscriptionTransaction>.That.Matches(transactionSaved =>
		//            transactionSaved.SettlementId.Equals(_expectedNewSettlementId) &&
		//            transactionSaved.Id.Equals(transaction.Id)
		//        ))).MustHaveHappened();
		//    }
		//}
	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_manage_order_view_with_invoiced_order : ContractSalesTestbase<OrderView>
	{
		private Order _order;
		private OrderStatus _status;

		public When_loading_manage_order_view_with_invoiced_order()
		{
			Context = () =>
			{
				_order = OrderFactory.GetInvoicedOrder();
				_status = OrderFactory.GetOrderStatus();
				MockedSynologenSqlProvider.Setup(x => x.GetOrder(_order.Id)).Returns(_order);
				MockedSynologenSqlProvider.Setup(x => x.GetOrderStatusRow(_order.StatusId)).Returns(_status);
			};
			Because = controller => controller.ManageOrder(_order.Id);
		}


		[Test]
		public void ViewModel_should_have_expected_invoice_values()
		{
			ViewModel.BackUrl.ShouldBe(ComponentPages.Orders.Replace("~",""));
			ViewModel.Id.ShouldBe(_order.Id);
			ViewModel.Status.ShouldBe(_status.Name);
			ViewModel.VISMAInvoiceNumber.ShouldBe(_order.InvoiceNumber.ToString());
		}

		[Test]
		public void ViewModel_should_display_cancel_invoice_button()
		{
			ViewModel.DisplayCancelButton.ShouldBe(true);
		}

		[Test]
		public void ViewModel_should_display_invoice_copy_link()
		{
			ViewModel.DisplayInvoiceCopyLink.ShouldBe(true);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_loading_manage_order_view_with_uninvoiced_order : ContractSalesTestbase<OrderView>
	{
		private Order _order;
		private OrderStatus _status;

		public When_loading_manage_order_view_with_uninvoiced_order()
		{
			Context = () =>
			{
				_order = OrderFactory.GetUnInvoicedOrder();
				_status = OrderFactory.GetOrderStatus();
				MockedSynologenSqlProvider.Setup(x => x.GetOrder(_order.Id)).Returns(_order);
				MockedSynologenSqlProvider.Setup(x => x.GetOrderStatusRow(_order.StatusId)).Returns(_status);
			};
			Because = controller => controller.ManageOrder(_order.Id);
		}


		[Test]
		public void ViewModel_should_not_display_cancel_invoice_button()
		{
			ViewModel.DisplayCancelButton.ShouldBe(false);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_canceling_an_order : ContractSalesTestbase<RedirectResult>
	{
		private Order _order;
		private WpcUser _user;

		public When_canceling_an_order()
		{
			Context = () =>
			{
				_order = OrderFactory.GetInvoicedOrder();
				_user = OrderFactory.GetUser();
				MockedSynologenSqlProvider.Setup(x => x.GetOrder(_order.Id)).Returns(_order);
				A.CallTo(() => UserContextService.GetLoggedInUser()).Returns(_user);
			};
			Because = controller => controller.CancelOrder(_order.Id);
		}

		[Test]
		public void Order_should_be_updated()
		{
			MockedSynologenSqlProvider.Verify(x => x.AddUpdateDeleteOrder(Enumerations.Action.Update, ref _order /* Only tests method call, not parameters because of ref */));
		}

		[Test]
		public void An_order_history_should_be_added()
		{
			var expectedOrderHistory = "Order makulerad manuellt av användare \"{UserName}\".".Replace("{UserName}", _user.User.UserName);
			MockedSynologenSqlProvider.Verify(x => x.AddOrderHistory(_order.Id, expectedOrderHistory));
		}

		[Test]
		public void A_success_message_should_be_added()
		{
			MessageQueue.HasMessages.ShouldBe(true);
			MessageQueue.ReadMessage().IsError.ShouldBe(false);
		}

		[Test]
		public void Controller_redirects()
		{
			ViewModel.Url.ShouldBe(ComponentPages.Orders);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests")]
	public class When_canceling_an_order_fails : ContractSalesTestbase<RedirectToRouteResult>
	{
		private Order _order;
		private WpcUser _user;

		public When_canceling_an_order_fails()
		{
			Context = () =>
			{
				_order = OrderFactory.GetInvoicedOrder();
				_user = OrderFactory.GetUser();
				MockedSynologenSqlProvider.Setup(x => x.GetOrder(_order.Id)).Throws(new Exception("Testexception"));
				A.CallTo(() => UserContextService.GetLoggedInUser()).Returns(_user);
			};
			Because = controller => controller.CancelOrder(_order.Id);
		}

		[Test]
		public void Error_message_is_added()
		{
			var actionMessage = ActionMessages.First();
			actionMessage.Message.ShouldStartWith("Ett fel uppstod vid fakturamakulering: ");
			actionMessage.Type.ShouldBe(WpcActionMessageType.Error);
		}

		[Test]
		public void Controller_redirects_to_manage_order()
		{
			ViewModel.RouteValues["action"].ShouldBe("ManageOrder");
			ViewModel.RouteValues["id"].ShouldBe(_order.Id);
		}
	}
}