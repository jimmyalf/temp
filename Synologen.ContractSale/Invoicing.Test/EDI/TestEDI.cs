using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;

namespace Spinit.Wpc.Synologen.Test.EDI {
	[TestFixture]
	public class TestEDI 
	{
		private EDIConversionSettings ediSettings;

		[SetUp]
		public void Setup()
		{

			ediSettings = new EDIConversionSettings
			{
				SenderId = "556262-6100", 
				BankGiro = "5693-6677", 
				VATAmount = 0.25F
			};
		}

		[Test]
		public void Can_create_edi_invoice()
		{
			var company = Factory.Factory.GetCompany();
			var shop = Factory.Factory.GetShop();
			var invoiceItems = Factory.Factory.GetOrderItems();
			var order = Factory.Factory.GetOrder(company, shop, invoiceItems);
			var invoice = General.CreateInvoiceEDI(order, ediSettings);
			var invoiceText = invoice.Parse();

			Assert.IsNotNull(invoiceText);

		}
	}
}