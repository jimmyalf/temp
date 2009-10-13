using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura {
	[TestFixture]
	public class TestXmlSerialization {

		[TestFixtureSetUp]
		public void Setup() {}

		[Test]
		public void Test_Generate_Xml_From_Object() {
			var invoice = GetMockInvoice();
			var output = ToXML(invoice);
			Assert.IsNotNull(output);
			Assert.Greater(output.Length,0);
			Debug.WriteLine(output);
		}

		//[Test]
		//public void Test_Read_Xml_Into_Invoice_Object() {
		//    var invoice = GetMockInvoice();
		//    var output = ToXML(invoice);
		//    var readInvoice = ToInvoice(output);
		//    Assert.AreEqual(invoice, readInvoice);
		//}

		private static string ToXML(SFTIInvoiceType objToSerialize) {
			XmlSerializer serializer;
			try {
				var namespaces = new XmlSerializerNamespaces();
				namespaces.Add("cac", "urn:sfti:CommonAggregateComponents:1:0");
				//namespaces.Add("cac", "urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0");
				namespaces.Add("cbc", "urn:oasis:names:tc:ubl:CommonBasicComponents:1:0");
				var sb = new StringBuilder();
				//var test = new CultureInfo("sv-SE") {
				//    NumberFormat = new NumberFormatInfo {
				//        NumberDecimalDigits = 10,
				//        NumberDecimalSeparator = "¤",
				//        CurrencyDecimalDigits=10,
				//        CurrencyDecimalSeparator="#",
				//        PercentDecimalDigits=10,
				//        PercentDecimalSeparator="%%%%"
				//    }
				//};
				//var output = new StringWriter(sb, test) { NewLine = Environment.NewLine };
				var output = new StringWriter(sb) { NewLine = Environment.NewLine };
				serializer = new XmlSerializer(objToSerialize.GetType());
				serializer.Serialize(output, objToSerialize, namespaces);
				return output.ToString();
			}
			catch (Exception ex) {
				var text = ex.Message;
				return null;
			}
		}

		private static SFTIInvoiceType ToInvoice(string xml) {
			try{
				var obj = new SFTIInvoiceType();
				var serializer = new XmlSerializer(obj.GetType());
				var reader = new StringReader(xml);
				obj = (SFTIInvoiceType)(serializer.Deserialize(reader));
				return obj;
			}
			catch { return null; }
		}

		#region Mock Data Generation

		//private static CommonInvoice GetMockInvoice() {
		//    return new CommonInvoice {
		//        AmountToBePayed = 150,
		//        Buyer = new Party {
		//            Contact = "Adam Bertil",
		//            DeliveryAddress = GetMockAddress(),
		//            InvoiceAddress = GetMockAddress(),
		//            OrganizationName = "SAAB Technologies",
		//            OrganizationNumber = "555 BOMB",
		//            TaxAccountingNumber = "78936541"
		//        },
		//        BuyerCostCenter = null,
		//        BuyerOrderNumber = "88-55",
		//        CreditForInvoiceNumber = null,
		//        Currency = "SEK",
		//        InoviceCreated = DateTime.Now,
		//        InvoiceExpiryDate = DateTime.Now.AddDays(30),
		//        InvoiceNumber = 555,
		//        InvoiceRows = GetMockInvoiceRows(),
		//        InvoiceType = Invoice.Enumerations.InvoiceType.Test,
		//        Seller = GetMockSynologenParty(),
		//        PaymentAccountNumber = null,
		//        TotalAmountExcludingVAT = 600,
		//        TotalAmountIncludingVAT = 720,
		//        TotalAmountVAT = 120,
		//        VAT = 0.25F,
		//        VATSpecification = null,
		//        VendorOrderNumber = null
		//    };
		//}

		//private static Collection<InvoiceRow> GetMockInvoiceRows() {
		//    var returnList = new Collection<InvoiceRow>();
		//    returnList.Add(
		//        new InvoiceRow {
		//            ArticleDescription = "Hylo-COMOD används till lugnande lindring av torra eller rinnande ögon. Hylo-COMOD innehåller en låg dos hyaluronsyra som återfuktar ögat.",
		//            ArticleName = "Hylo-COMOD",
		//            ArticleNumber = "123456",
		//            FreeTextRows = null,
		//            NoVAT=false,
		//            Quantity=1,
		//            SinglePriceExcludingVAT = 60,
		//            TotalRowAmountExcludingVAT = 60,
		//            UseRowAsFreeTextRow = false
		//        }
		//    );
		//    returnList.Add(
		//        new InvoiceRow {
		//            FreeTextRows = new Collection<string>{"Fritext rad ett.","Fritext rad två."},
		//        }
		//    );
		//    returnList.Add(
		//        new InvoiceRow {
		//            ArticleDescription = "Lacryvisc är en ögondroppe med geléaktig konsistens och är framtagen speciellt för dig som har svårare problem med torra ögon. Denna gel i droppform innehåller inget konserveringsmedel och ger en långvarigt fuktande och smörjande verkan. Lacryvisc fungerar även bra för nattbruk.",
		//            ArticleName = "Lacryvisc",
		//            ArticleNumber = "987654",
		//            FreeTextRows = null,
		//            NoVAT = false,
		//            Quantity = 2,
		//            SinglePriceExcludingVAT = 36,
		//            TotalRowAmountExcludingVAT = 72,
		//            UseRowAsFreeTextRow = false
		//        }
		//    );
		//    return returnList;
		//}

		//private static Address GetMockAddress() {
		//    return new Address {
		//        PostBox = "Vägen 1", StreetName = null, City = "Göteborg", Country = null, Zip = "436 32"
		//    } ;
		//}

		//private static Party GetMockSynologenParty() {
		//    return new Party {
		//        DeliveryAddress = new Address {
		//            PostBox = "Synhälsan Svenska Aktiebolag", 
		//            StreetName = "Box 111", 
		//            City = "Klippan", 
		//            Zip = "264 22"
		//        },
		//        OrganizationName="Synhälsan Svenska Aktiebolag",
		//        OrganizationNumber="556262-6100",
		//        TaxAccountingNumber = "556262-6100"
		//    };
		//}


		//private static Invoice GetMockInvoice() {
		private const string OrganizationNumber = "556262-6100";
		private const string OrganizationName = "Synhälsan Svenska Aktiebolag";
		private const string OrganizationAddress = "Box 111";
		private const string OrganizationZip = "264 22";
		private const string OrganizationCity = "Klippan";
		private const string InvoiceCurrencyCode = "SEK";
		private const string InvoiceDefaultQuantityName = "styck";
		public SFTIInvoiceType GetMockInvoice() {
			var invoice = new SFTIInvoiceType();

			invoice.SellerParty = new SFTISellerPartyType{Party = new SFTIPartyType()};
			invoice.SellerParty.Party.PartyIdentification = new List<SFTIPartyIdentificationType> {GetPartyIdentification(OrganizationNumber)};
			invoice.SellerParty.Party.PartyName = new List<NameType> {new NameType {Value = OrganizationName}};
			invoice.SellerParty.Party.PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {GetPartyTaxScheme(OrganizationNumber)};
			invoice.SellerParty.Party.Address = new SFTIAddressType {
				Postbox = new PostboxType { Value = OrganizationAddress },
				PostalZone = new ZoneType { Value = OrganizationZip },
				CityName = new CityNameType { Value = OrganizationCity }
			};

			invoice.BuyerParty = new SFTIBuyerPartyType { Party = new SFTIPartyType() };
			invoice.BuyerParty.Party.PartyIdentification = new List<SFTIPartyIdentificationType> {GetPartyIdentification("orgNr")};
			invoice.BuyerParty.Party.PartyName = new List<NameType>{new NameType {Value = "orgName"}};
			invoice.BuyerParty.Party.PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {GetPartyTaxScheme("orgNr")};
			invoice.BuyerParty.Party.Address = new SFTIAddressType {
				Postbox = new PostboxType { Value = "Address" },
				PostalZone = new ZoneType { Value = "zip" },
				CityName = new CityNameType { Value = "city" }
			};

			invoice.InvoiceTypeCode = new CodeType { Value = "380" }; //? 380=faktura, 381=kredit}
			invoice.ID = new SFTISimpleIdentifierType { Value = "invoiceNumber" };
			invoice.IssueDate = new IssueDateType { Value = DateTime.Now }; //Replace with invoice Date}}
			invoice.PaymentMeans = new List<SFTIPaymentMeansType> {
			                                                      	GetDuePaymentDate(DateTime.Now),
			                                                      	new SFTIPaymentMeansType { PayeeFinancialAccount = new SFTIFinancialAccountType { ID = GetIdentifier("5693-6677".Trim('-')) } }
			}; //Replace with Invoice expiery date}
			invoice.RequisitionistDocumentReference = new List<SFTIDocumentReferenceType> {new SFTIDocumentReferenceType {ID = GetIdentifier("purchaseOrderNumber")}};
			invoice.Note = new NoteType { Value = "Reference to original invoice at credit invoices" };
			invoice.LegalTotal = new SFTILegalTotalType {
				TaxExclusiveTotalAmount = new TotalAmountType { amountCurrencyID = InvoiceCurrencyCode, Value = 40 },
				TaxInclusiveTotalAmount = new TotalAmountType { amountCurrencyID = InvoiceCurrencyCode, Value = 48 },
				LineExtensionTotalAmount = new ExtensionTotalAmountType { amountCurrencyID = InvoiceCurrencyCode, Value = 48 }
			};
			invoice.TaxTotal = new List<SFTITaxTotalType> {new SFTITaxTotalType {TotalTaxAmount = new TaxAmountType {amountCurrencyID = "SEK", Value = 8}}};
			invoice.TaxTotal[0].TaxSubTotal = new List<SFTITaxSubTotalType>{new SFTITaxSubTotalType { TaxCategory =  new SFTITaxCategoryType { ID = GetIdentifier("S"), Percent = new  PercentType {Value = 0.25m} }} };

			var invoiceLine1 = GetInvoiceLine("Bröd", null, "skålpund", 2, 10, 20);
			var invoiceLine2 = GetInvoiceLine("Vete", null, "skålpund", 2, 10, 20);
			invoice.InvoiceLine = new List<SFTIInvoiceLineType> { invoiceLine1, invoiceLine2 };
			invoice.LineItemCountNumeric = new LineItemCountNumericType {Value = invoice.InvoiceLine.Count};
			return invoice;
		}

		public IdentifierType GetIdentifier(string id) {
			return String.IsNullOrEmpty(id) ? null : new IdentifierType { Value = id };
		}


		public SFTIPartyTaxSchemeType GetPartyTaxScheme(string orgNr) {
			if (String.IsNullOrEmpty(orgNr)) return null;
			return new SFTIPartyTaxSchemeType {
				CompanyID = new IdentifierType { Value = OrganizationNumber },
				TaxScheme = new SFTITaxSchemeType {
					ID = new IdentifierType { Value = "VAT" }
				}
			};
		}

		public SFTIPartyIdentificationType GetPartyIdentification(string orgNr) {
			return String.IsNullOrEmpty(orgNr) ? null : new SFTIPartyIdentificationType { ID = GetIdentifier(orgNr) };
		}

		public SFTIPaymentMeansType GetDuePaymentDate(DateTime value) {
			return new SFTIPaymentMeansType {DuePaymentDate = new PaymentDateType {Value = value}};
		}

		public SFTIInvoiceLineType GetInvoiceLine(string articleName, string articleNumber, string quantityUnitCode, Decimal quantity, Decimal singlePriceNoVat, Decimal rowTotalNoVAT) {
			var sellersItemIdentification = String.IsNullOrEmpty(articleNumber) ? null : new SFTIItemIdentificationType {ID = GetIdentifier(articleNumber)};
			var invoiceLine = new SFTIInvoiceLineType {
				Item = new SFTIItemType {
					Description = new DescriptionType { Value = articleName },
					SellersItemIdentification = sellersItemIdentification,
					BasePrice = new SFTIBasePriceType {
						PriceAmount = new PriceAmountType {
							amountCurrencyID = InvoiceCurrencyCode,
							Value = singlePriceNoVat
						}
					}
				},
				InvoicedQuantity = new QuantityType {
					quantityUnitCode = String.IsNullOrEmpty(quantityUnitCode) ? InvoiceDefaultQuantityName : quantityUnitCode,
					Value = quantity
				},
				LineExtensionAmount = new ExtensionAmountType {
					amountCurrencyID = InvoiceCurrencyCode,
					Value = rowTotalNoVAT
				}
			};
			return invoiceLine;
		}

		#endregion
	}
}