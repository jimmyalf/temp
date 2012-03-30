using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class SubscriptionFactory
	{
		public static OldSubscription Get(int id, Customer customer)
		{
			var mockedSubscription = new Mock<OldSubscription>();
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			return mockedSubscription.Object;
		}
		public static OldSubscription Get(int id, int customerId, int shopId)
		{
			return Get(id, CustomerFactory.Get(customerId, shopId));
		}
	}
}