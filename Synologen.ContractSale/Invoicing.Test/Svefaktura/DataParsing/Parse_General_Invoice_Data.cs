using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
    [TestFixture]
    public class Parse_General_Invoice_Data : SvefakturaTestBase
    {
        private Order _order;
        private SFTIInvoiceType _invoice;

        [SetUp]
        public void Setup()
        {
            var company = Factory.GetCompany();
            var shop = Factory.GetShop();
            var orderItems = Factory.GetOrderItems();
            _order = Factory.GetOrder(company, shop, orderItems);
            _invoice = BuildInvoice<EBrev_InvoiceInformationBuilder>(_order);
        }

        [Test]
        public void Test_Create_Invoice_Sets_ID() 
        {
            Assert.AreEqual(_order.InvoiceNumber.ToString(), _invoice.ID.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_RequisitionistDocumentReference() 
        {
            Assert.AreEqual(_order.CustomerOrderNumber, _invoice.RequisitionistDocumentReference[0].ID.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_IssueDate() 
        {

            Assert.AreEqual(Settings.InvoiceIssueDate, _invoice.IssueDate.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_InvoiceTypeCode() 
        {
            Assert.AreEqual(Settings.InvoiceTypeCode, _invoice.InvoiceTypeCode.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_InvoiceCurrencyCode() 
        {
            Assert.AreEqual(_invoice.InvoiceCurrencyCode.Value, _invoice.InvoiceCurrencyCode.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_Note() 
        {
            var expectedNote = _order.ContractCompany.InvoiceFreeTextFormat
                                     .ReplaceWith(new {CustomerName = _order.CustomerCombinedName})
                                     .ReplaceWith(new {CustomerPersonalIdNumber = _order.PersonalIdNumber})
                                     .ReplaceWith(new {CompanyUnit = _order.CompanyUnit})
                                     .ReplaceWith(new {CustomerPersonalBirthDateString = _order.PersonalBirthDateString})
                                     .ReplaceWith(new {CustomerFirstName = _order.CustomerFirstName})
                                     .ReplaceWith(new {CustomerLastName = _order.CustomerLastName})
                                     .ReplaceWith(new {RST = _order.RstText})
                                     .ReplaceWith(new {BuyerCompanyId = _order.CompanyId})
                                     .ReplaceWith(new {BankCode = _order.ContractCompany.BankCode})
                                     .ReplaceWith(new {SellingShopName = _order.SellingShop.Name})
                                     .ReplaceWith(new {SellingShopNumber = _order.SellingShop.Number});
            Assert.AreEqual(expectedNote, _invoice.Note.Value);
        }
    }
}