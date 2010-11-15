using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class TransactionFactory
	{
		public static Transaction Get()
		{
			return Get(1, 586.23M);
		}

		public static Transaction Get(int id, decimal amount)
		{
			var mockedTransaction = new Mock<Transaction>();
			mockedTransaction.SetupGet(x => x.Id).Returns(id);
			var returnObject = mockedTransaction.Object;
			returnObject.Amount = amount;
			return returnObject;
		}

		public static IEnumerable<Transaction> GetList()
		{
			return new[]
			{
				Get(1, 285.45M),
				Get(2, 12.86M),
				Get(3, 775),
				Get(4, 256),
				Get(5, 125),
				Get(6, 555.55M),
			};
		}
	}
}