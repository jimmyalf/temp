using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public static class ContractSalesOrderFactory 
	{
		public static Order GetOrder(int companyId, int salesPersonMemberId, int shopId, int statusId)
		{
			return new Order
			{
				CompanyId = companyId,
				CreatedDate = new DateTime(2011,01,01),
				CompanyUnit = "Unit",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				CustomerOrderNumber = null,
				Email = "a.b@test.se",
				InvoiceNumber = 1234,
				InvoiceSumExcludingVAT = 256,
				InvoiceSumIncludingVAT = 302,
				MarkedAsPayedByShop = false,
				OrderItems = null,
				PersonalIdNumber = "197001015566",
				Phone = "031-0123456",
				RstText = "RST",
				SalesPersonMemberId = salesPersonMemberId,
				SalesPersonShopId = shopId,
				StatusId = statusId,
			};
		}
	}
}