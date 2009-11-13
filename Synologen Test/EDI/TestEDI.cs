using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;

namespace Spinit.Wpc.Synologen.Unit.Test.EDI{
	[TestFixture]
	public class TestEDI {
		private string connectionString;
		private EDIConversionSettings ediSettings;

		[TestFixtureSetUp, Explicit]
		public void Setup() {
			connectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
			ediSettings = new EDIConversionSettings {
			                                        	SenderId = "556262-6100",
			                                        	BankGiro = "5693-6677",
			                                        	VATAmount = 0.25F
			                                        };
		}

		[TestFixtureTearDown]
		public void TearDown() {}

		[Test, Explicit]
		public void CreateEDIInvoices() {
			var provider = new SqlProvider(connectionString);
			for (var i = 19; i <= 30; i++) {
				var order = provider.GetOrder(i);
				var invoice = General.CreateInvoiceEDI(order, ediSettings);
				var invoiceText = invoice.Parse();
				Assert.IsNotNull(invoiceText);
			}
		}
		[Test]
		public void CreateMockEDIInvoice() {
			const int orderId = 1;
			const int orderItemId = 1;
			var order = Mock.Utility.GetMockOrderRow(orderId);
			order.SellingShop = Mock.Utility.GetMockShopRow();
			order.OrderItems =  new List<OrderItem> {Mock.Utility.GetMockOrderItemRow(orderId, orderItemId)};
			order.ContractCompany = Mock.Utility.GetMockCompanyRow();
			var invoice = General.CreateInvoiceEDI(order, ediSettings);
			var invoiceText = invoice.Parse();
			Assert.IsNotNull(invoiceText);
		}

	}
}