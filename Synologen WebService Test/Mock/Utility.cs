using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Visma.Types;

namespace Spinit.Wpc.Synologen.Test.Mock {
	public static class Utility {

		#region ShopData/IShop/ShopRow
		public static ShopData GetMockShopData() {
			return new ShopData(GetMockIShop());
		}
		public static IShop GetMockIShop() {
			return GetMockShopRow();
		}
		public static ShopRow GetMockShopRow() {
			return new ShopRow {
			                   	Active = true,
			                   	Address = String.Empty,
			                   	Address2 = "Gustav Adolfstorg 51",
			                   	CategoryId = 1,
			                   	City = "Malmö",
			                   	ContactFirstName = String.Empty,
			                   	ContactLastName = String.Empty,
			                   	Description = String.Empty,
			                   	Email = "mansson.gustav@synologen.se",
			                   	Fax = "040-39 68 08",
			                   	MapUrl = String.Empty,
			                   	Name = "Anders Månsson Optik",
			                   	Number = "4055",
			                   	Phone = "040-39 67 30",
			                   	Phone2 = String.Empty,
			                   	ShopId = 25,
			                   	Url = String.Empty,
			                   	Zip = "211 39"
			                   };
		}
		#endregion

		#region OrderItemData/IOrderItem/OrderItemRow
		private static OrderItemData GetMockOrderItem(int orderId) {
			return new OrderItemData(GetMockIOrderItem(orderId, orderId + 1));
		}
		private static IOrderItem GetMockIOrderItem(int orderId, int orderItemId) {
			return GetMockOrderItemRow(orderId, orderItemId);
		}
		public static OrderItemRow GetMockOrderItemRow(int orderId,int orderItemId) {
			return new OrderItemRow {
			                        	ArticleDisplayName = "Synundersökning",
			                        	ArticleDisplayNumber = "3210",
			                        	ArticleId = 1,
			                        	DisplayTotalPrice = 300,
			                        	Id = orderItemId,
			                        	Notes = String.Empty,
			                        	NoVAT = true,
			                        	NumberOfItems = 2,
			                        	OrderId = orderId,
			                        	SinglePrice = 150
			                        };
		}
		#endregion

		#region ContractCompanyData/ICompany/CompanyRow
		public static ContractCompanyData GetMockCompany() {
			return new ContractCompanyData(GetMockICompany());
		}
		private static ICompany GetMockICompany() {
			return GetMockCompanyRow();
		}
		public static CompanyRow GetMockCompanyRow() {
			return new CompanyRow {
			                      	Address1 = "Swedbank Finans AB",
			                      	Address2 = "Ekonomi",
			                      	BankCode = "8899",
			                      	City = "STOCKHOLM",
			                      	CompanyCode = "901",
			                      	ContractId = 1,
			                      	Id = 1,
			                      	Name = "Swedbank Finans",
			                      	Zip = "105 34"
			                      };
		}
		#endregion

		#region OrderData/IOrder/OrderRow
		public static OrderData GetMockOrderData(int orderId) {
			return new OrderData(GetMockIOrder(orderId)) {
			                                             	ContractCompany = GetMockCompany(),
			                                             	OrderItems = new List<OrderItemData> {GetMockOrderItem(orderId)},
			                                             	SellingShop = GetMockShopData(),
			                                             };
		}
		public static OrderRow GetMockOrderRow(int orderId) {
			return new OrderRow {
			                    	CompanyId = 1,
			                    	CompanyUnit = "Test-enhet",
			                    	CreatedDate = DateTime.Now,
			                    	CustomerFirstName = "Adam",
			                    	CustomerLastName = "Bertil",
			                    	Email = "carl.berg@spinit.se",
			                    	Id = orderId,
			                    	InvoiceNumber = 1,
			                    	MarkedAsPayedByShop = false,
			                    	PersonalIdNumber = "197001015374",
			                    	Phone = "031123456",
			                    	RSTId = 0,
			                    	RstText = "01234",
			                    	SalesPersonMemberId = 1,
			                    	SalesPersonShopId = 1,
			                    	StatusId = 1,
			                    	UpdateDate = DateTime.MinValue
			                    };
		}
		private static IOrder GetMockIOrder(int orderId) {
			return GetMockOrderRow(orderId);
		}
		#endregion

		public static PaymentInfo GetMockPaymentInfo(long invoiceNumber) {
			return new PaymentInfo {
			                       	InvoiceCanceled = false,
			                       	InvoiceNumber = invoiceNumber,
			                       	InvoicePaymentCanceled = false,
			                       	InvoicePaymentDate = DateTime.Now,
			                       	Other = new object(),
			                       	Status = String.Empty
			                       };
		}
		
	}
}