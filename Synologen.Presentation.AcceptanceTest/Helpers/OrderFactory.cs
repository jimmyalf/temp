using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class OrderFactory 
	{
		public static Order GetOrder(int companyId, int salesPersonMemberId, int salesPersonShopId)
		{
			return new Order
			{
				PersonalIdNumber = "198512242101",
				CompanyUnit = "1030",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				StatusId = 5 /* Invoiced Status */,
				Phone = "031 - 12 34 56", 
				Email = "adam.bertil@testbolaget.se",
				SalesPersonMemberId = salesPersonMemberId,
				SalesPersonShopId = salesPersonShopId,
				RstText = "1234",
				CustomerOrderNumber = "ABC123",
				CompanyId = companyId,
				InvoiceNumber = 12345
			};
		}
	}
}