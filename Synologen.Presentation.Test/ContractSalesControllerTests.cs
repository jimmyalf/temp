using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlement_view : ContractSalesTestbase
	{
		private readonly int _settlementId;
		private SettlementView _viewModel;
		private readonly ShopSettlement _settlement;

		public When_loading_settlement_view()
		{
			// Arrange
			_settlementId = 5;
			_settlement = SettlementFactory.Get(_settlementId);

			Context = () => {
				MockedSettlementRepository.Setup(x => x.Get(It.Is<int>(id => id.Equals(_settlementId)))).Returns(_settlement);
			};

			Because = () => {
				var controller = new ContractSalesController(ViewService);
				var view = (ViewResult)controller.ViewSettlement(_settlementId);
				_viewModel = (SettlementView)view.ViewData.Model;
			};
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.CreatedDate.ShouldBe(_settlement.CreatedDate.ToString("yyyy-MM-dd HH:mm"));
			_viewModel.Id.ShouldBe(_settlement.Id);
			_viewModel.Period.ShouldBe("1045");
			_viewModel.SumAmountIncludingVAT.ShouldBe((4546.11M).ToString("C2"));
			_viewModel.SettlementItems.Count().ShouldBe(4);
			_viewModel.SettlementItems.Each(settlementItem => 
			{
				settlementItem.BankGiroNumber.ShouldBe("123456987");
				settlementItem.ShopDescription.ShouldBe("1350 - Örebro Optik");
			});
			_viewModel.SettlementItems.ForElementAtIndex( 0, settlementItem =>  
			{
			    settlementItem.NumberOfContractSalesInSettlement.ShouldBe(2);
				settlementItem.NumberOfLensSubscriptionTransactionsInSettlement.ShouldBe(4);
			    settlementItem.SumAmountIncludingVAT.ShouldBe(1592.87M.ToString("C2"));
			});
			_viewModel.SettlementItems.ForElementAtIndex( 1, settlementItem =>  
			{
			    settlementItem.NumberOfContractSalesInSettlement.ShouldBe(3);
				settlementItem.NumberOfLensSubscriptionTransactionsInSettlement.ShouldBe(2);
			    settlementItem.SumAmountIncludingVAT.ShouldBe(2145.34M.ToString("C2"));
			});
			_viewModel.SettlementItems.ForElementAtIndex( 2, settlementItem =>  
			{
			    settlementItem.NumberOfContractSalesInSettlement.ShouldBe(1);
				settlementItem.NumberOfLensSubscriptionTransactionsInSettlement.ShouldBe(0);
			    settlementItem.SumAmountIncludingVAT.ShouldBe(678.90M.ToString("C2"));
			});
			_viewModel.SettlementItems.ForElementAtIndex( 3, settlementItem =>  
			{
			    settlementItem.NumberOfContractSalesInSettlement.ShouldBe(0);
				settlementItem.NumberOfLensSubscriptionTransactionsInSettlement.ShouldBe(1);
			    settlementItem.SumAmountIncludingVAT.ShouldBe(129M.ToString("C2"));
			});
		}
	}

	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_new_settlementable_contract_sales_and_transactions : ContractSalesTestbase
	{
		private SettlementListView _viewModel;
		private readonly IList<ShopSettlement> _settlements;
		private readonly IEnumerable<ContractSale> _expectedContractSalesReadyForInvocing;
		private readonly int _readyForSettlementStatus;
		private readonly IEnumerable<SubscriptionTransaction> _expectedLensSubscriptionTransactionsReadyForInvocing;
		

		public When_loading_settlements_list_with_new_settlementable_contract_sales_and_transactions()
		{
			// Arrange
			_readyForSettlementStatus = 6;
			_expectedContractSalesReadyForInvocing = ContractSaleFactory.GetList(23);
			_expectedLensSubscriptionTransactionsReadyForInvocing = SubscriptionTransactionFactory.GetList();
			_settlements = SettlementFactory.GetList().ToList();

			Context = () =>
			{
				MockedSettlementRepository.Setup(x => x.GetAll()).Returns(_settlements);
				MockedContractSaleRepository.Setup(x => x.FindBy(It.IsAny<AllContractSalesMatchingCriteria>())).Returns(_expectedContractSalesReadyForInvocing);
				MockedTransactionRepository.Setup(x => x.FindBy(It.IsAny<AllTransactionsMatchingCriteria>())).Returns(_expectedLensSubscriptionTransactionsReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(_readyForSettlementStatus);
			};

			Because = () =>
			{
				var controller = new ContractSalesController(ViewService);
				var view = (ViewResult) controller.Settlements();
				_viewModel = (SettlementListView) view.ViewData.Model;
			};
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.NumberOfContractSalesReadyForInvocing.ShouldBe(_expectedContractSalesReadyForInvocing.Count());
			_viewModel.NumberOfLensSubscriptionTransactionsReadyForInvocing.ShouldBe(_expectedLensSubscriptionTransactionsReadyForInvocing.Count());
			_viewModel.Settlements.Count().ShouldBe(_settlements.Count());
			_viewModel.Settlements.For((index, settlement) =>
			{
			    settlement.CreatedDate.ShouldBe(_settlements.ElementAt(index).CreatedDate.ToString("yyyy-MM-dd HH:mm"));
			    settlement.Id.ShouldBe(_settlements.ElementAt(index).Id);
			    settlement.NumberOfContractSalesInSettlement.ShouldBe(_settlements.ElementAt(index).ContractSales.Count());
			    settlement.NumberOfLensSubscriptionTransactionsInSettlement.ShouldBe(_settlements.ElementAt(index).LensSubscriptionTransactions.Count());
			});
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			_viewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

		[Test]
		public void Settings_service_is_called_to_get_settlementable_contract_sale_status()
		{
			MockedSettingsService.Verify(x => x.GetContractSalesReadyForSettlementStatus(), Times.Once());
		}

		[Test]
		public void All_contract_sales_with_given_status_are_fetched()
		{
			MockedContractSaleRepository.Verify(x => x.FindBy(It.Is<AllContractSalesMatchingCriteria>(criteria => criteria.ContractSaleStatus.Equals(_readyForSettlementStatus))), Times.Once());
		}

		[Test]
		public void All_settlements_are_fetched()
		{
			MockedSettlementRepository.Verify(x => x.GetAll(), Times.Once());
		}

		[Test]
		public void Expected_transactions_ready_for_settlement_are_fetched()
		{
			MockedTransactionRepository.Verify(x => x.FindBy(It.Is<AllTransactionsMatchingCriteria>( criteria => 
				criteria.Reason.Equals(TransactionReason.Payment) &&
				criteria.Type.Equals(TransactionType.Deposit) &&
				criteria.SettlementStatus.Equals(SettlementStatus.DoesNotHaveSettlement)
			)));
		}
	}

	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_settlementable_contract_sales : ContractSalesTestbase
	{
		private SettlementListView _viewModel;

		public When_loading_settlements_list_with_settlementable_contract_sales()
		{
			var expectedContractSalesReadyForInvocing = ContractSaleFactory.GetList(15);
			var expectedLensSubscriptionTransactionsReadyForInvocing = Enumerable.Empty<SubscriptionTransaction>();
			const int readyForSettlementStatus = 6;
			var settlements = SettlementFactory.GetList();

			Context = () => 
			{
				MockedSettlementRepository.Setup(x => x.GetAll()).Returns(settlements);
				MockedContractSaleRepository.Setup(x => x.FindBy(It.IsAny<AllContractSalesMatchingCriteria>())).Returns(expectedContractSalesReadyForInvocing);
				MockedTransactionRepository.Setup(x => x.FindBy(It.IsAny<AllTransactionsMatchingCriteria>())).Returns(expectedLensSubscriptionTransactionsReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(readyForSettlementStatus);
			};

			Because = () =>
			{
				var controller = new ContractSalesController(ViewService);
				var view = (ViewResult) controller.Settlements();
				_viewModel = (SettlementListView) view.ViewData.Model;
			};
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			_viewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

	}

	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_settlementable_transactions : ContractSalesTestbase
	{
		private SettlementListView _viewModel;

		public When_loading_settlements_list_with_settlementable_transactions()
		{
			const int readyForSettlementStatus = 6;
			var expectedContractSalesReadyForInvocing = Enumerable.Empty<ContractSale>();
			var expectedLensSubscriptionTransactionsReadyForInvocing = SubscriptionTransactionFactory.GetList();
			var settlements = SettlementFactory.GetList();
			Context = () => 
			{
				MockedSettlementRepository.Setup(x => x.GetAll()).Returns(settlements);
				MockedContractSaleRepository.Setup(x => x.FindBy(It.IsAny<AllContractSalesMatchingCriteria>())).Returns(expectedContractSalesReadyForInvocing);
				MockedTransactionRepository.Setup(x => x.FindBy(It.IsAny<AllTransactionsMatchingCriteria>())).Returns(expectedLensSubscriptionTransactionsReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(readyForSettlementStatus);
			};
			Because = () =>
			{
				var controller = new ContractSalesController(ViewService);
				var view = (ViewResult) controller.Settlements();
				_viewModel = (SettlementListView) view.ViewData.Model;
			};
		}

		[Test]
		public void Create_settlement_button_should_be_visible()
		{
			_viewModel.DisplayCreateSettlementsButton.ShouldBe(true);
		}

	}

	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list_with_no_settlementable_contract_sales_or_transactions : ContractSalesTestbase
	{
		private SettlementListView _viewModel;

		public When_loading_settlements_list_with_no_settlementable_contract_sales_or_transactions()
		{
			var expectedContractSalesReadyForInvocing = Enumerable.Empty<ContractSale>();
			var expectedLensSubscriptionTransactionsReadyForInvocing = Enumerable.Empty<SubscriptionTransaction>();
			const int readyForSettlementStatus = 6;
			var settlements = SettlementFactory.GetList();
			Context = () => 
			{
				MockedSettlementRepository.Setup(x => x.GetAll()).Returns(settlements);
				MockedContractSaleRepository.Setup(x => x.FindBy(It.IsAny<AllContractSalesMatchingCriteria>())).Returns(expectedContractSalesReadyForInvocing);
				MockedTransactionRepository.Setup(x => x.FindBy(It.IsAny<AllTransactionsMatchingCriteria>())).Returns(expectedLensSubscriptionTransactionsReadyForInvocing);
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(readyForSettlementStatus);
			};
			Because = () =>
			{
				var controller = new ContractSalesController(ViewService);
				var view = (ViewResult) controller.Settlements();
				_viewModel = (SettlementListView) view.ViewData.Model;
			};
		}

		[Test]
		public void Create_settlement_button_should_be_hidden()
		{
			_viewModel.DisplayCreateSettlementsButton.ShouldBe(false);
		}

	}

	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_creating_a_settlement : ContractSalesTestbase
	{
		private readonly int _readyForSettlementStatus;
		private readonly int _afterSettlementStatus;
		private RedirectToRouteResult _redirectToRouteResult;
		private readonly int _expectedNewSettlementId;
		private readonly IEnumerable<SubscriptionTransaction> _transactions;

		public When_creating_a_settlement()
		{
			_readyForSettlementStatus = 6;
			_afterSettlementStatus = 8;
			_expectedNewSettlementId = 33;
			_transactions = SubscriptionTransactionFactory.GetList();
			Context = () => 
			{
				MockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(_readyForSettlementStatus);
				MockedSettingsService.Setup(x => x.GetContractSalesAfterSettlementStatus()).Returns(_afterSettlementStatus);
				MockedSynologenSqlProvider.Setup(x => x.AddSettlement(It.IsAny<int>(), It.IsAny<int>())).Returns(_expectedNewSettlementId);
			};
			Because = () =>
			{
				var controller = new ContractSalesController(ViewService);
				_redirectToRouteResult = (RedirectToRouteResult) controller.CreateSettlement();
			};
		}

		[Test]
		public void Controller_redirects_to_view_page_for_newly_created_settlement()
		{
			_redirectToRouteResult.RouteValues["action"].ShouldBe("ViewSettlement");
			_redirectToRouteResult.RouteValues["id"].ShouldBe(_expectedNewSettlementId);
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

		//TODO: Tests for connecting transactions
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
		//public void All_fetched_transactions_get_connected_to_settlement()
		//{
		//    _transactions.Each( transaction => MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(
		//        transactionSaved => transactionSaved.Settlement.Id.Equals(transaction.Id)
		//    ))));
		//}
	}
}