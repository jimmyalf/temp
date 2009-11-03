using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
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

		private static Buyer GetBuyerInformation(string buyerId, ICompany company) {
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

		public static List<InvoiceRow> ToEDIArticles(IList<OrderItem> orderItems, IOrder order, ICompany company) {
			var EDIArticles = new List<InvoiceRow>();
			var articleCounter = 1;
			var freeTextRows = CommonConversion.GetFreeTextRows(company, order);
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


	}
}