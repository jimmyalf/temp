using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public static class Convert {

		public static Invoice ToEDIInvoice(EDIConversionSettings EDISettings, OrderRow order, List<IOrderItem> orderItems, ICompany company, IShop shop) {
			var invoiceValueIncludingVAT = System.Convert.ToSingle(order.InvoiceSumIncludingVAT);
			var invoiceValueExcludingVAT = System.Convert.ToSingle(order.InvoiceSumExcludingVAT);
			var invoice = new Invoice(EDISettings.VATAmount, EDISettings.NumberOfDecimalsUsedAtRounding, invoiceValueIncludingVAT, invoiceValueExcludingVAT) {
				Articles = ToEDIArticles(orderItems, order),
             	Buyer = GetBuyerInformation(EDISettings.RecipientId,company),
             	BuyerOrderNumber = String.Empty,
             	BuyerRSTNumber = order.RstText,
             	DocumentNumber = order.InvoiceNumber.ToString(),
				InterchangeHeader = new InterchangeHeader { RecipientId = EDISettings.RecipientId, SenderId = EDISettings.SenderId },
             	InvoiceCreatedDate = order.CreatedDate,
				InvoiceSetting = new InvoiceSetting { InvoiceCurrency = EDISettings.InvoiceCurrencyCode, InvoiceExpiryDate = EDISettings.InvoiceExpieryDate },
             	VendorOrderNumber = order.Id.ToString(),
				Supplier = GetSupplierInformation(EDISettings.SenderId, EDISettings.BankGiro,EDISettings.Postgiro, shop)
             };
			return invoice;
		}

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

		private static Buyer GetBuyerInformation(string buyerId, ICompany company) {
			var buyer = new Buyer {                	
				BuyerIdentity = buyerId,
                InvoiceIdentity = company.BankCode,
				DeliveryAddress = new Address {
      				Address1 = company.Address1, 
					Address2 = company.Address2, 
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

		public static InvoiceRow ToEDIFreeTextInformationRow(List<string> listOfFreeTextRows) {
			var eDIitem = new InvoiceRow {FreeTextRows = listOfFreeTextRows, UseInvoiceRowAsFreeTextRow = true, RowNumber = 1};
			return eDIitem;
		}

		public static List<string> GetOrderBuyerInformation(OrderRow order) {
			var listOfStrings = new List<string>();
			listOfStrings.Add(String.Format("Beställare Namn, {0} {1}", order.CustomerFirstName, order.CustomerLastName));
            listOfStrings.Add(String.Format("Beställare Personnummer, {0}", order.PersonalIdNumber));
            listOfStrings.Add(String.Format("Beställare Enhet, {0}", order.CompanyUnit));
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

		public static List<InvoiceRow> ToEDIArticles(List<IOrderItem> orderItems, OrderRow order) {
			var EDIArticles = new List<InvoiceRow>();

			//Add one freetextRow
			var listOfBuyerData = GetOrderBuyerInformation(order);
			var freeTextBuyerInvoiceRow = ToEDIFreeTextInformationRow(listOfBuyerData);
			EDIArticles.Add(freeTextBuyerInvoiceRow);

			var articleCounter = 2;
			foreach (var item in orderItems) {
				EDIArticles.Add(ToEDIArticle(item, articleCounter));
				articleCounter++;
			}
			return EDIArticles;
		}
	}
}