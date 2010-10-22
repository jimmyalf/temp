using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class CustomerFactory
	{
		public static IEnumerable<Customer> GetList()
		{
			return new[]
			{
				GetCustomer(1, "Eva", "Bergström", "8407143778", 1), 
				GetCustomer(2, "Lasse", "Larsson", "5406011857", 1),
				GetCustomer(3, "Lotta", "Olsson", "4906103207", 1),
			};
		}
		public static Customer Get(int id)
		{
			return GetCustomer(id, "Eva", "Bergström", "8407143778", 1);
		}
		public static Customer Get(int id, int shopId)
		{
			return GetCustomer(id, "Eva", "Bergström", "8407143778", shopId);
		}
		public static Customer Get(int id, Mock<Shop> mockedShop)
		{
			return GetCustomer(id, "Eva", "Bergström", "8407143778", mockedShop);
		}

		private static Customer GetCustomer(int id, string firstName, string lastName, string personalIdNumber, int shopId)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);
			var mockedShop = new Mock<Shop>();
			mockedShop.SetupGet(x => x.Id).Returns(shopId);
			mockedShop.SetupGet(x => x.Name).Returns("Butik");
			mockedCustomer.SetupGet(x => x.Shop).Returns(mockedShop.Object);
			return mockedCustomer.Object;
		}
		private static Customer GetCustomer(int id, string firstName, string lastName, string personalIdNumber, Mock<Shop> mockedShop)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);
			mockedCustomer.SetupGet(x => x.Shop).Returns(mockedShop.Object);
			return mockedCustomer.Object;
		}
	}
}
