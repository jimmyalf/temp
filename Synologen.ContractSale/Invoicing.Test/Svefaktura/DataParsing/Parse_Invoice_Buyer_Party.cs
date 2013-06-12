using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
    [TestFixture]
    public class Parse_Invoice_Buyer_Party : SvefakturaTestBase
    {
        private Order _order;
        private SFTIInvoiceType _invoice;

        [SetUp]
        public void Setup()
        {
            var company = Factory.GetCompany();
            _order = Factory.GetOrder(company);
            _invoice = BuildInvoice<BuyerPartyBuilder>(_order);
        }

        [Test]
        public void Test_Create_Invoice_Sets_BuyerParty_Address()
        {
            var invoice = General.CreateInvoiceSvefaktura(_order, Settings);

            Assert.AreEqual(_order.ContractCompany.PostBox, invoice.BuyerParty.Party.Address.Postbox.Value);
            Assert.AreEqual(_order.ContractCompany.StreetName, invoice.BuyerParty.Party.Address.StreetName.Value);
            Assert.AreEqual(_order.ContractCompany.Zip, invoice.BuyerParty.Party.Address.PostalZone.Value);
            Assert.AreEqual(_order.ContractCompany.City, invoice.BuyerParty.Party.Address.CityName.Value);
            Assert.AreEqual(_order.CompanyUnit, invoice.BuyerParty.Party.Address.Department.Value);
            Assert.AreEqual(_order.ContractCompany.InvoiceCompanyName, invoice.BuyerParty.Party.PartyName[0].Value);
            Assert.AreEqual(_order.ContractCompany.OrganizationNumber, invoice.BuyerParty.Party.PartyIdentification[0].ID.Value);
            Assert.AreEqual(_order.CustomerFirstName + " " + _order.CustomerLastName, invoice.BuyerParty.Party.Contact.Name.Value);
            Assert.AreEqual(_order.Phone, invoice.BuyerParty.Party.Contact.Telephone.Value);
            Assert.AreEqual(_order.Email, invoice.BuyerParty.Party.Contact.ElectronicMail.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_BuyerParty_VAT_Tax_Scheme()
        {
            var vatTaxScheme = _invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
            Assert.IsNotNull(vatTaxScheme);
            Assert.AreEqual(_order.ContractCompany.TaxAccountingCode, vatTaxScheme.CompanyID.Value);
            Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_BuyerParty_SWT_Tax_Scheme()
        {
            var swtTaxScheme = _invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
            Assert.IsNotNull(swtTaxScheme);
            Assert.AreEqual(_order.ContractCompany.OrganizationNumber, swtTaxScheme.CompanyID.Value);
            Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
            Assert.AreEqual(_order.ContractCompany.City, swtTaxScheme.RegistrationAddress.CityName.Value);
            Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
            Assert.AreEqual(_order.ContractCompany.Country.Name, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.name);
        }

        [Test]
        public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_FirstName_Missing() 
        {
            _order.CustomerFirstName = null;
            var invoice = BuildInvoice<BuyerPartyBuilder>(_order);
            Assert.AreEqual(_order.CustomerLastName, invoice.BuyerParty.Party.Contact.Name.Value);
        }

        [Test]
        public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_LastName_Missing() 
        {
            _order.CustomerLastName = null;
            var invoice = BuildInvoice<BuyerPartyBuilder>(_order);
            Assert.AreEqual(_order.CustomerFirstName, invoice.BuyerParty.Party.Contact.Name.Value);
        }
    }
}