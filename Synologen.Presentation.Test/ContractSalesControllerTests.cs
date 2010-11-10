using System;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
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
			var mockedRepository = new Mock<ISettlementRepository>();
			mockedRepository.Setup(x => x.Get(It.Is<int>(id => id.Equals(_settlementId)))).Returns(_settlement);


			var viewService = new ContractSalesViewService(mockedRepository.Object);
			
			var controller = new ContractSalesController(viewService);

			var view = (ViewResult) controller.ViewSettlement(_settlementId);
			_viewModel = (SettlementView) view.ViewData.Model;
		}

		[Test]
		public void ViewModel_should_have_expected_values()
		{
			var amountsIncludingVAT = _settlement.ContractSales.GroupBy(x => x.Shop.Id).Select(x => x.Sum(y => y.TotalAmountIncludingVAT));
			var amountsExcludingVAT = _settlement.ContractSales.GroupBy(x => x.Shop.Id).Select(x => x.Sum(y => y.TotalAmountExcludingVAT));
			var shops = _settlement.ContractSales.GroupBy(x => x.Shop.Id, x => x.Shop).Select(x => x.FirstOrDefault());
			var numberOfContracts = _settlement.ContractSales.GroupBy(x => x.Shop.Id).Select(x => x.Count());
			_viewModel.CreatedDate.ShouldBe(_settlement.CreatedDate.ToString("yyyy-MM-dd"));
			_viewModel.Id.ShouldBe(_settlement.Id);
			_viewModel.Period.ShouldBe("1045");
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
}