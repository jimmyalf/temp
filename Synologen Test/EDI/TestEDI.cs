using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.EDI {
	[TestFixture]
	public class TestEDI {
		private string connectionString;
		private SqlProvider provider;
		private EDIConversionSettings ediSettings;

		[TestFixtureSetUp]
		[Ignore]
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
		[Ignore]
		public void CreateEDIInvoices() {
			for (var i = 19; i <= 30; i++) {
				var order = provider.GetOrder(i);
				var invoice = Utility.General.CreateInvoiceEDI(order, ediSettings);
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
			var invoice = Utility.General.CreateInvoiceEDI(order, ediSettings);
			var invoiceText = invoice.Parse();
			Assert.IsNotNull(invoiceText);
		}

	}
}