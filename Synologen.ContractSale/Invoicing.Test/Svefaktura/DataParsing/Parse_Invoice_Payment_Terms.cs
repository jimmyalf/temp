using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing 
{
    [TestFixture]
	public class Parse_Invoice_Payment_Terms : SvefakturaTestBase
	{
		private SvefakturaConversionSettings _settings;
		private SFTIInvoiceType _invoice;
		private Company _company;

		[SetUp]
		public void Setup()
		{
			_company = Factory.GetCompany();
			var order = Factory.GetOrder(_company);
			_settings = Factory.GetSettings();
		    _invoice = BuildInvoice<PaymentTermsBuilder>(order);
		}

		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoicePaymentTermsTextFormat()
		{
			var expectedText = _settings.InvoicePaymentTermsTextFormat.Replace("{InvoiceNumberOfDueDays}", _company.PaymentDuePeriod.ToString());
			Assert.AreEqual(expectedText, _invoice.PaymentTerms.Note.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoiceExpieryPenaltySurcharge() 
		{
			Assert.AreEqual(_settings.InvoiceExpieryPenaltySurchargePercent, _invoice.PaymentTerms.PenaltySurchargePercent.Value);
		}
	}
}