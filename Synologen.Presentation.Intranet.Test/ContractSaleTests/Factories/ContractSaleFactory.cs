using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class ContractSaleFactory
	{
		public static ContractSale Get(int id, ContractCompany contractCompany)
		{
			var mockedContractSale = new Mock<ContractSale>();
			mockedContractSale.SetupGet(x => x.Id).Returns(id);
			mockedContractSale.SetupGet(x => x.ContractCompany).Returns(contractCompany);
			return mockedContractSale.Object;
		}
	}
}