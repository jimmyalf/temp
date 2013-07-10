using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
	[TestFixture]
	public class Parse_Complete : AssertionHelper
	{
        private const int SwedenCountryCodeNumber = 187;
		private Order _order;
		private SvefakturaConversionSettings _settings;
		private SFTIInvoiceType _invoice;

		[TestFixtureSetUp]
		public void Setup()
        {
			_order = new Order
			{
				CompanyId = 184,
				CompanyUnit = "Avdelning 123",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				CustomerOrderNumber = "123456",
				Email = "adam.bertil@testforetaget.se",
				InvoiceNumber = 12345,
				InvoiceSumExcludingVAT = 123456,
				InvoiceSumIncludingVAT = 123456,
				PersonalIdNumber = "197001015374",
				Phone = "031-123456",
				RstText = "Kostnadsställe ABC",
				CreatedDate = new DateTime(2009, 11, 18),
				ContractCompany = new Company 
				{
					StreetName = "Gatuadress 1",
					PostBox = "Postbox 123",
					BankCode = "99998",
					City = "Järfälla",
					Zip = "17588",
					Country = new Country { Id = 1, Name = "Sverige", OrganizationCountryCodeId = SwedenCountryCodeNumber },
					InvoiceCompanyName = "5440Saab AB",
					InvoiceFreeTextFormat = "{CustomerName}{CustomerPersonalIdNumber}{CompanyUnit}{CustomerPersonalBirthDateString}{CustomerFirstName}{CustomerLastName}{BuyerCompanyId}{RST}{BankCode}",
					OrganizationNumber = "5560360793",
					TaxAccountingCode = "SE556036079301",
					PaymentDuePeriod = 30,
				},
				SellingShop = new Shop 
				{
					ContactFirstName = "Anders",
					ContactLastName = "Andersson",
					Phone = "031-987654",
					Fax = "031-987653",
					Email = "info@butiken.se",
					Name = "Synbutiken AB",
                    Address = "Box 123",
					Address2 = "Storgata 12",
					Zip = "123 45",
                    City = "Storstad",
                    OrganizationNumber = "5555551234",
				},
				OrderItems = new List<OrderItem> 
				{
					new OrderItem 
					{
						ArticleDisplayName = "Artikelnamn 1",
						ArticleDisplayNumber = "Artikelnr 12456",
						DisplayTotalPrice = 123.5f,
						Notes = "Applicera långsamt",
						NumberOfItems = 2,
						NoVAT = false,
						SinglePrice = 61.75f,
					},
					new OrderItem 
					{
						ArticleDisplayName = "Artikelnamn 2",
						ArticleDisplayNumber = "Artikelnr 9854",
						DisplayTotalPrice = 66f,
						Notes = "Applicera omedelbart",
						NumberOfItems = 3,
						NoVAT = true,
						SinglePrice = 22f,
					},
					new OrderItem 
					{
						ArticleDisplayName = "Artikelnamn 3",
						ArticleDisplayNumber = "Artikelnr 1654",
						DisplayTotalPrice = 199.5f,
						Notes = null,
						NumberOfItems = 2,
						NoVAT = false,
						SinglePrice = 99.75f,
					}
				},
			};
			_settings = Factory.GetSettings();

		    _invoice = CreateInvoice(_settings, _order);
        }

	    private SFTIInvoiceType CreateInvoice(ISvefakturaConversionSettings settings, IOrder order)
	    {
            var builder = new EBrevSvefakturaBuilder(new SvefakturaFormatter(), settings, new Invoicing.Svefaktura.SvefakturaBuilderBuilderValidator());
            return builder.Build(order);   
	    }

		#region Order
		[Test]
		public void Test_Sets_OrderRow_CompanyId()
		{
		    Expect(_invoice.Note.Value.Substring(54, 3), Is.EqualTo("184"));
		}

		[Test]
		public void Test_Sets_OrderRow_Unit()
        {
			Expect(_invoice.BuyerParty.Party.Address.Department.Value, Is.EqualTo("Avdelning 123"));
		}

		[Test]
		public void Test_Sets_OrderRow_CustomerFirstName()
		{
		    Expect(_invoice.Note.Value.Substring(44, 4), Is.EqualTo("Adam"));
		}

		[Test]
		public void Test_Sets_OrderRow_CustomerLastName()
		{
		    Expect(_invoice.Note.Value.Substring(48, 6), Is.EqualTo("Bertil"));
		}

		[Test]
		public void Test_Sets_OrderRow_CustomerCombinedName_In_FreeText()
        {
			Expect(_invoice.Note.Value.StartsWith("Adam Bertil"));
		}

		[Test]
		public void Test_Sets_OrderRow_CustomerCombinedName_In_Party_Contact()
        {
			Expect(_invoice.BuyerParty.Party.Contact.Name.Value, Is.EqualTo("Adam Bertil"));
		}

		[Test]
		public void Test_Sets_OrderRow_CustomerOrderNumber()
        {
			Expect(_invoice.RequisitionistDocumentReference[0].ID.Value, Is.EqualTo("123456"));
		}

		[Test]
		public void Test_Sets_OrderRow_Email_In_PartyContact()
        {
			Expect(_invoice.BuyerParty.Party.Contact.ElectronicMail.Value, Is.EqualTo("adam.bertil@testforetaget.se"));
		}

		[Test]
		public void Test_Sets_OrderRow_InvoiceNumber()
        {
			Expect(_invoice.ID.Value, Is.EqualTo("12345"));
		}

		[Test]
		public void Test_Sets_OrderRow_InvoiceSumExcludingVAT()
        {
			Expect(_invoice.LegalTotal.TaxExclusiveTotalAmount.Value, Is.EqualTo(123456));
		}

		[Test]
		public void Test_Sets_OrderRow_InvoiceSumIncludingVAT()
        {
			Expect(_invoice.LegalTotal.TaxInclusiveTotalAmount.Value, Is.EqualTo(123456));
		}

		[Test]
		public void Test_Sets_OrderRow_PersonalIdNumber()
		{
		    Expect(_invoice.Note.Value.Substring(11, 12), Is.EqualTo("197001015374"));
		}

		[Test]
		public void Test_Sets_OrderRow_PersonalBirthDate()
		{
		    Expect(_invoice.Note.Value.Substring(36, 8), Is.EqualTo("19700101"));
		}

		[Test]
		public void Test_Sets_OrderRow_Phone()
        {
			Expect(_invoice.BuyerParty.Party.Contact.Telephone.Value, Is.EqualTo("031123456"));
		}

		[Test]
		public void Test_Sets_OrderRow_RstText()
		{
		    Expect(_invoice.Note.Value.Substring(57, 18), Is.EqualTo("Kostnadsställe ABC"));
		}

		[Test]
		public void Test_Sets_Order_TaxPointDate()
		{
		    Expect(_invoice.TaxPointDate.Value, Is.EqualTo(new DateTime(2009, 11, 18)));
		}
		#endregion

		#region OrderItems
		[Test]
		public void Test_Sets_OrderItemRow_ArticleDisplayName()
        {
			Expect(_invoice.InvoiceLine[0].Item.Description.Value, Is.EqualTo("Artikelnamn 1"));
			Expect(_invoice.InvoiceLine[1].Item.Description.Value, Is.EqualTo("Artikelnamn 2"));
			Expect(_invoice.InvoiceLine[2].Item.Description.Value, Is.EqualTo("Artikelnamn 3"));
		}

		[Test]
		public void Test_Sets_OrderItemRow_ArticleDisplayNumber()
        {
			Expect(_invoice.InvoiceLine[0].Item.SellersItemIdentification.ID.Value, Is.EqualTo("Artikelnr 12456"));
			Expect(_invoice.InvoiceLine[1].Item.SellersItemIdentification.ID.Value, Is.EqualTo("Artikelnr 9854"));
			Expect(_invoice.InvoiceLine[2].Item.SellersItemIdentification.ID.Value, Is.EqualTo("Artikelnr 1654"));
		}

		[Test]
		public void Test_Sets_OrderItemRow_DisplayTotalPrice()
        {
			Expect(_invoice.InvoiceLine[0].LineExtensionAmount.Value, Is.EqualTo(123.5m));
			Expect(_invoice.InvoiceLine[1].LineExtensionAmount.Value, Is.EqualTo(66m));
			Expect(_invoice.InvoiceLine[2].LineExtensionAmount.Value, Is.EqualTo(199.5m));
		}

		[Test]
		public void Test_Sets_OrderItemRow_Notes()
        {
			Expect(_invoice.InvoiceLine[0].Note.Value, Is.EqualTo("Applicera långsamt"));
			Expect(_invoice.InvoiceLine[1].Note.Value, Is.EqualTo("Applicera omedelbart"));
			Expect(_invoice.InvoiceLine[2].Note, Is.Null);
		}

		[Test]
		public void Test_Sets_OrderItemRow_NumberOfItems()
        {
			Expect(_invoice.InvoiceLine[0].InvoicedQuantity.Value, Is.EqualTo(2));
			Expect(_invoice.InvoiceLine[1].InvoicedQuantity.Value, Is.EqualTo(3));
			Expect(_invoice.InvoiceLine[2].InvoicedQuantity.Value, Is.EqualTo(2));
		}

		[Test]
		public void Test_Sets_OrderItemRow_NoVAT_TaxCategory_Percent()
        {
			Expect(_invoice.InvoiceLine[0].Item.TaxCategory[0].Percent.Value, Is.EqualTo(25));
			Expect(_invoice.InvoiceLine[1].Item.TaxCategory[0].Percent.Value, Is.EqualTo(0));
			Expect(_invoice.InvoiceLine[2].Item.TaxCategory[0].Percent.Value, Is.EqualTo(25));
		}

		[Test]
		public void Test_Sets_OrderItemRow_NoVAT_TaxCategory_ID()
        {
			Expect(_invoice.InvoiceLine[0].Item.TaxCategory[0].ID.Value, Is.EqualTo("S"));
			Expect(_invoice.InvoiceLine[1].Item.TaxCategory[0].ID.Value, Is.EqualTo("E"));
			Expect(_invoice.InvoiceLine[2].Item.TaxCategory[0].ID.Value, Is.EqualTo("S"));
		}

		[Test]
		public void Test_Sets_OrderItemRow_NoVAT_TaxCategory_TaxScheme_ID()
        {
			Expect(_invoice.InvoiceLine[0].Item.TaxCategory[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
			Expect(_invoice.InvoiceLine[1].Item.TaxCategory[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
			Expect(_invoice.InvoiceLine[2].Item.TaxCategory[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
		}

		[Test]
		public void Test_Sets_OrderItemRow_SinglePrice()
        {
			Expect(_invoice.InvoiceLine[0].Item.BasePrice.PriceAmount.Value, Is.EqualTo(61.75m));
			Expect(_invoice.InvoiceLine[1].Item.BasePrice.PriceAmount.Value, Is.EqualTo(22m));
			Expect(_invoice.InvoiceLine[2].Item.BasePrice.PriceAmount.Value, Is.EqualTo(99.75m));
		}

		[Test]
		public void Test_Sets_OrderItemRow_Generics()
        {
			Expect(_invoice.InvoiceLine.Count, Is.EqualTo(3));
			Expect(_invoice.LineItemCountNumeric.Value, Is.EqualTo(3));
		}
		#endregion

		#region Settings
		[Test]
		public void Test_Sets_Settings_BankGiro()
		{
			var bankGiro = _invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "BGABSESS");
			Expect(bankGiro.PayeeFinancialAccount.ID.Value, Is.EqualTo(_settings.BankGiro));
		}

		[Test]
		public void Test_Sets_Settings_Postgiro()
        {
			var postGiro = _invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "PGSISESS");
			Expect(postGiro.PayeeFinancialAccount.ID.Value, Is.EqualTo(_settings.Postgiro));
		}

		[Test]
		public void Test_Sets_Settings_ExemptionReason()
        {
			Expect(_invoice.SellerParty.Party.PartyTaxScheme[1].ExemptionReason.Value, Is.EqualTo("Innehar F-skattebevis"));
		}

		[Test]
		public void Test_Sets_Settings_InvoiceCurrencyCode()
        {
			Expect(_invoice.InvoiceCurrencyCode.Value, Is.EqualTo(CurrencyCodeContentType.SEK));
		}

		[Test]
		public void Test_Sets_Settings_InvoiceExpieryPenaltySurchargePercent()
        {
			Expect(_invoice.PaymentTerms.PenaltySurchargePercent.Value, Is.EqualTo(12.50m));
		}

		[Test]
		public void Test_Sets_Settings_InvoiceIssueDate()
        {
			Expect(_invoice.IssueDate.Value, Is.EqualTo(_settings.InvoiceIssueDate));
		}

		[Test]
		public void Test_Sets_Settings_InvoicePaymentTermsTextFormat_Parsed()
        {
			Expect(_invoice.PaymentTerms.Note.Value, Is.EqualTo("30 dagar netto"));
		}

		[Test]
		public void Test_Sets_Settings_InvoiceTypeCode()
        {
			Expect(_invoice.InvoiceTypeCode.Value, Is.EqualTo("380"));
		}

		[Test]
		public void Test_Sets_Order_SellingShop_Address()
		{
			var address = _invoice.SellerParty.Party.Address;
			Expect(address.StreetName.Value, Is.EqualTo(_order.SellingShop.Address2));
			Expect(address.CityName.Value, Is.EqualTo(_order.SellingShop.City));
			Expect(address.Postbox.Value, Is.EqualTo(_order.SellingShop.Address));
			Expect(address.PostalZone.Value, Is.EqualTo(_order.SellingShop.Zip));
			Expect(address.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_AccountsContact()
		{
			Expect(_invoice.SellerParty.AccountsContact.Name.Value, Is.EqualTo(_settings.Contact.Name.Value));
			Expect(_invoice.SellerParty.AccountsContact.ElectronicMail.Value, Is.EqualTo(_settings.Contact.ElectronicMail.Value));
			Expect(_invoice.SellerParty.AccountsContact.Telephone.Value, Is.EqualTo(_settings.Contact.Telephone.Value));
			Expect(_invoice.SellerParty.AccountsContact.Telefax.Value, Is.EqualTo(_settings.Contact.Telefax.Value));
		}

		[Test]
		public void Test_Sets_Order_SellingShop_Name()
		{
			Expect(_invoice.SellerParty.Party.PartyName[0].Value, Is.EqualTo(_order.SellingShop.Name));
		}

		[Test]
		public void Test_Sets_Order_SellingShop_Number()
		{
			Expect(_invoice.SellerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo(_order.SellingShop.OrganizationNumber));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_TaxAccountingCode()
		{
			Expect(_invoice.SellerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo(_settings.TaxAccountingCode));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_OrgNumber()
		{
            Expect(_invoice.SellerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo(_settings.SellingOrganizationNumber.Replace("-", string.Empty)));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_Address()
		{
			var address = _invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value == "VAT").RegistrationAddress;
			address.CityName.Value.ShouldBe(_settings.Adress.CityName.Value);
			address.Postbox.Value.ShouldBe(_settings.Adress.Postbox.Value);
			address.PostalZone.Value.ShouldBe(_settings.Adress.PostalZone.Value);
			address.Country.IdentificationCode.Value.ShouldBe(_settings.Adress.Country.IdentificationCode.Value);
			address.Country.IdentificationCode.name.ShouldBe(_settings.Adress.Country.IdentificationCode.name);
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_Registration_Address()
		{
			var address = _invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value == "SWT").RegistrationAddress;
			address.CityName.Value.ShouldBe(_settings.RegistrationAdress.CityName.Value);
			address.Country.IdentificationCode.Value.ShouldBe(_settings.RegistrationAdress.Country.IdentificationCode.Value);
			address.Country.IdentificationCode.name.ShouldBe(_settings.RegistrationAdress.Country.IdentificationCode.name);
		}

		[Test]
		public void Test_Sets_Settings_VATAmount()
        {
			var invoiceLinesWithNormalVat = _invoice.InvoiceLine.Where(x => x.Item.TaxCategory[0].ID.Value.Equals("S")).ToList();
			foreach (var invoiceLine in invoiceLinesWithNormalVat)
            {
				Expect(invoiceLine.Item.TaxCategory[0].Percent.Value, Is.EqualTo(0.25 * 100));
			}

			var taxSubTotalsWithNormalVAT = _invoice.TaxTotal[0].TaxSubTotal.Where(x => x.TaxCategory.ID.Value.Equals("S")).ToList();
			foreach (var taxSubTotal in taxSubTotalsWithNormalVAT)
            {
				Expect(taxSubTotal.TaxCategory.Percent.Value, Is.EqualTo(0.25 * 100));
			}

			Expect(invoiceLinesWithNormalVat.Count(), Is.EqualTo(2));
			Expect(taxSubTotalsWithNormalVAT.Count(), Is.EqualTo(1));
		}

		[Test]
		public void Test_Sets_Settings_Generics()
        {
			Expect(_invoice.SellerParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
			Expect(_invoice.SellerParty.Party.PartyTaxScheme[1].TaxScheme.ID.Value, Is.EqualTo("SWT"));
		}

		#endregion

		#region Company
		[Test]
		public void Test_Sets_CompanyRow_StreetName()
        {
			Expect(_invoice.BuyerParty.Party.Address.StreetName.Value, Is.EqualTo("Gatuadress 1"));
		}

		[Test]
		public void Test_Sets_CompanyRow_PostBox()
        {
			Expect(_invoice.BuyerParty.Party.Address.Postbox.Value, Is.EqualTo("Postbox 123"));
		}

		[Test]
		public void Test_Sets_CompanyRow_BankCode()
        {
			Expect(_invoice.Note.Value.EndsWith("99998"));
		}

		[Test]
		public void Test_Sets_CompanyRow_City()
        {
			Expect(_invoice.BuyerParty.Party.Address.CityName.Value, Is.EqualTo("Järfälla"));
			Expect(_invoice.BuyerParty.Party.PartyTaxScheme[1].RegistrationAddress.CityName.Value, Is.EqualTo("Järfälla"));
		}

		[Test]
		public void Test_Sets_CompanyRow_Zip()
        {
			Expect(_invoice.BuyerParty.Party.Address.PostalZone.Value, Is.EqualTo("17588"));
		}

		[Test]
		public void Test_Sets_CompanyRow_Country()
        {
			Expect(_invoice.BuyerParty.Party.Address.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
			Expect(_invoice.BuyerParty.Party.PartyTaxScheme[1].RegistrationAddress.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
		}

		[Test]
		public void Test_Sets_CompanyRow_InvoiceCompanyName()
        {
			Expect(_invoice.BuyerParty.Party.PartyName[0].Value, Is.EqualTo("5440Saab AB"));
		}

		[Test]
		public void Test_Sets_CompanyRow_InvoiceFreeTextFormat()
        {
			Expect(_invoice.Note.Value, Is.EqualTo("Adam Bertil197001015374Avdelning 12319700101AdamBertil184Kostnadsställe ABC99998"));
		}

		[Test]
		public void Test_Sets_CompanyRow_OrganizationNumber()
        {
			Expect(_invoice.BuyerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo("5560360793"));
			Expect(_invoice.BuyerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo("5560360793"));
		}

		[Test]
		public void Test_Sets_CompanyRow_TaxAccountingCode()
        {
			Expect(_invoice.BuyerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo("SE556036079301"));
		}

		[Test]
		public void Test_Sets_CompanyRow_PaymentDuePeriod()
        {
			Expect(_invoice.PaymentMeans[0].DuePaymentDate.Value, Is.EqualTo(_settings.InvoiceIssueDate.AddDays(_order.ContractCompany.PaymentDuePeriod)));
			Expect(_invoice.PaymentTerms.Note.Value.StartsWith(_order.ContractCompany.PaymentDuePeriod.ToString()));
		}
		#endregion

		#region Shop
		[Test]
		public void Test_Sets_ShopRow_CombinedName()
		{
			Expect(_invoice.SellerParty.Party.Contact.Name.Value, Is.EqualTo(_order.SellingShop.ContactCombinedName));
		}

		[Test]
		public void Test_Sets_ShopRow_Phone()
		{
			Expect(_invoice.SellerParty.Party.Contact.Telephone.Value, Is.EqualTo(_order.SellingShop.Phone.Replace("-", string.Empty)));
		}

		[Test]
		public void Test_Sets_ShopRow_Fax()
		{
			Expect(_invoice.SellerParty.Party.Contact.Telefax.Value, Is.EqualTo(_order.SellingShop.Fax.Replace("-", string.Empty)));
		}

		[Test]
		public void Test_Sets_ShopRow_Email()
		{
			Expect(_invoice.SellerParty.Party.Contact.ElectronicMail.Value, Is.EqualTo(_order.SellingShop.Email));
		}

		#endregion
	}
}