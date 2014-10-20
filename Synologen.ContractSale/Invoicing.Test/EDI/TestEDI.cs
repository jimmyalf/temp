using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.EDI {
	[TestFixture]
	public class TestEDI 
	{
		private EDIConversionSettings _ediSettings;

		[SetUp]
		public void Setup()
		{

			_ediSettings = new EDIConversionSettings
			{
				SenderEdiAddress = new EdiAddress("556262-6100"), 
				BankGiro = "5693-6677", 
				VATAmount = 0.25F
			};
		}

		[Test]
		public void Can_create_edi_invoice()
		{
			var company = Factory.GetCompany();
			var shop = Factory.GetShop();
			var invoiceItems = Factory.GetOrderItems();
			var order = Factory.GetOrder(company, shop, invoiceItems);
			var invoice = General.CreateInvoiceEDI(order, _ediSettings);
			var invoiceText = invoice.Parse();

			Assert.IsNotNull(invoiceText);

		}
	}
}