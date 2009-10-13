using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public partial class Convert {
		private static Supplier GetSupplierInformation(string supplierId, string bankGiro, string postGiro, IShop shop) {
			var supplier = new Supplier {
				BankGiroNumber = bankGiro,
				PostGiroNumber = postGiro,
				Contact = new Contact {
					ContactInfo = shop.Name,
					Email = shop.Email,
					Fax = shop.Fax,
					Telephone = shop.Phone
				},
				SupplierIdentity = supplierId
			};
			return supplier;
		}

		private static Buyer GetBuyerInformation(string buyerId, CompanyRow company) {
			var buyer = new Buyer {                	
				BuyerIdentity = buyerId,
                InvoiceIdentity = company.BankCode,
				DeliveryAddress = new Address {
      				Address1 = company.PostBox, 
					Address2 = company.StreetName, 
					City = company.City, 
					Zip = company.Zip
				  }
			};
			return buyer;
		}

		public static InvoiceRow ToEDIArticle(IOrderItem orderItem, int invoiceRowNumber) {
			var EDIitem = new InvoiceRow {
              	ArticleName = orderItem.ArticleDisplayName,
              	ArticleNumber = orderItem.ArticleDisplayNumber,
              	Quantity = orderItem.NumberOfItems,
              	RowNumber = invoiceRowNumber,
              	SinglePriceExcludingVAT = orderItem.SinglePrice,
              	ArticleDescription = orderItem.Notes,
				NoVAT = orderItem.NoVAT
			  };

			return EDIitem;
		}

		public static InvoiceRow ToEDIFreeTextInformationRow(IList<string> listOfFreeTextRows) {
			var eDIitem = new InvoiceRow {FreeTextRows = new List<string>(listOfFreeTextRows), UseInvoiceRowAsFreeTextRow = true, RowNumber = 1};
			return eDIitem;
		}

		public static List<string> GetOrderBuyerInformation(OrderRow order) {
			var listOfStrings = new List<string>();
			if (!String.IsNullOrEmpty(order.CustomerFirstName) && !String.IsNullOrEmpty(order.CustomerLastName)){
				listOfStrings.Add(String.Format("Beställare Namn, {0} {1}", order.CustomerFirstName, order.CustomerLastName));
			}
			if (!String.IsNullOrEmpty(order.PersonalIdNumber)){
				listOfStrings.Add(String.Format("Beställare Personnummer, {0}", order.PersonalIdNumber));
			}
			if(!String.IsNullOrEmpty(order.CompanyUnit)){
				listOfStrings.Add(String.Format("Beställare Enhet, {0}", order.CompanyUnit));
			}
			return listOfStrings;
		}

		//public static List<InvoiceRow> ToEDIArticles(List<IOrderItem> orderItems) {
		//    var EDIArticles = new List<InvoiceRow>();
		//    var articleCounter = 1;
		//    foreach(var item in orderItems) {
		//        EDIArticles.Add(ToEDIArticle(item, articleCounter));
		//        articleCounter++;
		//    }
		//    return EDIArticles;
		//}

		public static List<InvoiceRow> ToEDIArticles(List<IOrderItem> orderItems, OrderRow order, CompanyRow company) {
			var EDIArticles = new List<InvoiceRow>();
			var articleCounter = 1;
			//Add one freetextRow if any information is available
			//var freeTextRows = GetOrderBuyerInformation(order);
			var freeTextRows = GetFreeTextRows(company, order);
			if(freeTextRows!=null && freeTextRows.Count>0){
				var freeTextBuyerInvoiceRow = ToEDIFreeTextInformationRow(freeTextRows);
				EDIArticles.Add(freeTextBuyerInvoiceRow);
				articleCounter = 2;
			}
			foreach (var item in orderItems) {
				var ediArticle = ToEDIArticle(item, articleCounter);
				EDIArticles.Add(ediArticle);
				articleCounter++;
			}
			return EDIArticles;
		}

		public static IList<string> GetFreeTextRows(CompanyRow company, OrderRow order) {
			if (String.IsNullOrEmpty(company.InvoiceFreeTextFormat)) return new List<string>();
			var parsedInvoiceFreeText = ParseInvoiceFreeTeext(company.InvoiceFreeTextFormat, order);
			return parsedInvoiceFreeText.Trim().Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
		}

		public static string ParseInvoiceFreeTeext(string invoiceFreeTextFormat, OrderRow order) {
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerName}", order.CustomerCombinedName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerPersonalIdNumber}", order.PersonalIdNumber ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CompanyUnit}", order.CompanyUnit ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerPersonalBirthDateString}", order.PersonalBirthDateString ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerFirstName}", order.CustomerFirstName ?? String.Empty);
			invoiceFreeTextFormat = invoiceFreeTextFormat.Replace("{CustomerLastName}", order.CustomerLastName ?? String.Empty);
			return invoiceFreeTextFormat;
		}
	}
}