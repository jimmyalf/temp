using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	public static class OrderFactory
	{
		public static PickCustomerEventArgs GetOrderCustomerForm(int? customerId = null)
		{
			return new PickCustomerEventArgs
			{
				AddressLineOne = "Box 123",
				AddressLineTwo = "Datavägen 2",
				City = "Askim",
				Email = "adam.bertil@testbolaget.se",
				FirstName = "Adam",
				LastName = "Bertil",
				MobilePhone = "0701-123456",
				Notes = "Anteckningar ABC",
				PersonalIdNumber = "197001013239",
				Phone = "0317483000",
				PostalCode = "43632",
				CustomerId = customerId
			};
		}

		public static FetchCustomerDataByPersonalIdEventArgs GetPersonalIdForm()
		{
			return new FetchCustomerDataByPersonalIdEventArgs {PersonalIdNumber = "197001013239"};
		}

		public static SearchCustomerEventArgs GetSearchCustomerEventArgs(string personalIdNumber = "197001013239")
		{
			return new SearchCustomerEventArgs {PersonalIdNumber = personalIdNumber};
		}

		public static OrderCustomer GetCustomer(string personalIdNumber = "197001013239")
		{
			return new OrderCustomer
			{
				AddressLineOne = "Box 1234",
				AddressLineTwo = "Datavägen 23",
				City = "Mölndal",
				Email = "adam.b@testbolaget.se",
				FirstName = "Bertil",
				LastName = "Adamsson",
				MobilePhone = "0701-987654",
				Notes = "Anteckningar ABC DEF",
				PersonalIdNumber = personalIdNumber,
				Phone = "031123456",
				PostalCode = "41300",
			};
		}
	}
}