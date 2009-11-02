using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.DataParsing{
	[TestFixture]
	public class TestSvefakturaCompleteParsing : AssertionHelper{
		private Order order;
		private IList<IOrderItem> orderItems;
		private SvefakturaConversionSettings settings;
		private Company company;
		private Shop shop;
		private SFTIInvoiceType invoice;
		private const int SwedenCountryCodeNumber = 187;

		[TestFixtureSetUp]
		public void Setup(){
			order = new Order {
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
			};
			orderItems = new List<IOrderItem> {
				new OrderItem {
					ArticleDisplayName = "Artikelnamn 1",
					ArticleDisplayNumber = "Artikelnr 12456",
					DisplayTotalPrice = 123.5f,
					Notes = "Applicera långsamt",
					NumberOfItems = 2,
					NoVAT = false,
					SinglePrice = 61.75f,
				},
				new OrderItem {
					ArticleDisplayName = "Artikelnamn 2",
					ArticleDisplayNumber = "Artikelnr 9854",
					DisplayTotalPrice = 66f,
					Notes = "Applicera omedelbart",
					NumberOfItems = 3,
					NoVAT = true,
					SinglePrice = 22f,
				},
				new OrderItem {
					ArticleDisplayName = "Artikelnamn 3",
					ArticleDisplayNumber = "Artikelnr 1654",
					DisplayTotalPrice = 199.5f,
					Notes = null,
					NumberOfItems = 2,
					NoVAT = false,
					SinglePrice = 99.75f,
				}
			};
			settings = new SvefakturaConversionSettings {
				BankGiro = "123456789",
				BankgiroBankIdentificationCode = "BGABSESS",
				Postgiro = "987654321",
				PostgiroBankIdentificationCode = "PGSISESS",
				ExemptionReason = "Innehar f-skattebevis",
				InvoiceCurrencyCode = CurrencyCodeContentType.SEK,
				InvoiceExpieryPenaltySurchargePercent = 12.50m,
				InvoiceIssueDate = new DateTime(2009, 10, 28),
				InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagar netto",
				InvoiceTypeCode = "380",
				SellingOrganizationCity = "Klippan",
				SellingOrganizationContactEmail = "info@synologen.se",
				SellingOrganizationContactName = "Lotta Wieslander",
				SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE,
				SellingOrganizationFax = "0435-134 33",
				SellingOrganizationName = "Synologen AB",
				SellingOrganizationNumber = "556401-1962",
				SellingOrganizationPostalCode = "264 22",
				SellingOrganizationPostBox = "Box 111",
				SellingOrganizationStreetName = "Köpmansgården",
				SellingOrganizationTelephone = "0435-134 33",
				TaxAccountingCode = "SE556401196201",
				VATAmount = 0.25m,
			};
			company = new Company {
				StreetName = "Gatuadress 1",
				PostBox = "Postbox 123",
				BankCode = "99998",
				City = "Järfälla",
				Zip = "17588",
				Country = new Country {Id = 1, Name = "Sverige", OrganizationCountryCodeId = SwedenCountryCodeNumber},
				InvoiceCompanyName = "5440Saab AB",
				InvoiceFreeTextFormat = "{CustomerName}{CustomerPersonalIdNumber}{CompanyUnit}{CustomerPersonalBirthDateString}{CustomerFirstName}{CustomerLastName}{BuyerCompanyId}{RST}{BankCode}",
				//Name = "Saab",
				OrganizationNumber = "5560360793",
				TaxAccountingCode = "SE556036079301",
				PaymentDuePeriod = 30,
			};
			shop = new Shop {
				ContactFirstName = "Anders",
				ContactLastName = "Andersson",
				Phone = "031-987654",
				Fax = "031-987653",
				Email = "info@butiken.se",
			};
			invoice = Utility.Convert.ToSvefakturaInvoice(settings, order, orderItems, company, shop);
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
			Expect(invoice.Note.Value.Substring(57,18), Is.EqualTo("Kostnadsställe ABC"));
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
			Expect(invoice.InvoiceLine[0].Note.Value, Is.EqualTo("Applicera långsamt"));
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
		public void Test_Sets_Settings_BankGiro(){
			Expect(invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value, Is.EqualTo("123456789"));
		}
		[Test]
		public void Test_Sets_Settings_BankgiroBankIdentificationCode(){
			Expect(invoice.PaymentMeans[0].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value, Is.EqualTo("BGABSESS"));
		}
		[Test]
		public void Test_Sets_Settings_Postgiro(){
			Expect(invoice.PaymentMeans[1].PayeeFinancialAccount.ID.Value, Is.EqualTo("987654321"));
		}
		[Test]
		public void Test_Sets_Settings_PostgiroBankIdentificationCode(){
			Expect(invoice.PaymentMeans[1].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value, Is.EqualTo("PGSISESS"));
		}
		[Test]
		public void Test_Sets_Settings_ExemptionReason(){
			Expect(invoice.SellerParty.Party.PartyTaxScheme[1].ExemptionReason.Value, Is.EqualTo("Innehar f-skattebevis"));
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
			Expect(invoice.IssueDate.Value, Is.EqualTo(new DateTime(2009, 10, 28)));
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
		public void Test_Sets_Settings_SellingOrganizationCity(){
			Expect(invoice.SellerParty.Party.Address.CityName.Value, Is.EqualTo("Klippan"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationStreetName(){
			Expect(invoice.SellerParty.Party.Address.StreetName.Value, Is.EqualTo("Köpmansgården"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationPostBox(){
			Expect(invoice.SellerParty.Party.Address.Postbox.Value, Is.EqualTo("Box 111"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationCountry(){
			Expect(invoice.SellerParty.Party.Address.Country.IdentificationCode.Value, Is.EqualTo(CountryIdentificationCodeContentType.SE));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationContactName(){
			Expect(invoice.SellerParty.AccountsContact.Name.Value, Is.EqualTo("Lotta Wieslander"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationContactEmail(){
			Expect(invoice.SellerParty.AccountsContact.ElectronicMail.Value, Is.EqualTo("info@synologen.se"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationTelephone(){
			Expect(invoice.SellerParty.AccountsContact.Telephone.Value, Is.EqualTo("043513433"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationFax(){
			Expect(invoice.SellerParty.AccountsContact.Telefax.Value, Is.EqualTo("043513433"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationName(){
			Expect(invoice.SellerParty.Party.PartyName[0].Value, Is.EqualTo("Synologen AB"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationNumber(){
			Expect(invoice.SellerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo("5564011962"));
			Expect(invoice.SellerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo("5564011962"));
		}
		[Test]
		public void Test_Sets_Settings_SellingOrganizationPostalCode(){
			Expect(invoice.SellerParty.Party.Address.PostalZone.Value, Is.EqualTo("264 22"));
		}
		[Test]
		public void Test_Sets_Settings_TaxAccountingCode(){
			Expect(invoice.SellerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo("SE556401196201"));
			
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
			Expect(invoice.BuyerParty.Party.Address.CityName.Value, Is.EqualTo("Järfälla"));
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[1].RegistrationAddress.CityName.Value, Is.EqualTo("Järfälla"));
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
			Expect(invoice.Note.Value, Is.EqualTo("Adam Bertil197001015374Avdelning 12319700101AdamBertil184Kostnadsställe ABC99998"));
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
			Expect(invoice.PaymentMeans[0].DuePaymentDate.Value, Is.EqualTo(new DateTime(2009, 11, 27)));
			Expect(invoice.PaymentTerms.Note.Value.StartsWith("30"));
		}
		#endregion

		#region Shop
		[Test]
		public void Test_Sets_ShopRow_CombinedName(){
			Expect(invoice.SellerParty.Party.Contact.Name.Value, Is.EqualTo("Anders Andersson"));
		}
		[Test]
		public void Test_Sets_ShopRow_Phone(){
			Expect(invoice.SellerParty.Party.Contact.Telephone.Value, Is.EqualTo("031987654"));
		}
		[Test]
		public void Test_Sets_ShopRow_Fax(){
			Expect(invoice.SellerParty.Party.Contact.Telefax.Value, Is.EqualTo("031987653"));
		}
		[Test]
		public void Test_Sets_ShopRow_Email(){
			Expect(invoice.SellerParty.Party.Contact.ElectronicMail.Value, Is.EqualTo("info@butiken.se"));
		}

		#endregion
	}
}