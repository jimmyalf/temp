using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.Factories
{
	public static class SubscriptionFactory
	{
		public static OldSubscription GetOld(int id, Customer customer)
		{
			var mockedSubscription = new Mock<OldSubscription>();
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			return mockedSubscription.Object;
		}
		public static NewSubscription GetNew(int id, Customer customer)
		{
			var mockedSubscription = new Mock<NewSubscription>();
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			return mockedSubscription.Object;
		}
	}
}
