using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Convert=Spinit.Wpc.Synologen.Invoicing.Convert;

namespace Spinit.Wpc.Synologen.Test.Svefaktura {
	[TestFixture]
	public class TestMockXmlSerialization : AssertionHelper {
		private SFTIInvoiceType _invoice;

		[SetUp]
		public void Setup()
		{
			var company = Factory.Factory.GetCompany();
			var shop = Factory.Factory.GetShop();
			var invoiceItems = Factory.Factory.GetOrderItems();
			var order = Factory.Factory.GetOrder(company, shop, invoiceItems);
			var settings = Factory.Factory.GetSettings();
			_invoice = General.CreateInvoiceSvefaktura(order, settings);
		}

		
		[Test]
		public void Test_MockInvoice_Is_Valid() {
			var ruleViolations = SvefakturaValidator.ValidateObject(_invoice);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_Generate_Xml_From_Object() {
			var output = ToXML(_invoice, Encoding.Default);
			output = output.Replace("Windows-1252", "utf-8");
			var expectedXml = GetExpectedXml();
			Expect(output, Is.EqualTo(expectedXml));
		}


		[Ignore]
		public void ReadXmlFileToGetValidationTextString() {
			TextReader tw = new StreamReader(@"C:\Documents and Settings\cberg\Skrivbord\fsv_test_svefaktura.xml");
			var fileContent = tw.ReadToEnd();
            tw.Close();
		}

		private static string ToXML(SFTIInvoiceType objToSerialize, Encoding encoding) {
			var namespaces = new XmlSerializerNamespaces();
			namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			namespaces.Add("udt", "urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0");
			namespaces.Add("sdt", "urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0");
			namespaces.Add("cur", "urn:oasis:names:tc:ubl:codelist:CurrencyCode:1:0");
			namespaces.Add("ccts", "urn:oasis:names:tc:ubl:CoreComponentParameters:1:0");
			namespaces.Add("cbc", "urn:oasis:names:tc:ubl:CommonBasicComponents:1:0");
			namespaces.Add("cac", "urn:sfti:CommonAggregateComponents:1:0");
			namespaces.Add(String.Empty, "urn:sfti:documents:BasicInvoice:1:0");
			//var sb = new StringBuilder();
			//var output = new StringWriter(sb) { NewLine = Environment.NewLine };
			var output = new MemoryStream();
			var serializer = new XmlSerializer(objToSerialize.GetType());
			var xmlTextWriter = new XmlTextWriter ( output, encoding );
			serializer.Serialize(xmlTextWriter, objToSerialize, namespaces);
			var memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
			return encoding.GetString(memoryStream.ToArray());
		}

		////public SFTIInvoiceType GetMockInvoice() {
		////    var shop = GetMockShop();
		////    var company = GetMockCompany();
		////    var settings = GetMockSettings();
		////    var order = GetMockOrder();
		////    var orderItems = GetMockOrderItems();
		////    return Convert.ToSvefakturaInvoice(settings, order);//, orderItems, company, shop);
		////}
		//public Shop GetMockShop() {
		//    return new Shop {
		//        ContactFirstName = "Adam",
		//        ContactLastName = "Bertil",
		//        Phone = "0811122233",
		//        Fax = "089876543",
		//        Email ="sales@modernaprodukter.se"
		//    };
		//}
		//public Company GetMockCompany() {
		//    return new Company {
		//        InvoiceFreeTextFormat = "H�r kan jag beskriva fakturan med fritext",
		//        InvoiceCompanyName = "Johnssons byggvaror",
		//        Address2 = "R�dhusgatan 5",
		//        City = "Stockholm",
		//        Zip = "11000",
		//        PaymentDuePeriod = 30
		//    };
		//}
		//public SvefakturaConversionSettings GetMockSettings() {
		//    return new SvefakturaConversionSettings {
		//        InvoiceIssueDate = new DateTime(2003,09,11),
		//        InvoiceTypeCode = "380",
		//        InvoiceCurrencyCode = CurrencyCodeContentType.SEK,
		//        SellingOrganizationName = "Moderna Produkter AB",
		//        SellingOrganizationStreetName = "Storgatan 5",
		//        SellingOrganizationCity = "H�gersten",
		//        SellingOrganizationPostalCode = "12652",
		//        ExemptionReason = "F-skattebevis finns",
		//        SellingOrganizationNumber = "5565624223",
		//        TaxAccountingCode = "SE556562422301",
		//        //SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE,
		//        SellingOrganizationContactName = "A Person, Fakturaavd",
		//        BankGiro = "9551548524585",
		//        BankgiroBankIdentificationCode = "SKIASESS",
		//        InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagars netto",
		//        InvoiceExpieryPenaltySurchargePercent = 23,
		//        VATAmount = 0.25m
		//    };
		//}
		//public Order GetMockOrder() {
		//    return new Order {
		//        InvoiceNumber = 15,
		//        CustomerFirstName = "Pelle",
		//        CustomerLastName = "Svensson",
		//        InvoiceSumIncludingVAT = 6725.00,
		//        InvoiceSumExcludingVAT = 5480.00,
		//        CustomerOrderNumber = "123456789"
		//    };
		//}
		//public List<IOrderItem> GetMockOrderItems() {
		//    return new List<IOrderItem> {
		//        new OrderItem {
		//            ArticleDisplayName = "Falu r�df�rg",
		//            NumberOfItems = 120,
		//            DisplayTotalPrice = 4980,
		//            SinglePrice = 41.50f,
		//            NoVAT = false,
		//            Notes = "Fritext p� fakturaraden",
		//            ArticleDisplayNumber = "12345"
		//        },
		//        new OrderItem {
		//            ArticleDisplayName = "Pensel 20 mm",
		//            NumberOfItems = 10,
		//            DisplayTotalPrice = 500,
		//            SinglePrice = 50,
		//            NoVAT = true,
		//            ArticleDisplayNumber = "524522"
		//        }
		//    };
		//}

		public string GetExpectedXml()
		{
			return "<?xml version=\"1.0\" encoding=\"utf-8\"?><Invoice xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:cac=\"urn:sfti:CommonAggregateComponents:1:0\" xmlns:ccts=\"urn:oasis:names:tc:ubl:CoreComponentParameters:1:0\" xmlns:cur=\"urn:oasis:names:tc:ubl:codelist:CurrencyCode:1:0\" xmlns:sdt=\"urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0\" xmlns:cbc=\"urn:oasis:names:tc:ubl:CommonBasicComponents:1:0\" xmlns:udt=\"urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0\" xmlns=\"urn:sfti:documents:BasicInvoice:1:0\"><ID>15</ID><cbc:IssueDate>2003-09-11</cbc:IssueDate><InvoiceTypeCode>380</InvoiceTypeCode><cbc:Note>H�r kan jag beskriva fakturan med fritext</cbc:Note><InvoiceCurrencyCode>SEK</InvoiceCurrencyCode><LineItemCountNumeric>2</LineItemCountNumeric><cac:BuyerParty><cac:Party><cac:PartyName><cbc:Name>Johnssons byggvaror</cbc:Name></cac:PartyName><cac:Address><cbc:StreetName>R�dhusgatan 5</cbc:StreetName><cbc:CityName>Stockholm</cbc:CityName><cbc:PostalZone>11000</cbc:PostalZone></cac:Address><cac:Contact><cbc:Name>Pelle Svensson</cbc:Name></cac:Contact></cac:Party></cac:BuyerParty><cac:SellerParty><cac:Party><cac:PartyIdentification><cac:ID>5565624223</cac:ID></cac:PartyIdentification><cac:PartyName><cbc:Name>Moderna Produkter AB</cbc:Name></cac:PartyName><cac:Address><cbc:StreetName>Storgatan 5</cbc:StreetName><cbc:CityName>H�gersten</cbc:CityName><cbc:PostalZone>12652</cbc:PostalZone><cac:Country><cac:IdentificationCode>SE</cac:IdentificationCode></cac:Country></cac:Address><cac:PartyTaxScheme><cac:CompanyID>SE556562422301</cac:CompanyID><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:PartyTaxScheme><cac:PartyTaxScheme><cac:CompanyID>5565624223</cac:CompanyID><cbc:ExemptionReason>F-skattebevis finns</cbc:ExemptionReason><cac:RegistrationAddress><cbc:StreetName>Storgatan 5</cbc:StreetName><cbc:CityName>H�gersten</cbc:CityName><cbc:PostalZone>12652</cbc:PostalZone><cac:Country><cac:IdentificationCode>SE</cac:IdentificationCode></cac:Country></cac:RegistrationAddress><cac:TaxScheme><cac:ID>SWT</cac:ID></cac:TaxScheme></cac:PartyTaxScheme><cac:Contact><cbc:Name>Adam Bertil</cbc:Name><cbc:Telephone>0811122233</cbc:Telephone><cbc:Telefax>089876543</cbc:Telefax><cbc:ElectronicMail>sales@modernaprodukter.se</cbc:ElectronicMail></cac:Contact></cac:Party><cac:AccountsContact><cbc:Name>A Person, Fakturaavd</cbc:Name></cac:AccountsContact></cac:SellerParty><cac:PaymentMeans><cac:PaymentMeansTypeCode>1</cac:PaymentMeansTypeCode><cbc:DuePaymentDate>2003-10-11</cbc:DuePaymentDate><cac:PayeeFinancialAccount><cac:ID>9551548524585</cac:ID><cac:FinancialInstitutionBranch><cac:FinancialInstitution><cac:ID>SKIASESS</cac:ID></cac:FinancialInstitution></cac:FinancialInstitutionBranch></cac:PayeeFinancialAccount></cac:PaymentMeans><cac:PaymentTerms><cbc:Note>30 dagars netto</cbc:Note><cbc:PenaltySurchargePercent>23</cbc:PenaltySurchargePercent></cac:PaymentTerms><cac:TaxTotal><cbc:TotalTaxAmount amountCurrencyID=\"SEK\">1245</cbc:TotalTaxAmount><cac:TaxSubTotal><cbc:TaxableAmount amountCurrencyID=\"SEK\">6225.00</cbc:TaxableAmount><cbc:TaxAmount amountCurrencyID=\"SEK\">1245</cbc:TaxAmount><cac:TaxCategory><cac:ID>S</cac:ID><cbc:Percent>25.00</cbc:Percent><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory></cac:TaxSubTotal><cac:TaxSubTotal><cbc:TaxableAmount amountCurrencyID=\"SEK\">500</cbc:TaxableAmount><cbc:TaxAmount amountCurrencyID=\"SEK\">0.00</cbc:TaxAmount><cac:TaxCategory><cac:ID>E</cac:ID><cbc:Percent>0.00</cbc:Percent><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory></cac:TaxSubTotal></cac:TaxTotal><cac:LegalTotal><cbc:LineExtensionTotalAmount amountCurrencyID=\"SEK\">5480</cbc:LineExtensionTotalAmount><cbc:TaxExclusiveTotalAmount amountCurrencyID=\"SEK\">5480</cbc:TaxExclusiveTotalAmount><cbc:TaxInclusiveTotalAmount amountCurrencyID=\"SEK\">6725</cbc:TaxInclusiveTotalAmount></cac:LegalTotal><cac:InvoiceLine><cac:ID>1</cac:ID><cbc:InvoicedQuantity quantityUnitCode=\"styck\">120</cbc:InvoicedQuantity><cbc:LineExtensionAmount amountCurrencyID=\"SEK\">4980</cbc:LineExtensionAmount><cbc:Note>Fritext p� fakturaraden</cbc:Note><cac:Item><cbc:Description>Falu r�df�rg</cbc:Description><cac:SellersItemIdentification><cac:ID>12345</cac:ID></cac:SellersItemIdentification><cac:TaxCategory><cac:ID>S</cac:ID><cbc:Percent>25.00</cbc:Percent><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory><cac:BasePrice><cbc:PriceAmount amountCurrencyID=\"SEK\">41.5</cbc:PriceAmount></cac:BasePrice></cac:Item></cac:InvoiceLine><cac:InvoiceLine><cac:ID>2</cac:ID><cbc:InvoicedQuantity quantityUnitCode=\"styck\">10</cbc:InvoicedQuantity><cbc:LineExtensionAmount amountCurrencyID=\"SEK\">500</cbc:LineExtensionAmount><cac:Item><cbc:Description>Pensel 20 mm</cbc:Description><cac:SellersItemIdentification><cac:ID>524522</cac:ID></cac:SellersItemIdentification><cac:TaxCategory><cac:ID>E</cac:ID><cbc:Percent>0</cbc:Percent><cac:TaxScheme><cac:ID>VAT</cac:ID></cac:TaxScheme></cac:TaxCategory><cac:BasePrice><cbc:PriceAmount amountCurrencyID=\"SEK\">50</cbc:PriceAmount></cac:BasePrice></cac:Item></cac:InvoiceLine><RequisitionistDocumentReference><cac:ID>123456789</cac:ID></RequisitionistDocumentReference></Invoice>";
		}

	}
}