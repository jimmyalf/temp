using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Shop = Spinit.Wpc.Synologen.Business.Domain.Entities.Shop;

namespace Spinit.Wpc.Synologen.Test.EDI 
{
	[TestFixture]
	public class TestParsing 
	{
		private EDIConversionSettings _ediSettings = new EDIConversionSettings();
		private Order _order;
		private Company _company;
		private Shop _shop;
		private IList<OrderItem> _orderItems;
		private int _numberOfMessageSegmentsToExcludeInCount;
		private const string NewLine = "\r\n";

		[TestFixtureSetUp]
		public void Setup() 
		{
			_ediSettings = new EDIConversionSettings 
			{
				SenderEdiAddress = new EdiAddress("5562626100","14"),
				BankGiro = "56936677",
				VATAmount = 0.25F,
				InvoiceCurrencyCode = "SEK",
			};
			_company = Factory.Factory.GetCompany();
			_shop = Factory.Factory.GetShop();
			_orderItems = Factory.Factory.GetOrderItems().ToList();
			_order = Factory.Factory.GetOrder(_company, _shop, _orderItems);
			_numberOfMessageSegmentsToExcludeInCount = 3;
		}

		[Test]
		public void Test_Invoice_Parsing_Output() {
			var invoice = General.CreateInvoiceEDI(_order, _ediSettings);
			var invoiceText = invoice.Parse();
			var rowNumber = 1;
			var expectedString = String.Concat(
				"UNA:+,? '", NewLine,
				String.Format("UNB+UNOA:1+5562626100:14+00075020177753TEST:30+{0}+{1}'",invoice.InterchangeHeader.DateOfPreparation.ToString("yyMMdd:HHmm"), invoice.InterchangeHeader.ControlReference), NewLine,
				String.Format("UNH+{0}+INVOIC:D:93A:UN'", invoice.MessageReference), NewLine,
				"BGM+380+{InvoiceNumber}+9'".ReplaceWith(new {_order.InvoiceNumber}), NewLine,
				"DTM+137:{InvoiceCreatedDate}:102'".ReplaceWith(new {InvoiceCreatedDate = _order.CreatedDate.ToString("yyyyMMdd")}), NewLine,
				"RFF+VN:{Id}'".ReplaceWith(new {_order.Id }), NewLine,
				"RFF+CR:{RstText}'".ReplaceWith(new {_order.RstText}), NewLine,
				"NAD+SU+5562626100:14'", NewLine,
				"FII+RB+56936677+BK'", NewLine,
				"CTA+AR+:{ShopName}'".ReplaceWith(new {ShopName = _shop.Name}), NewLine,
				"COM+{Phone}:TE'".ReplaceWith(new {_shop.Phone}), NewLine,
				"COM+{Fax}:FX'".ReplaceWith(new {_shop.Fax}), NewLine,
				"COM+{Email}:EM'".ReplaceWith(new{_shop.Email}), NewLine,
				"NAD+BY+00075020177753TEST:30'", NewLine,
				"NAD+IV+{CompanyBankCode}:30'".ReplaceWith(new {CompanyBankCode = _company.BankCode}), NewLine,
				"NAD+CN+++{PostBox}+{StreetName}+{City}++{Zip}'".ReplaceWith(new{_company.City,_company.PostBox,_company.StreetName,_company.Zip,}), NewLine,
				"CUX+2:SEK:4'", NewLine,
				"PAT+3'", NewLine,
				String.Format("DTM+13:{0}:102'",invoice.InterchangeHeader.DateOfPreparation.AddDays(_company.PaymentDuePeriod).ToString("yyyyMMdd")), NewLine,
				"LIN+{RowNumber}000'".ReplaceWith(new{RowNumber = rowNumber++}), NewLine,
				"FTX+GEN+++Namn {CustomerName}'".ReplaceWith(new{CustomerName = _order.CustomerCombinedName}), NewLine,
				"FTX+GEN+++Personnummer {CustomerPersonalIdNumber}'".ReplaceWith(new{ CustomerPersonalIdNumber = _order.PersonalIdNumber}), NewLine,
				"FTX+GEN+++Enhet {CompanyUnit}'".ReplaceWith(new{ _order.CompanyUnit}), NewLine,
				"FTX+GEN+++Födelsedatum {CustomerPersonalBirthDateString}'".ReplaceWith(new{ CustomerPersonalBirthDateString = _order.PersonalBirthDateString}), NewLine,
				"FTX+GEN+++Förnamn {CustomerFirstName}'".ReplaceWith(new{_order.CustomerFirstName}), NewLine,
				"FTX+GEN+++Efternamn {CustomerLastName}'".ReplaceWith(new{_order.CustomerLastName}), NewLine,
				"FTX+GEN+++KundId {BuyerCompanyId}'".ReplaceWith(new{BuyerCompanyId = _order.CompanyId}), NewLine,
				"FTX+GEN+++Rst {RST}'".ReplaceWith(new{RST = _order.RstText}), NewLine,
				"FTX+GEN+++Bankkod {BankCode}'".ReplaceWith(new{_order.ContractCompany.BankCode}), NewLine,
				"FTX+GEN+++Säljande butik {SellingShopName}'".ReplaceWith(new{SellingShopName = _shop.Name}), NewLine,
				"FTX+GEN+++Säljande butiknummer {SellingShopNumber}'".ReplaceWith(new{SellingShopNumber = _shop.Number}), NewLine,
				GetInvoiceRows(_orderItems, ref rowNumber),
				"UNS+S'", NewLine,
				"CNT+2:{NumberOfRows}'".ReplaceWith(new{NumberOfRows = rowNumber -1}), NewLine,
				"MOA+9:{InvoiceAmountExcludingVAT}'".ReplaceWith(new{InvoiceAmountExcludingVAT = _order.InvoiceSumIncludingVAT}), NewLine,
				"MOA+176:{TaxAmount}'".ReplaceWith(new{TaxAmount = Math.Ceiling(_order.InvoiceSumIncludingVAT - _order.InvoiceSumExcludingVAT)}), NewLine,
				"UNT+{NumberOfSegmentsInMessage}+{MessageReference}'".ReplaceWith(new{invoice.MessageReference}), NewLine,
				String.Format("UNZ+1+{0}'", invoice.InterchangeControlReference));

			var numberOfMessageSegments = CountNumberOfMessageSegmentsInEDIFile(expectedString, _numberOfMessageSegmentsToExcludeInCount);
			expectedString = expectedString.ReplaceWith(new {NumberOfSegmentsInMessage = numberOfMessageSegments});
			Assert.AreEqual(expectedString, invoiceText.ToString());
		}

		private static int CountNumberOfMessageSegmentsInEDIFile(string content, int numberOfSegmentsToExclude)
		{
			var numberOfMessageSegments = content.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).Count();
			return numberOfMessageSegments - numberOfSegmentsToExclude;
		}

		private static string GetInvoiceRows(IEnumerable<OrderItem> orderItems, ref int rowNumber)
		{
			var output = new StringBuilder();
			foreach (var orderItem in orderItems)
			{
				output
					.AppendLine("LIN+{RowNumber}000'".ReplaceWith(new { RowNumber = rowNumber++ }))
					.AppendLine("PIA+5+{ArticleNumber}:SA'".ReplaceWith(new { ArticleNumber = orderItem.ArticleDisplayNumber }))
					.AppendLine("IMD+F++:::{ArticleName}'".ReplaceWith(new { ArticleName = orderItem.ArticleDisplayName }))
					.AppendLine("QTY+47:{Quantity}:PCE'".ReplaceWith(new { Quantity = orderItem.NumberOfItems }))
					.AppendLine("MOA+203:{TotalRowPrice}'".ReplaceWith(new { TotalRowPrice = orderItem.DisplayTotalPrice }))
					.AppendLine("PRI+AAA:{SinglePrice}'".ReplaceWith(new { orderItem.SinglePrice }));
			}
			return output.ToString();
		}
	}
}