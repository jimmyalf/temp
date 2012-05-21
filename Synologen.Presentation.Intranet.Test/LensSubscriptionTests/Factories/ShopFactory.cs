using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.LensSubscriptionTests.Factories
{
	public static class ShopFactory
	{
		public static Shop Get(int id)
		{
			var mockedShop = new Mock<Shop>();
			mockedShop.SetupGet(x => x.Id).Returns(id);
			mockedShop.SetupGet(x => x.Name).Returns("Butik");
			return mockedShop.Object;
		}
	}
}