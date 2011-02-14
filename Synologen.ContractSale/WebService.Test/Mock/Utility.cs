using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Test.Mock {
	public static class Utility {

		#region ShopData/IShop/Shop
		public static Shop GetMockShopData() {
			return GetMockShop();
		}
		public static IShop GetMockIShop() {
			return GetMockShop();
		}
		public static Shop GetMockShop() {
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
		private static IOrderItem GetMockIOrderItem(int orderId, int orderItemId) {
			return GetMockOrderItem(orderId, orderItemId);
		}
		public static OrderItem GetMockOrderItem(int orderId,int orderItemId) {
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
		//public static Company GetMockCompany() {
		//    return new Company(GetMockICompany());
		//}
		//private static ICompany GetMockICompany() {
		//    return GetMockCompany();
		//}
		public static Company GetMockCompany() {
			return new Company {
				PostBox = "Swedbank",
				StreetName = "Fakturagruppen RST",
				BankCode = "8999",
				City = "Stockholm",
				//CompanyCode = "900",
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
		public static Order GetMockOrder(int orderId) {
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
			                    	//RSTId = 0,
									RstText = "45403",
			                    	SalesPersonMemberId = 1,
			                    	SalesPersonShopId = 1,
			                    	StatusId = 1,
			                    	UpdateDate = DateTime.MinValue
			                    };
		}
		private static IOrder GetMockIOrder(int orderId) {
			return GetMockOrder(orderId);
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

		public static List<IOrderItem> GetMockOrderItems(int id) {
			var list = new List<IOrderItem> {
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