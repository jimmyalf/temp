using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.Factories
{
	public static class ContractCompanyFactory 
	{
		public static ContractCompany Get() 
		{
			var mockedContractCompany = new Mock<ContractCompany>();
			mockedContractCompany.SetupGet(x => x.Id).Returns(123);
			mockedContractCompany.SetupGet(x => x.ContractId).Returns(12);
			mockedContractCompany.SetupGet(x => x.Name).Returns("Test-contract-company");
			return mockedContractCompany.Object;
		}
	}
}