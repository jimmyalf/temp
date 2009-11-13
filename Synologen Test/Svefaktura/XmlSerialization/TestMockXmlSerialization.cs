using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.XmlSerialization {
	[TestFixture]
	public class TestMockXmlSerialization : AssertionHelper {
		private const int SwedenCountryCodeNumber = 187;

		[TestFixtureSetUp]
		public void Setup() {}

		
		[Test, Explicit]
		public void Test_MockInvoice_Is_Valid() {
			var invoice = GetMockInvoice();
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_Generate_Xml_From_Object() {
			var invoice = GetMockInvoice();
			var output = ToXML(invoice, Encoding.Default);
			output = output.Replace("Windows-1252", "utf-8");
			var expectedXml = GetExpectedXml();
			Expect(output, Is.EqualTo(expectedXml));
		}
		[Test]
		public void Test_Xml_Output_From_Object() {
			var invoice = GetMockInvoice();
			var output = GetFormattedXmlString(invoice);
			Debug.WriteLine(output);
			Expect(output, Is.Not.Null);
		}

		[Test, Explicit]
		public void Test_Generate_Xml_From_Object_And_Write_To_File() {
			var invoice = GetMockInvoice();
			ToXML(invoice, @"C:\Documents and Settings\cberg\Skrivbord\Test-Svefaktura.xml");
		}

		[Test, Explicit]
		public void ReadXmlFileToGetValidationTextString() {
			TextReader tw = new StreamReader(@"C:\Documents and Settings\cberg\Skrivbord\fsv_test_svefaktura.xml");
			var fileContent = tw.ReadToEnd();
			tw.Close();
		}

		private static string ToXML(SFTIInvoiceType objToSerialize, Encoding encoding) {
			var namespaces = GetNamespaces();
			var output = new MemoryStream();
			var serializer = new XmlSerializer(objToSerialize.GetType());
			var xmlTextWriter = new XmlTextWriter ( output, encoding );
			serializer.Serialize(xmlTextWriter, objToSerialize, namespaces);
			var memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
			return encoding.GetString(memoryStream.ToArray());
		}
		private static void ToXML(SFTIInvoiceType objToSerialize, string filePath) {
			var namespaces = GetNamespaces();
			var settings = new XmlWriterSettings {
				Encoding = Encoding.UTF8, 
				Indent = true, 
				IndentChars = "\t", 
				NewLineChars = Environment.NewLine, 
				ConformanceLevel = ConformanceLevel.Document,
			};
			TextWriter textWriter = new StreamWriter(filePath, false, Encoding.UTF8);
			var serializer = new XmlSerializer(objToSerialize.GetType());
			using(var writer = XmlWriter.Create(textWriter, settings)){
				if(writer == null) return;
				serializer.Serialize(writer, objToSerialize, namespaces);
				writer.Close();
			}
		}

		private static string GetFormattedXmlString(SFTIInvoiceType input){
			var xmlSerializer = new XmlSerializer(input.GetType());
			var output = new StringWriterWithEncoding(new StringBuilder(), Encoding.UTF8) { NewLine = Environment.NewLine};
			xmlSerializer.Serialize(output, input,  GetNamespaces());
			return output.ToString();
		}
		private static XmlSerializerNamespaces GetNamespaces(){
			var namespaces = new XmlSerializerNamespaces();
			namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			namespaces.Add("udt", "urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0");
			namespaces.Add("sdt", "urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0");
			namespaces.Add("cur", "urn:oasis:names:tc:ubl:codelist:CurrencyCode:1:0");
			namespaces.Add("ccts", "urn:oasis:names:tc:ubl:CoreComponentParameters:1:0");
			namespaces.Add("cbc", "urn:oasis:names:tc:ubl:CommonBasicComponents:1:0");
			namespaces.Add("cac", "urn:sfti:CommonAggregateComponents:1:0");
			namespaces.Add(String.Empty, "urn:sfti:documents:BasicInvoice:1:0");
			return namespaces;
		}


		#region Get Mock Data
		public SFTIInvoiceType GetMockInvoice() {
			var settings = GetMockSettings();
			var order = GetMockOrder();
			order.ContractCompany = GetMockCompany();
			order.SellingShop = GetMockShop();
			order.OrderItems = GetMockOrderItems();
			return Utility.Convert.ToSvefakturaInvoice(settings, order);
		}
		public Shop GetMockShop() {
			return new Shop {
				ContactFirstName = "Adam",
				ContactLastName = "Bertil",
				Phone = "0811122233",
				Fax = "089876543",
				Email = "sales@modernaprodukter.se"
			};
		}
		public Company GetMockCompany() {
			return new Company {
				InvoiceFreeTextFormat =
					"Kundens namn: {CustomerName}\r\n"
					+ "Kundens förnamn: {CustomerFirstName}\r\n"
					+ "Kundens efternamn: {CustomerLastName}\r\n"
					+ "Kundens personnummer: {CustomerPersonalIdNumber}\r\n"
					+ "Kundens födelsedag: {CustomerPersonalBirthDateString}\r\n"
					+ "Kundens företagsenhet: {CompanyUnit}\r\n"
					+ "Kundens konstnadsställe: {RST}\r\n"
					+ "Kundens bankkod: {BankCode}\r\n"
					+ "Företagsid: {BuyerCompanyId}",
				InvoiceCompanyName = "Johnssons byggvaror",
				StreetName = "Rådhusgatan 5",
				City = "Stockholm",
				PostBox = "Box 123",
				Zip = "11000",
				PaymentDuePeriod = 30,
				Country = new Country {OrganizationCountryCodeId = SwedenCountryCodeNumber},
				BankCode = "99998",
				OrganizationNumber = "555123456",
				TaxAccountingCode = "SE555123456",
			};
		}
		public SvefakturaConversionSettings GetMockSettings() {
			return new SvefakturaConversionSettings {
				InvoiceIssueDate = new DateTime(2003, 09, 11),
				InvoiceTypeCode = "380",
				InvoiceCurrencyCode = CurrencyCodeContentType.SEK,
				SellingOrganizationName = "Moderna Produkter AB",
				SellingOrganizationStreetName = "Storgatan 5",
				SellingOrganizationCity = "Hägersten",
				SellingOrganizationPostalCode = "12652",
				ExemptionReason = "F-skattebevis finns",
				SellingOrganizationNumber = "5565624223",
				TaxAccountingCode = "SE556562422301",
				SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE,
				SellingOrganizationContactName = "A Person, Fakturaavd",
				BankGiro = "9551548524585",
				BankgiroBankIdentificationCode = "SKIASESS",
				InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagars netto",
				InvoiceExpieryPenaltySurchargePercent = 23,
				VATAmount = 0.25m,
				Postgiro = "123456789",
				PostgiroBankIdentificationCode = "PGSISESS",
				SellingOrganizationContactEmail = "info@synologen.se",
				SellingOrganizationFax = "0123-456789",
				SellingOrganizationPostBox = "Box 789",
				SellingOrganizationTelephone = "0123-567890"
			};
		}
		public Order GetMockOrder() {
			return new Order {
				InvoiceNumber = 15,
				CustomerFirstName = "Pelle",
				CustomerLastName = "Svensson",
				InvoiceSumIncludingVAT = 6725.00,
				InvoiceSumExcludingVAT = 5480.00,
				CustomerOrderNumber = "123456789",
				CompanyUnit = "Företagsenhet",
				Email = "pelle.svensson@inkop.se",
				PersonalIdNumber = "197001015374",
				Phone = "08-987654",
				RstText = "Kostnadsställe ABCD",
				CompanyId = 987
			};
		}
		public List<OrderItem> GetMockOrderItems() {
			return new List<OrderItem> {
				new OrderItem {
					ArticleDisplayName = "Falu rödfärg",
					NumberOfItems = 120,
					DisplayTotalPrice = 4980,
					SinglePrice = 41.50f,
					NoVAT = false,
					Notes = "Fritext på fakturaraden",
					ArticleDisplayNumber = "12345"
				},
				new OrderItem {
					ArticleDisplayName = "Pensel 20 mm",
					NumberOfItems = 10,
					DisplayTotalPrice = 500,
					SinglePrice = 50,
					NoVAT = true,
					ArticleDisplayNumber = "524522"
				}
			};
		}
		#endregion

		public string GetExpectedXml() {
			return 
				 "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
				+"<Invoice xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:cac=\"urn:sfti:CommonAggregateComponents:1:0\" xmlns:ccts=\"urn:oasis:names:tc:ubl:CoreComponentParameters:1:0\" xmlns:cur=\"urn:oasis:names:tc:ubl:codelist:CurrencyCode:1:0\" xmlns:sdt=\"urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0\" xmlns:cbc=\"urn:oasis:names:tc:ubl:CommonBasicComponents:1:0\" xmlns:udt=\"urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0\" xmlns=\"urn:sfti:documents:BasicInvoice:1:0\">"
					+"<ID>15</ID>"
					+"<cbc:IssueDate>2003-09-11</cbc:IssueDate>"
					+"<InvoiceTypeCode>380</InvoiceTypeCode>"
					+"<cbc:Note>"
						+"Kundens namn: Pelle Svensson\r\n"
						+"Kundens förnamn: Pelle\r\n"
						+"Kundens efternamn: Svensson\r\n"
						+"Kundens personnummer: 197001015374\r\n"
						+"Kundens födelsedag: 19700101\r\n"
						+"Kundens företagsenhet: Företagsenhet\r\n"
						+"Kundens konstnadsställe: Kostnadsställe ABCD\r\n"
						+"Kundens bankkod: 99998\r\n"
						+"Företagsid: 987"
					+"</cbc:Note>"
					+"<InvoiceCurrencyCode>SEK</InvoiceCurrencyCode>"
					//+"<TaxCurrencyCode>SEK</TaxCurrencyCode>"
					+"<LineItemCountNumeric>2</LineItemCountNumeric>"
					+"<cac:BuyerParty>"
						+"<cac:Party>"
							+"<cac:PartyIdentification><cac:ID>555123456</cac:ID></cac:PartyIdentification>"
							+"<cac:PartyName><cbc:Name>Johnssons byggvaror</cbc:Name></cac:PartyName>"
							+"<cac:Address><cbc:Postbox>Box 123</cbc:Postbox><cbc:StreetName>Rådhusgatan 5</cbc:StreetName><cbc:Department>Företagsenhet</cbc:Department><cbc:CityName>Stockholm</cbc:CityName><cbc:PostalZone>11000</cbc:PostalZone><cac:Country><cac:IdentificationCode>SE</cac:IdentificationCode></cac:Country></cac:Address>"
							+"<cac:PartyTaxScheme><cac:CompanyID>SE555123456</cac:CompanyID><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:PartyTaxScheme><cac:PartyTaxScheme><cac:CompanyID>555123456</cac:CompanyID><cac:RegistrationAddress><cbc:Postbox>Box 123</cbc:Postbox><cbc:StreetName>Rådhusgatan 5</cbc:StreetName><cbc:CityName>Stockholm</cbc:CityName><cbc:PostalZone>11000</cbc:PostalZone><cac:Country><cac:IdentificationCode>SE</cac:IdentificationCode></cac:Country></cac:RegistrationAddress><cac:TaxScheme><cac:ID>SWT</cac:ID></cac:TaxScheme></cac:PartyTaxScheme>"
							+"<cac:Contact><cbc:Name>Pelle Svensson</cbc:Name><cbc:Telephone>08987654</cbc:Telephone><cbc:ElectronicMail>pelle.svensson@inkop.se</cbc:ElectronicMail></cac:Contact>"
						+"</cac:Party>"
					+"</cac:BuyerParty>"
					+"<cac:SellerParty>"
						+"<cac:Party>"
							+"<cac:PartyIdentification><cac:ID>5565624223</cac:ID></cac:PartyIdentification>"
							+"<cac:PartyName><cbc:Name>Moderna Produkter AB</cbc:Name></cac:PartyName>"
							+"<cac:Address><cbc:Postbox>Box 789</cbc:Postbox><cbc:StreetName>Storgatan 5</cbc:StreetName><cbc:CityName>Hägersten</cbc:CityName><cbc:PostalZone>12652</cbc:PostalZone><cac:Country><cac:IdentificationCode>SE</cac:IdentificationCode></cac:Country></cac:Address>"
							+"<cac:PartyTaxScheme><cac:CompanyID>SE556562422301</cac:CompanyID><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:PartyTaxScheme><cac:PartyTaxScheme><cac:CompanyID>5565624223</cac:CompanyID><cbc:ExemptionReason>F-skattebevis finns</cbc:ExemptionReason><cac:RegistrationAddress><cbc:Postbox>Box 789</cbc:Postbox><cbc:StreetName>Storgatan 5</cbc:StreetName><cbc:CityName>Hägersten</cbc:CityName><cbc:PostalZone>12652</cbc:PostalZone><cac:Country><cac:IdentificationCode>SE</cac:IdentificationCode></cac:Country></cac:RegistrationAddress><cac:TaxScheme><cac:ID>SWT</cac:ID></cac:TaxScheme></cac:PartyTaxScheme>"
							+"<cac:Contact><cbc:Name>Adam Bertil</cbc:Name><cbc:Telephone>0811122233</cbc:Telephone><cbc:Telefax>089876543</cbc:Telefax><cbc:ElectronicMail>sales@modernaprodukter.se</cbc:ElectronicMail></cac:Contact>"
						+"</cac:Party>"
						+"<cac:AccountsContact><cbc:Name>A Person, Fakturaavd</cbc:Name><cbc:Telephone>0123567890</cbc:Telephone><cbc:Telefax>0123456789</cbc:Telefax><cbc:ElectronicMail>info@synologen.se</cbc:ElectronicMail></cac:AccountsContact>"
					+"</cac:SellerParty>"
					+"<cac:PaymentMeans><cac:PaymentMeansTypeCode>1</cac:PaymentMeansTypeCode><cbc:DuePaymentDate>2003-10-11</cbc:DuePaymentDate><cac:PayeeFinancialAccount><cac:ID>9551548524585</cac:ID><cac:FinancialInstitutionBranch><cac:FinancialInstitution><cac:ID>SKIASESS</cac:ID></cac:FinancialInstitution></cac:FinancialInstitutionBranch></cac:PayeeFinancialAccount></cac:PaymentMeans>"
					+"<cac:PaymentMeans><cac:PaymentMeansTypeCode>1</cac:PaymentMeansTypeCode><cbc:DuePaymentDate>2003-10-11</cbc:DuePaymentDate><cac:PayeeFinancialAccount><cac:ID>123456789</cac:ID><cac:FinancialInstitutionBranch><cac:FinancialInstitution><cac:ID>PGSISESS</cac:ID></cac:FinancialInstitution></cac:FinancialInstitutionBranch></cac:PayeeFinancialAccount></cac:PaymentMeans>"
					+"<cac:PaymentTerms><cbc:Note>30 dagars netto</cbc:Note><cbc:PenaltySurchargePercent>23</cbc:PenaltySurchargePercent></cac:PaymentTerms>"
					+"<cac:TaxTotal><cbc:TotalTaxAmount amountCurrencyID=\"SEK\">1245.00</cbc:TotalTaxAmount><cac:TaxSubTotal><cbc:TaxableAmount amountCurrencyID=\"SEK\">4980</cbc:TaxableAmount><cbc:TaxAmount amountCurrencyID=\"SEK\">1245.00</cbc:TaxAmount><cac:TaxCategory><cac:ID>S</cac:ID><cbc:Percent>25.00</cbc:Percent><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory></cac:TaxSubTotal><cac:TaxSubTotal><cbc:TaxableAmount amountCurrencyID=\"SEK\">500</cbc:TaxableAmount><cbc:TaxAmount amountCurrencyID=\"SEK\">0</cbc:TaxAmount><cac:TaxCategory><cac:ID>E</cac:ID><cbc:Percent>0</cbc:Percent><cbc:ExemptionReason>Momsfri artikel</cbc:ExemptionReason><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory></cac:TaxSubTotal></cac:TaxTotal>"
					+"<cac:LegalTotal><cbc:LineExtensionTotalAmount amountCurrencyID=\"SEK\">5480</cbc:LineExtensionTotalAmount><cbc:TaxExclusiveTotalAmount amountCurrencyID=\"SEK\">5480</cbc:TaxExclusiveTotalAmount><cbc:TaxInclusiveTotalAmount amountCurrencyID=\"SEK\">6725</cbc:TaxInclusiveTotalAmount></cac:LegalTotal>"
					+"<cac:InvoiceLine>"
						+"<cac:ID>1</cac:ID>"
						+"<cbc:InvoicedQuantity quantityUnitCode=\"styck\">120</cbc:InvoicedQuantity>"
						+"<cbc:LineExtensionAmount amountCurrencyID=\"SEK\">4980</cbc:LineExtensionAmount>"
						+"<cbc:Note>Fritext på fakturaraden</cbc:Note>"
						+"<cac:Item>"
							+"<cbc:Description>Falu rödfärg</cbc:Description>"
							+"<cac:SellersItemIdentification><cac:ID>12345</cac:ID></cac:SellersItemIdentification>"
							+"<cac:TaxCategory><cac:ID>S</cac:ID><cbc:Percent>25.00</cbc:Percent><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory>"
							+"<cac:BasePrice><cbc:PriceAmount amountCurrencyID=\"SEK\">41.5</cbc:PriceAmount></cac:BasePrice>"
						+"</cac:Item>"
					+"</cac:InvoiceLine>"
					+"<cac:InvoiceLine><cac:ID>2</cac:ID><cbc:InvoicedQuantity quantityUnitCode=\"styck\">10</cbc:InvoicedQuantity><cbc:LineExtensionAmount amountCurrencyID=\"SEK\">500</cbc:LineExtensionAmount><cac:Item><cbc:Description>Pensel 20 mm</cbc:Description><cac:SellersItemIdentification><cac:ID>524522</cac:ID></cac:SellersItemIdentification><cac:TaxCategory><cac:ID>E</cac:ID><cbc:Percent>0</cbc:Percent><cbc:ExemptionReason>Momsfri artikel</cbc:ExemptionReason><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory><cac:BasePrice><cbc:PriceAmount amountCurrencyID=\"SEK\">50</cbc:PriceAmount></cac:BasePrice></cac:Item></cac:InvoiceLine>"
					+"<RequisitionistDocumentReference><cac:ID>123456789</cac:ID></RequisitionistDocumentReference>"
				+"</Invoice>";
		}

	}
}