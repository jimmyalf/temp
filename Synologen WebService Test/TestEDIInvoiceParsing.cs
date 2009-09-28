using System;
using System.Diagnostics;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test {
	[TestFixture]
	public class TestEDIInvoiceParsing {
		private EDIConversionSettings ediSettings = new EDIConversionSettings();
		private const string NewLine = "\r\n";

		[TestFixtureSetUp]
		public void Setup() {
			ediSettings = new EDIConversionSettings {
				SenderId = "5562626100",
				BankGiro = "56936677",
				VATAmount = 0.25F,
				InvoiceCurrencyCode = "SEK"
			};
		}

		[Test]
		public void Test_Invoice_Parsing_Output() {
			const int orderId = 265;
			var order = Mock.Utility.GetMockOrderRow(orderId);
			var shop = Mock.Utility.GetMockShopRow();
			var orderItems = Mock.Utility.GetMockOrderItems(orderId);
			var company = Mock.Utility.GetMockCompanyRow();
			var invoice = Utility.General.CreateInvoiceEDI(order, orderItems, company, shop, ediSettings);
			var invoiceText = invoice.Parse();
			Debug.WriteLine(invoiceText);
			var expectedString = String.Concat(
				"UNA:+,? '", NewLine,
				String.Format("UNB+UNOA:1+5562626100:14+00075020177753TEST:30+{0}+{1}'",invoice.InterchangeHeader.DateOfPreparation.ToString("yyMMdd:HHmm"), invoice.InterchangeHeader.ControlReference), NewLine,
				String.Format("UNH+{0}+INVOIC:D:93A:UN'", invoice.MessageReference), NewLine,
				"BGM+380+199+9'", NewLine,
				"DTM+137:20090923:102'", NewLine,
				"RFF+VN:265'", NewLine,
				"RFF+CR:45403'", NewLine,
				"NAD+SU+5562626100:14'", NewLine,
				"FII+RB+56936677+BK'", NewLine,
				"CTA+AR+:City Optik'", NewLine,
				"COM+0515-120 20:TE'", NewLine,
				"COM+0515-159 29:FX'", NewLine,
				"COM+cityoptik@synologen.se:EM'", NewLine,
				"NAD+BY+00075020177753TEST:30'", NewLine,
				"NAD+IV+8999:30'", NewLine,
				"NAD+CN+++Swedbank+Fakturagruppen RST+Stockholm++105 34'", NewLine,
				"CUX+2:SEK:4'", NewLine,
				"PAT+3'", NewLine,
				String.Format("DTM+13:{0}:102'",invoice.InterchangeHeader.DateOfPreparation.AddDays(company.PaymentDuePeriod).ToString("yyyyMMdd")), NewLine,
				"LIN+1000'", NewLine,
				"FTX+GEN+++Beställare Namn, Anders Johansson'", NewLine,
				"FTX+GEN+++Beställare Personnummer, 197001015374'", NewLine,
				"FTX+GEN+++Beställare Enhet, Bygg & Inredning'", NewLine,
				"LIN+2000'", NewLine,
				"PIA+5+1000:SA'", NewLine,
				"IMD+F++:::Synundersökning (momsbefriad)'", NewLine,
				"QTY+47:1:PCE'", NewLine,
				"MOA+203:200'", NewLine,
				"PRI+AAA:200'", NewLine,
				"LIN+3000'", NewLine,
				"PIA+5+2000:SA'", NewLine,
				"IMD+F++:::Företagsbåge'", NewLine,
				"QTY+47:1:PCE'", NewLine,
				"MOA+203:400'", NewLine,
				"PRI+AAA:400'", NewLine,
				"LIN+4000'", NewLine,
				"PIA+5+3110:SA'", NewLine,
				"IMD+F++:::G närprogressiva plast (standard)'", NewLine,
				"QTY+47:2:PCE'", NewLine,
				"MOA+203:440'", NewLine,
				"PRI+AAA:220'", NewLine,
				"LIN+5000'", NewLine,
				"PIA+5+3412:SA'", NewLine,
				"IMD+F++:::Superantireflex plast inkl. hårdyta'", NewLine,
				"QTY+47:2:PCE'", NewLine,
				"MOA+203:400'", NewLine,
				"PRI+AAA:200'", NewLine,
				"UNS+S'", NewLine,
				"CNT+2:5'", NewLine,
				"MOA+9:1750'", NewLine,
				"MOA+176:310'", NewLine,
				String.Format("UNT+50+{0}'",invoice.MessageReference), NewLine,
				String.Format("UNZ+1+{0}'", invoice.InterchangeControlReference));
			Assert.AreEqual(expectedString, invoiceText.ToString());
		}
	}
}