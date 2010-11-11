using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlement_view
	{
		private readonly int _settlementId;
		private readonly SettlementView _viewModel;
		private readonly ShopSettlement _settlement;

		public When_loading_settlement_view()
		{
			// Arrange
			_settlementId = 5;
			_settlement = SettlementFactory.Get(_settlementId);
			var mockedSettlementRepository = new Mock<ISettlementRepository>();
			var mockedContractSaleRepository = new Mock<IContractSaleRepository>();
			var mockedSettingsService = new Mock<IAdminSettingsService>();
			mockedSettlementRepository.Setup(x => x.Get(It.Is<int>(id => id.Equals(_settlementId)))).Returns(_settlement);


			var viewService = new ContractSalesViewService(mockedSettlementRepository.Object, mockedContractSaleRepository.Object, mockedSettingsService.Object);
			
			var controller = new ContractSalesController(viewService);

			var view = (ViewResult) controller.ViewSettlement(_settlementId);
			_viewModel = (SettlementView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			var sales = _settlement.ContractSales.OrderBy(x => x.Shop.Id);
			var amountsIncludingVAT = sales.GroupBy(x => x.Shop.Id).Select(x => x.Sum(y => y.TotalAmountIncludingVAT));
			var amountsExcludingVAT = sales.GroupBy(x => x.Shop.Id).Select(x => x.Sum(y => y.TotalAmountExcludingVAT));
			var shops = sales.GroupBy(x => x.Shop.Id, x => x.Shop).Select(x => x.FirstOrDefault());
			var numberOfContracts = sales.GroupBy(x => x.Shop.Id).Select(x => x.Count());
			_viewModel.CreatedDate.ShouldBe(_settlement.CreatedDate.ToString("yyyy-MM-dd HH:mm"));
			_viewModel.Id.ShouldBe(_settlement.Id);
			_viewModel.Period.ShouldBe("1045");
			_viewModel.SumAmountExcludingVAT.ShouldBe(sales.Sum(y => y.TotalAmountExcludingVAT).ToString("C2"));
			_viewModel.SumAmountIncludingVAT.ShouldBe(sales.Sum(y => y.TotalAmountIncludingVAT).ToString("C2"));
			_viewModel.SettlementItems.Count().ShouldBe(2);
			_viewModel.SettlementItems.Count().ShouldBe(2);
			_viewModel.SettlementItems.For((index,settlementItem) =>
			{
				settlementItem.BankGiroNumber.ShouldBe(shops.ElementAt(index).BankGiroNumber);
				settlementItem.NumberOfContractSalesInSettlement.ShouldBe(numberOfContracts.ElementAt(index));
				settlementItem.ShopDescription.ShouldBe(String.Format("{0} - {1}", shops.ElementAt(index).Number, shops.ElementAt(index).Name));
				settlementItem.SumAmountExcludingVAT.ShouldBe(amountsExcludingVAT.ElementAt(index).ToString("C2"));
				settlementItem.SumAmountIncludingVAT.ShouldBe(amountsIncludingVAT.ElementAt(index).ToString("C2"));

			});
		}
	}

	[TestFixture]
	[Category("ContractSalesControllerTests")]
	public class When_loading_settlements_list
	{
		private readonly SettlementListView _viewModel;
		private readonly IEnumerable<ShopSettlement> _settlements;
		private readonly IEnumerable<ContractSale> _expectedContractSalesReadyForInvocing;
		private readonly int _readyForSettlementStatus;
		private readonly Mock<ISettlementRepository> _mockedSettlementRepository;
		private readonly Mock<IAdminSettingsService> _mockedSettingsService;
		private readonly Mock<IContractSaleRepository> _mockedContractSaleRepository;

		public When_loading_settlements_list()
		{
			// Arrange
			_expectedContractSalesReadyForInvocing = ContractSaleFactory.GetList(23);
			_readyForSettlementStatus = 6;
			_settlements = SettlementFactory.GetList();
			_mockedSettlementRepository = new Mock<ISettlementRepository>();
			_mockedContractSaleRepository = new Mock<IContractSaleRepository>();
			_mockedSettingsService = new Mock<IAdminSettingsService>();


			_mockedSettlementRepository.Setup(x => x.GetAll()).Returns(_settlements);
			_mockedContractSaleRepository.Setup(x => x.FindBy(It.IsAny<AllContractSalesMatchingCriteria>())).Returns(_expectedContractSalesReadyForInvocing);
			_mockedSettingsService.Setup(x => x.GetContractSalesReadyForSettlementStatus()).Returns(_readyForSettlementStatus);
			

			var viewService = new ContractSalesViewService(_mockedSettlementRepository.Object, _mockedContractSaleRepository.Object, _mockedSettingsService.Object);
			var controller = new ContractSalesController(viewService);

			var view = (ViewResult) controller.Settlements();
			_viewModel = (SettlementListView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			_viewModel.NumberOfContractSalesReadyForInvocing.ShouldBe(_expectedContractSalesReadyForInvocing.Count());
			_viewModel.Settlements.Count().ShouldBe(_settlements.Count());
			_viewModel.Settlements.For((index, settlement) =>
			{
				settlement.CreatedDate.ShouldBe(_settlements.ElementAt(index).CreatedDate.ToString("yyyy-MM-dd"));
				settlement.Id.ShouldBe(_settlements.ElementAt(index).Id);
				settlement.NumberOfContractSalesInSettlement.ShouldBe(_settlements.ElementAt(index).ContractSales.Count());
			});
		}

		[Test]
		public void Repositories_and_Services_were_called_with_expected_parameters()
		{
			_mockedSettlementRepository.Verify(x => x.GetAll(), Times.Once());
			_mockedContractSaleRepository.Verify(x => x.FindBy(It.Is<AllContractSalesMatchingCriteria>(criteria => criteria.ContractSaleStatus.Equals(_readyForSettlementStatus))), Times.Once());
			_mockedSettingsService.Verify(x => x.GetContractSalesReadyForSettlementStatus(), Times.Once());
		}
	}
}