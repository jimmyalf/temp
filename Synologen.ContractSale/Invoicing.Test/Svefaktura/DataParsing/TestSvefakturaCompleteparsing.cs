using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Test.Factory;
using Convert=Spinit.Wpc.Synologen.Invoicing.Convert;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.DataParsing
{
	[TestFixture]
	public class TestSvefakturaCompleteParsing : AssertionHelper
	{
		private Order order;
		private SvefakturaConversionSettings settings;
		private SFTIInvoiceType invoice;
		private const int SwedenCountryCodeNumber = 187;

		[TestFixtureSetUp]
		public void Setup(){
			order = new Order
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
				RstText = "Kostnadsst�lle ABC",
				CreatedDate = new DateTime(2009, 11, 18),
				ContractCompany = new Company 
				{
					StreetName = "Gatuadress 1",
					PostBox = "Postbox 123",
					BankCode = "99998",
					City = "J�rf�lla",
					Zip = "17588",
					Country = new Country {Id = 1, Name = "Sverige", OrganizationCountryCodeId = SwedenCountryCodeNumber},
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
						Notes = "Applicera l�ngsamt",
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
			settings = Factory.GetSettings();
			//settings = new SvefakturaConversionSettings {
			//    BankGiro = "123456789",
			//    BankgiroBankIdentificationCode = "BGABSESS",
			//    Postgiro = "987654321",
			//    PostgiroBankIdentificationCode = "PGSISESS",
			//    ExemptionReason = "Innehar f-skattebevis",
			//    InvoiceCurrencyCode = CurrencyCodeContentType.SEK,
			//    InvoiceExpieryPenaltySurchargePercent = 12.50m,
			//    InvoiceIssueDate = new DateTime(2009, 10, 28),
			//    InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagar netto",
			//    InvoiceTypeCode = "380",
			//    SellingOrganizationCity = "Klippan",
			//    SellingOrganizationContactEmail = "info@synologen.se",
			//    SellingOrganizationContactName = "Lotta Wieslander",
			//    SellingOrganizationCountry = new SFTICountryType{ IdentificationCode = new CountryIdentificationCodeType{ Value = CountryIdentificationCodeContentType.SE, name="Sverige" } },
			//    SellingOrganizationFax = "0435-134 33",
			//    SellingOrganizationName = "Synh�lsan Svenska AB",
			//    SellingOrganizationNumber = "556401-1962",
			//    SellingOrganizationPostalCode = "264 22",
			//    SellingOrganizationPostBox = "Box 111",
			//    SellingOrganizationStreetName = "K�pmansg�rden",
			//    SellingOrganizationTelephone = "0435-134 33",
			//    TaxAccountingCode = "SE556401196201",
			//    VATAmount = 0.25m,
			//    SellingOrganizationRegistrationCity = "Klippan"
			//};

			invoice = Convert.ToSvefakturaInvoice(settings, order);
		}

		#region Order
		[Test]
		public void Test_Sets_OrderRow_CompanyId(){
			Expect(invoice.Note.Value.Substring(54,3), Is.EqualTo("184"));
		}
		[Test]
		public void Test_Sets_OrderRow_Unit(){
			Expect(invoice.BuyerParty.Party.Address.Department.Value, Is.EqualTo("Avdelning 123"));
		}
		[Test]
		public void Test_Sets_OrderRow_CustomerFirstName(){
			Expect(invoice.Note.Value.Substring(44,4), Is.EqualTo("Adam"));
		}
		[Test]
		public void Test_Sets_OrderRow_CustomerLastName(){
			Expect(invoice.Note.Value.Substring(48,6), Is.EqualTo("Bertil"));
		}
		[Test]
		public void Test_Sets_OrderRow_CustomerCombinedName_In_FreeText(){
			Expect(invoice.Note.Value.StartsWith("Adam Bertil"));
		}
		[Test]
		public void Test_Sets_OrderRow_CustomerCombinedName_In_Party_Contact(){
			Expect(invoice.BuyerParty.Party.Contact.Name.Value, Is.EqualTo("Adam Bertil"));
		}
		[Test]
		public void Test_Sets_OrderRow_CustomerOrderNumber(){
			Expect(invoice.RequisitionistDocumentReference[0].ID.Value, Is.EqualTo("123456"));
		}
		[Test]
		public void Test_Sets_OrderRow_Email_In_PartyContact(){
			Expect(invoice.BuyerParty.Party.Contact.ElectronicMail.Value, Is.EqualTo("adam.bertil@testforetaget.se"));
		}
		[Test]
		public void Test_Sets_OrderRow_InvoiceNumber(){
			Expect(invoice.ID.Value, Is.EqualTo("12345"));
		}
		[Test]
		public void Test_Sets_OrderRow_InvoiceSumExcludingVAT(){
			Expect(invoice.LegalTotal.TaxExclusiveTotalAmount.Value, Is.EqualTo(123456));
		}
		[Test]
		public void Test_Sets_OrderRow_InvoiceSumIncludingVAT(){
			Expect(invoice.LegalTotal.TaxInclusiveTotalAmount.Value, Is.EqualTo(123456));
		}
		[Test]
		public void Test_Sets_OrderRow_PersonalIdNumber(){
			Expect(invoice.Note.Value.Substring(11,12), Is.EqualTo("197001015374"));
		}
		[Test]
		public void Test_Sets_OrderRow_PersonalBirthDate(){
			Expect(invoice.Note.Value.Substring(36,8), Is.EqualTo("19700101"));
		}
		[Test]
		public void Test_Sets_OrderRow_Phone(){
			Expect(invoice.BuyerParty.Party.Contact.Telephone.Value, Is.EqualTo("031123456"));
		}
		[Test]
		public void Test_Sets_OrderRow_RstText(){
			Expect(invoice.Note.Value.Substring(57,18), Is.EqualTo("Kostnadsst�lle ABC"));
		}
		[Test]
		public void Test_Sets_Order_TaxPointDate(){
			Expect(invoice.TaxPointDate.Value, Is.EqualTo(new DateTime(2009,11,18)));
		}
		#endregion

		#region OrderItems
		[Test]
		public void Test_Sets_OrderItemRow_ArticleDisplayName(){
			Expect(invoice.InvoiceLine[0].Item.Description.Value, Is.EqualTo("Artikelnamn 1"));
			Expect(invoice.InvoiceLine[1].Item.Description.Value, Is.EqualTo("Artikelnamn 2"));
			Expect(invoice.InvoiceLine[2].Item.Description.Value, Is.EqualTo("Artikelnamn 3"));
		}
		[Test]
		public void Test_Sets_OrderItemRow_ArticleDisplayNumber(){
			Expect(invoice.InvoiceLine[0].Item.SellersItemIdentification.ID.Value, Is.EqualTo("Artikelnr 12456"));
			Expect(invoice.InvoiceLine[1].Item.SellersItemIdentification.ID.Value, Is.EqualTo("Artikelnr 9854"));
			Expect(invoice.InvoiceLine[2].Item.SellersItemIdentification.ID.Value, Is.EqualTo("Artikelnr 1654"));
		}
		[Test]
		public void Test_Sets_OrderItemRow_DisplayTotalPrice(){
			Expect(invoice.InvoiceLine[0].LineExtensionAmount.Value, Is.EqualTo(123.5m));
			Expect(invoice.InvoiceLine[1].LineExtensionAmount.Value, Is.EqualTo(66m));
			Expect(invoice.InvoiceLine[2].LineExtensionAmount.Value, Is.EqualTo(199.5m));
		}
		[Test]
		public void Test_Sets_OrderItemRow_Notes(){
			Expect(invoice.InvoiceLine[0].Note.Value, Is.EqualTo("Applicera l�ngsamt"));
			Expect(invoice.InvoiceLine[1].Note.Value, Is.EqualTo("Applicera omedelbart"));
			Expect(invoice.InvoiceLine[2].Note, Is.Null);
		}
		[Test]
		public void Test_Sets_OrderItemRow_NumberOfItems(){
			Expect(invoice.InvoiceLine[0].InvoicedQuantity.Value, Is.EqualTo(2));
			Expect(invoice.InvoiceLine[1].InvoicedQuantity.Value, Is.EqualTo(3));
			Expect(invoice.InvoiceLine[2].InvoicedQuantity.Value, Is.EqualTo(2));
		}
		[Test]
		public void Test_Sets_OrderItemRow_NoVAT_TaxCategory_Percent(){
			Expect(invoice.InvoiceLine[0].Item.TaxCategory[0].Percent.Value, Is.EqualTo(25));
			Expect(invoice.InvoiceLine[1].Item.TaxCategory[0].Percent.Value, Is.EqualTo(0));
			Expect(invoice.InvoiceLine[2].Item.TaxCategory[0].Percent.Value, Is.EqualTo(25));
		}
		[Test]
		public void Test_Sets_OrderItemRow_NoVAT_TaxCategory_ID(){
			Expect(invoice.InvoiceLine[0].Item.TaxCategory[0].ID.Value, Is.EqualTo("S"));
			Expect(invoice.InvoiceLine[1].Item.TaxCategory[0].ID.Value, Is.EqualTo("E"));
			Expect(invoice.InvoiceLine[2].Item.TaxCategory[0].ID.Value, Is.EqualTo("S"));
		}
		[Test]
		public void Test_Sets_OrderItemRow_NoVAT_TaxCategory_TaxScheme_ID(){
			Expect(invoice.InvoiceLine[0].Item.TaxCategory[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
			Expect(invoice.InvoiceLine[1].Item.TaxCategory[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
			Expect(invoice.InvoiceLine[2].Item.TaxCategory[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
		}
		[Test]
		public void Test_Sets_OrderItemRow_SinglePrice(){
			Expect(invoice.InvoiceLine[0].Item.BasePrice.PriceAmount.Value, Is.EqualTo(61.75m));
			Expect(invoice.InvoiceLine[1].Item.BasePrice.PriceAmount.Value, Is.EqualTo(22m));
			Expect(invoice.InvoiceLine[2].Item.BasePrice.PriceAmount.Value, Is.EqualTo(99.75m));
		}
		[Test]
		public void Test_Sets_OrderItemRow_Generics(){
			Expect(invoice.InvoiceLine.Count, Is.EqualTo(3));
			Expect(invoice.LineItemCountNumeric.Value, Is.EqualTo(3));
		}
		#endregion

		#region Settings
		[Test]
		public void Test_Sets_Settings_BankGiro()
		{
			var bankGiro = invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "BGABSESS");
			Expect(bankGiro.PayeeFinancialAccount.ID.Value, Is.EqualTo(settings.BankGiro));
		}
		//[Test]
		//public void Test_Sets_Settings_BankgiroBankIdentificationCode(){
		//    Expect(invoice.PaymentMeans[0].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value, Is.EqualTo("BGABSESS"));
		//}
		[Test]
		public void Test_Sets_Settings_Postgiro(){
			var postGiro = invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "PGSISESS");
			Expect(postGiro.PayeeFinancialAccount.ID.Value, Is.EqualTo(settings.Postgiro));
		}
		//[Test]
		//public void Test_Sets_Settings_PostgiroBankIdentificationCode(){
		//    Expect(invoice.PaymentMeans[1].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value, Is.EqualTo("PGSISESS"));
		//}
		[Test]
		public void Test_Sets_Settings_ExemptionReason(){
			Expect(invoice.SellerParty.Party.PartyTaxScheme[1].ExemptionReason.Value, Is.EqualTo("Innehar F-skattebevis"));
		}
		[Test]
		public void Test_Sets_Settings_InvoiceCurrencyCode(){
			Expect(invoice.InvoiceCurrencyCode.Value, Is.EqualTo(CurrencyCodeContentType.SEK));
		}
		[Test]
		public void Test_Sets_Settings_InvoiceExpieryPenaltySurchargePercent(){
			Expect(invoice.PaymentTerms.PenaltySurchargePercent.Value, Is.EqualTo(12.50m));
		}
		[Test]
		public void Test_Sets_Settings_InvoiceIssueDate(){
			Expect(invoice.IssueDate.Value, Is.EqualTo(settings.InvoiceIssueDate));
		}
		[Test]
		public void Test_Sets_Settings_InvoicePaymentTermsTextFormat_Parsed(){
			Expect(invoice.PaymentTerms.Note.Value, Is.EqualTo("30 dagar netto"));
		}
		[Test]
		public void Test_Sets_Settings_InvoiceTypeCode(){
			Expect(invoice.InvoiceTypeCode.Value, Is.EqualTo("380"));
		}

		[Test]
		public void Test_Sets_Order_SellingShop_Address()
		{
			var address = invoice.SellerParty.Party.Address;
			Expect(address.StreetName.Value, Is.EqualTo(order.SellingShop.Address2));
			Expect(address.CityName.Value, Is.EqualTo(order.SellingShop.City));
			Expect(address.Postbox.Value, Is.EqualTo(order.SellingShop.Address));
			Expect(address.PostalZone.Value, Is.EqualTo(order.SellingShop.Zip));
			Expect(address.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_AccountsContact()
		{
			Expect(invoice.SellerParty.AccountsContact.Name.Value, Is.EqualTo(settings.Contact.Name.Value));
			Expect(invoice.SellerParty.AccountsContact.ElectronicMail.Value, Is.EqualTo(settings.Contact.ElectronicMail.Value));
			Expect(invoice.SellerParty.AccountsContact.Telephone.Value, Is.EqualTo(settings.Contact.Telephone.Value));
			Expect(invoice.SellerParty.AccountsContact.Telefax.Value, Is.EqualTo(settings.Contact.Telefax.Value));
		}

		[Test]
		public void Test_Sets_Order_SellingShop_Name()
		{
			Expect(invoice.SellerParty.Party.PartyName[0].Value, Is.EqualTo(order.SellingShop.Name));
		}

		[Test]
		public void Test_Sets_Order_SellingShop_Number()
		{
			Expect(invoice.SellerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo(order.SellingShop.OrganizationNumber));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_TaxAccountingCode()
		{
			Expect(invoice.SellerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo(settings.TaxAccountingCode));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_OrgNumber()
		{
			Expect(invoice.SellerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo(settings.SellingOrganizationNumber.Replace("-","")));
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_Address()
		{
			var address = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value =="VAT").RegistrationAddress;
			address.CityName.Value.ShouldBe(settings.Adress.CityName.Value);
			address.Postbox.Value.ShouldBe(settings.Adress.Postbox.Value);
			address.PostalZone.Value.ShouldBe(settings.Adress.PostalZone.Value);
			address.Country.IdentificationCode.Value.ShouldBe(settings.Adress.Country.IdentificationCode.Value);
			address.Country.IdentificationCode.name.ShouldBe(settings.Adress.Country.IdentificationCode.name);
		}

		[Test]
		public void Test_Sets_Settings_SellingOrganization_Registration_Address()
		{
			var address = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value =="SWT").RegistrationAddress;
			address.CityName.Value.ShouldBe(settings.RegistrationAdress.CityName.Value);
			address.Country.IdentificationCode.Value.ShouldBe(settings.RegistrationAdress.Country.IdentificationCode.Value);
			address.Country.IdentificationCode.name.ShouldBe(settings.RegistrationAdress.Country.IdentificationCode.name);
		}

		[Test]
		public void Test_Sets_Settings_VATAmount(){
			var invoiceLinesWithNormalVAT = invoice.InvoiceLine.Where(x => x.Item.TaxCategory[0].ID.Value.Equals("S"));
			foreach (var invoiceLine in invoiceLinesWithNormalVAT){
				Expect(invoiceLine.Item.TaxCategory[0].Percent.Value, Is.EqualTo(0.25*100));
			}
			var taxSubTotalsWithNormalVAT = invoice.TaxTotal[0].TaxSubTotal.Where(x => x.TaxCategory.ID.Value.Equals("S"));
			foreach (var taxSubTotal in taxSubTotalsWithNormalVAT){
				Expect(taxSubTotal.TaxCategory.Percent.Value, Is.EqualTo(0.25*100));
			}
			Expect(invoiceLinesWithNormalVAT.Count(), Is.EqualTo(2));
			Expect(taxSubTotalsWithNormalVAT.Count(), Is.EqualTo(1));
		}

		[Test]
		public void Test_Sets_Settings_Generics(){
			Expect(invoice.SellerParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value, Is.EqualTo("VAT"));
			Expect(invoice.SellerParty.Party.PartyTaxScheme[1].TaxScheme.ID.Value, Is.EqualTo("SWT"));
		}

		#endregion

		#region Company
		[Test]
		public void Test_Sets_CompanyRow_StreetName(){
			Expect(invoice.BuyerParty.Party.Address.StreetName.Value, Is.EqualTo("Gatuadress 1"));
		}
		[Test]
		public void Test_Sets_CompanyRow_PostBox(){
			Expect(invoice.BuyerParty.Party.Address.Postbox.Value, Is.EqualTo("Postbox 123"));
		}
		[Test]
		public void Test_Sets_CompanyRow_BankCode(){
			Expect(invoice.Note.Value.EndsWith("99998"));
		}
		[Test]
		public void Test_Sets_CompanyRow_City(){
			Expect(invoice.BuyerParty.Party.Address.CityName.Value, Is.EqualTo("J�rf�lla"));
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[1].RegistrationAddress.CityName.Value, Is.EqualTo("J�rf�lla"));
		}
		[Test]
		public void Test_Sets_CompanyRow_Zip(){
			Expect(invoice.BuyerParty.Party.Address.PostalZone.Value, Is.EqualTo("17588"));
		}
		[Test]
		public void Test_Sets_CompanyRow_Country(){
			Expect(invoice.BuyerParty.Party.Address.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[1].RegistrationAddress.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
		}
		[Test]
		public void Test_Sets_CompanyRow_InvoiceCompanyName(){
			Expect(invoice.BuyerParty.Party.PartyName[0].Value, Is.EqualTo("5440Saab AB"));
		}
		[Test]
		public void Test_Sets_CompanyRow_InvoiceFreeTextFormat(){
			Expect(invoice.Note.Value, Is.EqualTo("Adam Bertil197001015374Avdelning 12319700101AdamBertil184Kostnadsst�lle ABC99998"));
		}
		[Test]
		public void Test_Sets_CompanyRow_OrganizationNumber(){
			Expect(invoice.BuyerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo("5560360793"));
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo("5560360793"));
		}
		[Test]
		public void Test_Sets_CompanyRow_TaxAccountingCode(){
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo("SE556036079301"));
		}
		[Test]
		public void Test_Sets_CompanyRow_PaymentDuePeriod(){
			Expect(invoice.PaymentMeans[0].DuePaymentDate.Value, Is.EqualTo(settings.InvoiceIssueDate.AddDays(order.ContractCompany.PaymentDuePeriod)));
			Expect(invoice.PaymentTerms.Note.Value.StartsWith(order.ContractCompany.PaymentDuePeriod.ToString()));
		}
		#endregion

		#region Shop
		[Test]
		public void Test_Sets_ShopRow_CombinedName()
		{
			Expect(invoice.SellerParty.Party.Contact.Name.Value, Is.EqualTo(order.SellingShop.ContactCombinedName));
		}
		[Test]
		public void Test_Sets_ShopRow_Phone()
		{
			Expect(invoice.SellerParty.Party.Contact.Telephone.Value, Is.EqualTo(order.SellingShop.Phone.Replace("-","")));
		}
		[Test]
		public void Test_Sets_ShopRow_Fax()
		{
			Expect(invoice.SellerParty.Party.Contact.Telefax.Value, Is.EqualTo(order.SellingShop.Fax.Replace("-","")));
		}
		[Test]
		public void Test_Sets_ShopRow_Email()
		{
			Expect(invoice.SellerParty.Party.Contact.ElectronicMail.Value, Is.EqualTo(order.SellingShop.Email));
		}

		#endregion
	}
}