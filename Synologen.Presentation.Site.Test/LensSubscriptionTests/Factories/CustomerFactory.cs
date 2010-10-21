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
				GetCustomer(1, "Eva", "Bergström", "8407143778"), 
				GetCustomer(2, "Lasse", "Larsson", "5406011857"),
				GetCustomer(3, "Lotta", "Olsson", "4906103207"),
			};
		}
		public static Customer Get()
		{
			return GetCustomer(1, "Eva", "Bergström", "8407143778");
		}

		private static Customer GetCustomer(int id, string firstName, string lastName, string personalIdNumber)
		{
			var mockedCustomer = new Mock<Customer>();
			mockedCustomer.SetupGet(x => x.Id).Returns(id);
			mockedCustomer.SetupGet(x => x.FirstName).Returns(firstName);
			mockedCustomer.SetupGet(x => x.LastName).Returns(lastName);
			mockedCustomer.SetupGet(x => x.PersonalIdNumber).Returns(personalIdNumber);
			return mockedCustomer.Object;
		}
	}
}
