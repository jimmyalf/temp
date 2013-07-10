using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
    [TestFixture]
    public class Parse_Invoice_Payment_Means : SvefakturaTestBase
    {
        private Order _order;
        private SvefakturaConversionSettings _settings;
        private SFTIInvoiceType _invoice;
        private Company _company;

        [SetUp]
        public void Setup()
        {
            _company = Factory.GetCompany();
            _order = Factory.GetOrder(_company);
            _settings = Factory.GetSettings();
            _invoice = BuildInvoice<PaymentMeansBuilder>(_order);
        }

        [Test]
        public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution_BankGiro() 
        {
            var paymentMeans = _invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "BGABSESS");
            Assert.AreEqual("BGABSESS", paymentMeans.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
            Assert.AreEqual("56936677", paymentMeans.PayeeFinancialAccount.ID.Value);
            Assert.AreEqual(PaymentMeansCodeContentType.Item1, paymentMeans.PaymentMeansTypeCode.Value);
            Assert.AreEqual(_settings.InvoiceIssueDate.AddDays(_company.PaymentDuePeriod), _invoice.PaymentMeans[0].DuePaymentDate.Value);
        }
        [Test]
        public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution_PostGiro() 
        {
            var paymentMeans = _invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "PGSISESS");
            Assert.AreEqual("PGSISESS", paymentMeans.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
            Assert.AreEqual("123456", paymentMeans.PayeeFinancialAccount.ID.Value);
            Assert.AreEqual(PaymentMeansCodeContentType.Item1, paymentMeans.PaymentMeansTypeCode.Value);
            Assert.AreEqual(_settings.InvoiceIssueDate.AddDays(_company.PaymentDuePeriod), _invoice.PaymentMeans[0].DuePaymentDate.Value);
        }
    }
}