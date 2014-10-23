using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class ContractFactory
	{
		public static Contract GetContract(int contractId)
		{
			return new Contract
			{
				Id = contractId,
				Name = "Bolag ABC"
			};
		}

		public static Contract GetContract()
		{
			return GetContract(9321);
		}
	}
}