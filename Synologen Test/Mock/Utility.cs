using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Unit.Test.Mock{
	public static class Utility {

		#region ShopData/IShop/Shop
		public static Shop GetMockShopData() {
			return GetMockIShop();
		}
		public static Shop GetMockIShop() {
			return GetMockShopRow();
		}
		public static Shop GetMockShopRow() {
			return new Shop {
			                	Active = true,
			                	Address = String.Empty,
			                	Address2 = "Gustav Adolfstorg 51",
			                	CategoryId = 1,
			                	City = "Malmö",
			                	ContactFirstName = String.Empty,
			                	ContactLastName = String.Empty,
			                	Description = String.Empty,
			                	Email = "cityoptik@synologen.se",
			                	Fax = "0515-159 29",
			                	MapUrl = String.Empty,
			                	Name = "City Optik",
			                	Number = "4055",
			                	Phone = "0515-120 20",
			                	Phone2 = String.Empty,
			                	ShopId = 25,
			                	Url = String.Empty,
			                	Zip = "211 39"
			                };
		}
		#endregion

		#region OrderItemData/IOrderItem/OrderItem
		private static OrderItem GetMockOrderItem(int orderId) {
			return new OrderItem(GetMockIOrderItem(orderId, orderId + 1));
		}
		private static OrderItem GetMockIOrderItem(int orderId, int orderItemId) {
			return GetMockOrderItemRow(orderId, orderItemId);
		}
		public static OrderItem GetMockOrderItemRow(int orderId,int orderItemId) {
			return new OrderItem {
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

		#region ContractCompanyData/ICompany/Company
		public static Company GetMockCompany() {
			return new Company(GetMockICompany());
		}
		private static Company GetMockICompany() {
			return GetMockCompanyRow();
		}
		public static Company GetMockCompanyRow() {
			return new Company {
			                   	PostBox = "Swedbank",
			                   	StreetName = "Fakturagruppen RST",
			                   	BankCode = "8999",
			                   	City = "Stockholm",
			                   	SPCSCompanyCode = "900",
			                   	ContractId = 1,
			                   	Id = 4,
			                   	Name = "Swedbank",
			                   	Zip = "105 34",
			                   	EDIRecipientId = "00075020177753TEST",
			                   	PaymentDuePeriod = 30,
			                   	InvoiceFreeTextFormat = String.Concat(
			                   		"Beställare Namn, {CustomerName}", "\r\n",
			                   		"Beställare Personnummer, {CustomerPersonalIdNumber}", "\r\n",
			                   		"Beställare Enhet, {CompanyUnit}"
			                   		)
			                   };
		}
		#endregion

		#region OrderData/IOrder/Order
		public static Order GetMockOrderData(int orderId) {
			return new Order(GetMockIOrder(orderId)) {
			                                         	ContractCompany = GetMockCompany(),
			                                         	OrderItems = new List<OrderItem> {GetMockOrderItem(orderId)},
			                                         	SellingShop = GetMockShopData(),
			                                         };
		}
		public static Order GetMockOrderRow(int orderId) {
			return new Order {
			                 	CompanyId = 1,
			                 	CompanyUnit = "Bygg & Inredning",
			                 	CreatedDate = new DateTime(2009, 09, 23),
			                 	CustomerFirstName = "Anders",
			                 	CustomerLastName = "Johansson",
			                 	//Email = "carl.berg@spinit.se",
			                 	Id = orderId,
			                 	InvoiceNumber = 199,
			                 	MarkedAsPayedByShop = false,
			                 	PersonalIdNumber = "197001015374",
			                 	Phone = "031123456",
			                 	RstText = "45403",
			                 	SalesPersonMemberId = 1,
			                 	SalesPersonShopId = 1,
			                 	StatusId = 1,
			                 	UpdateDate = DateTime.MinValue
			                 };
		}
		private static Order GetMockIOrder(int orderId) {
			return GetMockOrderRow(orderId);
		}
		#endregion

		public static IInvoiceStatus GetMockPaymentInfo(long invoiceNumber) {
			return new PaymentInfo {
			                       	InvoiceCanceled = false,
			                       	InvoiceNumber = invoiceNumber,
			                       	InvoicePaymentCanceled = false,
			                       	InvoicePaymentDate = DateTime.Now,
			                       	Other = new object(),
			                       	Status = String.Empty
			                       };
		}

		public static List<OrderItem> GetMockOrderItems(int id) {
			var list = new List<OrderItem> {
			                               	new OrderItem {
			                               	              	ArticleDisplayName = "Synundersökning (momsbefriad)",
			                               	              	ArticleDisplayNumber = "1000",
			                               	              	NumberOfItems = 1,
			                               	              	SinglePrice = 200,
			                               	              	OrderId = id,
			                               	              	NoVAT = true
			                               	              },
			                               	new OrderItem {
			                               	              	ArticleDisplayName = "Företagsbåge",
			                               	              	ArticleDisplayNumber = "2000",
			                               	              	NumberOfItems = 1,
			                               	              	SinglePrice = 400,
			                               	              	OrderId = id
			                               	              },
			                               	new OrderItem {
			                               	              	ArticleDisplayName = "G närprogressiva plast (standard)",
			                               	              	ArticleDisplayNumber = "3110",
			                               	              	NumberOfItems = 2,
			                               	              	SinglePrice = 220,
			                               	              	OrderId = id
			                               	              },
			                               	new OrderItem {
			                               	              	ArticleDisplayName = "Superantireflex plast inkl. hårdyta",
			                               	              	ArticleDisplayNumber = "3412",
			                               	              	NumberOfItems = 2,
			                               	              	SinglePrice = 200,
			                               	              	OrderId = id
			                               	              }
			                               };
			return list;
		}
	}
}