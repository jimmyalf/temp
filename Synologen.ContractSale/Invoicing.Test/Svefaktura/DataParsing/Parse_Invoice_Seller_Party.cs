using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
    [TestFixture]
    public class Parse_Invoice_Seller_Party : SvefakturaTestBase
    {
        private Order _order;
        private Shop _shop;
        private SFTIInvoiceType _invoice;

        [SetUp]
        public void Setup()
        {
            var company = Factory.GetCompany();
            _shop = Factory.GetShop();
            _order = Factory.GetOrder(company, _shop);
            _invoice = BuildInvoice<SellerPartyBuilder>(_order);
        }

        [Test]
        public void Test_Create_Invoice_Sets_SellerParty_Address()
        {
            _invoice.SellerParty.Party.Address.Postbox.Value.ShouldBe(_shop.Address);
            _invoice.SellerParty.Party.Address.StreetName.Value.ShouldBe(_shop.Address2);
            _invoice.SellerParty.Party.Address.PostalZone.Value.ShouldBe(_shop.Zip);
            _invoice.SellerParty.Party.Address.CityName.Value.ShouldBe(_shop.City);
            _invoice.SellerParty.Party.Address.Country.IdentificationCode.Value.ShouldBe(CountryIdentificationCodeContentType.SE);
            _invoice.SellerParty.Party.Address.Country.IdentificationCode.name.ShouldBe("Sverige");
        }

        [Test]
        public void Test_Create_Invoice_Sets_SellerParty_PartyName() 
        {
            _invoice.SellerParty.Party.PartyName.First().Value.ShouldBe(_shop.Name);
        }

        [Test]
        public void Test_Create_Invoice_Sets_SellerParty_PartyIdentification() 
        {
            _invoice.SellerParty.Party.PartyIdentification.First().ID.Value.ShouldBe(_shop.OrganizationNumber);
        }

        [Test]
        public void Test_Create_Invoice_Sets_SellerParty_Contact() 
        {
            _invoice.SellerParty.Party.Contact.Name.Value.ShouldBe(_order.SellingShop.ContactCombinedName);
            _invoice.SellerParty.Party.Contact.ElectronicMail.Value.ShouldBe(_shop.Email);
            _invoice.SellerParty.Party.Contact.Telefax.Value.ShouldBe(_shop.Fax);
            _invoice.SellerParty.Party.Contact.Telephone.Value.ShouldBe(_shop.Phone);
        }

        [Test]
        public void Test_Create_Invoice_Sets_SellerParty_AccountsContact()
        {
            _invoice.SellerParty.AccountsContact.ElectronicMail.Value.ShouldBe(Settings.Contact.ElectronicMail.Value);
            _invoice.SellerParty.AccountsContact.Name.Value.ShouldBe(Settings.Contact.Name.Value);
            _invoice.SellerParty.AccountsContact.Telefax.Value.ShouldBe(Settings.Contact.Telefax.Value);
            _invoice.SellerParty.AccountsContact.Telephone.Value.ShouldBe(Settings.Contact.Telephone.Value);
        }


        [Test]
        public void Test_Create_Invoice_Sets_SellerParty_PartyTaxSchemes_VAT() 
        {
            var vatTaxScheme = _invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
            vatTaxScheme.TaxScheme.ID.Value.ShouldBe("VAT");
            vatTaxScheme.CompanyID.Value.ShouldBe(Settings.TaxAccountingCode);
            vatTaxScheme.RegistrationAddress.Postbox.Value.ShouldBe(Settings.Adress.Postbox.Value);
            vatTaxScheme.RegistrationAddress.StreetName.Value.ShouldBe(Settings.Adress.StreetName.Value);
            vatTaxScheme.RegistrationAddress.CityName.Value.ShouldBe(Settings.Adress.CityName.Value);
            vatTaxScheme.RegistrationAddress.PostalZone.Value.ShouldBe(Settings.Adress.PostalZone.Value);
            vatTaxScheme.RegistrationAddress.Country.IdentificationCode.name.ShouldBe(Settings.Adress.Country.IdentificationCode.name);
            vatTaxScheme.RegistrationAddress.Country.IdentificationCode.Value.ShouldBe(Settings.Adress.Country.IdentificationCode.Value);
            vatTaxScheme.RegistrationName.Value.ShouldBe(Settings.SellingOrganizationName);
        }

        [Test]
        public void Test_Create_Invoice_Sets_Settings_PartyTaxSchemes_SWT() 
        {
            var swtTaxScheme = _invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
            swtTaxScheme.TaxScheme.ID.Value.ShouldBe("SWT");
            swtTaxScheme.CompanyID.Value.ShouldBe(Settings.SellingOrganizationNumber);
            swtTaxScheme.ExemptionReason.Value.ShouldBe(Settings.ExemptionReason);
            swtTaxScheme.RegistrationAddress.CityName.Value.ShouldBe(Settings.RegistrationAdress.CityName.Value);
            swtTaxScheme.RegistrationAddress.Country.IdentificationCode.name.ShouldBe(Settings.RegistrationAdress.Country.IdentificationCode.name);
            swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value.ShouldBe(Settings.RegistrationAdress.Country.IdentificationCode.Value);

        }
    }
}