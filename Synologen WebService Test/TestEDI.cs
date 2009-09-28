using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test {
	[TestFixture]
	public class TestEDI {
		private string connectionString;
		private SqlProvider provider;
		private EDIConversionSettings ediSettings;

		[TestFixtureSetUp]
		public void Setup() {
			connectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
			provider = new SqlProvider(connectionString);
			ediSettings = new EDIConversionSettings {
				SenderId = "556262-6100",
				BankGiro = "5693-6677",
				VATAmount = 0.25F
			};
		}

		[TestFixtureTearDown]
		public void TearDown() {}

		[Test]
		public void CreateEDIInvoices() {
			for (var i = 19; i <= 30; i++) {
				var order = provider.GetOrder(i);
				var orderItems = provider.GetOrderItemsList(i, 0, null);
				var company = provider.GetCompanyRow(order.CompanyId);
				var shop = provider.GetShop(order.SalesPersonShopId);
				var invoice = Utility.General.CreateInvoiceEDI(order, orderItems, company, shop, ediSettings);
				var invoiceText = invoice.Parse();
				Assert.IsNotNull(invoiceText);
			}
		}

		[Test]
		public void UploadEDIInvoicesToFTP() {
			for(var i = 19; i <= 30; i++) {
				var order = provider.GetOrder(i);
				var orderItems = provider.GetOrderItemsList(i, 0, null);
				var company = provider.GetCompanyRow(order.CompanyId);
				var shop = provider.GetShop(order.SalesPersonShopId);
				var invoice = Utility.General.CreateInvoiceEDI(order, orderItems, company,shop, ediSettings);
				var invoiceText = invoice.Parse();
				Assert.IsNotNull(invoiceText);
			}
		}
		[Test]
		public void CreateMockEDIInvoice() {
			var orderId = 1;
			var orderItemId = 1;
			var order = Mock.Utility.GetMockOrderRow(orderId);
			var shop = Mock.Utility.GetMockShopRow();
			var orderItems = new List<OrderItemRow> {Mock.Utility.GetMockOrderItemRow(orderId, orderItemId)};
			var company = Mock.Utility.GetMockCompanyRow();
			var invoice = Utility.General.CreateInvoiceEDI(order, orderItems, company,shop, ediSettings);
			var invoiceText = invoice.Parse();
			Assert.IsNotNull(invoiceText);
		}

	}
}