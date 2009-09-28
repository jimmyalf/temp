using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test {
	[TestFixture]
	public class TestSvefakturaInvoiceParsing {
		private EDIConversionSettings ediSettings = new EDIConversionSettings();
		private const string NewLine = "\r\n";
		private const string XmlFilePath = @"C:\Develop\WPC\Current-CustomerSpecific\Synologen\Synologen WebService Test\Mock\Svefaktura_example.xml";

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
		public void Test_Create_Invoice_Parameter_Checks_For_Null_And_Throws_Exceptions() {
			var emptyOrder = new OrderRow();
			var emptyOrderItemList = new List<IOrderItem>();
			var emptyCompany = new CompanyRow();
			var emptyShop = new ShopRow();
			var emptySettings = new EDIConversionSettings();
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(null,		emptyOrderItemList, emptyCompany,	emptyShop,	emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	null,				emptyCompany,	emptyShop,	emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	emptyOrderItemList, null,			emptyShop,	emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	emptyOrderItemList, emptyCompany,	null,		emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	emptyOrderItemList, emptyCompany,	emptyShop,	null));
		}
		[Test]
		public void Test_Create_Invoice_Sets_ConversionSettings() {
			var emptyOrder = new OrderRow();
			var emptyOrderItemList = new List<IOrderItem>();
			var emptyCompany = new CompanyRow();
			var emptyShop = new ShopRow();
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, ediSettings);
			Assert.AreEqual("56936677", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
			//Continue test and build
		}
	}
}