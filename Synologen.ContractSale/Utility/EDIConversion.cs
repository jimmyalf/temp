using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;

namespace Spinit.Wpc.Synologen.Invoicing
{
	public partial class Convert 
	{
		private static Supplier GetSupplierInformation(string supplierId, string bankGiro, string postGiro, IShop shop) 
		{
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

		private static Buyer GetBuyerInformation(string buyerId, ICompany company) 
		{
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

		public static InvoiceRow ToEDIArticle(IOrderItem orderItem)
		{
			return new InvoiceRow {
				ArticleName = orderItem.ArticleDisplayName,
				ArticleNumber = orderItem.ArticleDisplayNumber,
				Quantity = orderItem.NumberOfItems,
				//RowNumber = invoiceRowNumber,
				SinglePriceExcludingVAT = orderItem.SinglePrice,
				ArticleDescription = orderItem.Notes,
				NoVAT = orderItem.NoVAT
			};
		}

		public static InvoiceRow ToEDIFreeTextInformationRow(IList<string> listOfFreeTextRows)
		{
			var eDIitem = new InvoiceRow {FreeTextRows = new List<string>(listOfFreeTextRows), UseInvoiceRowAsFreeTextRow = true, RowNumber = 1};
			return eDIitem;
		}

		public static List<InvoiceRow> ToEDIArticles(IList<OrderItem> orderItems, IOrder order)
		{
			var invoiceRowCollection = new EDIInvoiceRowCollection();
			var freeTextInvoiceRow = GetFreeTextInvoiceRow(order);
			if(freeTextInvoiceRow != null) invoiceRowCollection.Add(freeTextInvoiceRow);
			var ediArticles = orderItems.Select(item => ToEDIArticle(item));
			foreach (var ediArticle in ediArticles) 
			{
				invoiceRowCollection.Add(ediArticle);
			}
			return invoiceRowCollection.Rows;
		}

		private static InvoiceRow GetFreeTextInvoiceRow(IOrder order)
		{
			if(order == null) return null;
			var parsedInvoiceFreeText = order.ParseFreeText();
			if(parsedInvoiceFreeText == null) return null;
			var freeTextRows = parsedInvoiceFreeText.Trim().Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
			return freeTextRows.Length <= 0 ? null : ToEDIFreeTextInformationRow(freeTextRows);
		}


		internal class EDIInvoiceRowCollection
		{
			private readonly List<InvoiceRow> _invoiceRows;
			private int invoiceRowNumber = 1;

			public EDIInvoiceRowCollection()
			{
				_invoiceRows = new List<InvoiceRow>();
			}
			public void Add(InvoiceRow row)
			{
				row.RowNumber = invoiceRowNumber;
				_invoiceRows.Add(row);
				invoiceRowNumber++;
			}
			public List<InvoiceRow> Rows{get { return _invoiceRows; }}

		}
	}
}