using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.DataParsing{
	[TestFixture]
	public class TestInvoiceParsing {
		private readonly Company emptyCompany = new Company{InvoiceFreeTextFormat = ""};
		private readonly Shop emptyShop = new Shop();
		private readonly IList<OrderItem> emptyOrderItems = new List<OrderItem>();
		private readonly Order emptyOrder = new Order{ ContractCompany = new Company{ InvoiceFreeTextFormat = ""},  SellingShop = new Shop(),  OrderItems = new List<OrderItem>()};
		private readonly SvefakturaConversionSettings emptySettings = new SvefakturaConversionSettings();
		private const int SwedenCountryCodeNumber = 187;
		[TestFixtureSetUp]
		public void Setup() { }

		[Test]
		public void Test_Create_Invoice_Parameter_Checks_For_Null_And_Throws_Exceptions() {
			Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(null, emptySettings));
			Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(emptyOrder,null));
			Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(null, null));
		}

		#region General Invoice
		[Test]
		public void Test_Create_Invoice_Sets_ID() {
			var customOrder = new Order { InvoiceNumber = 123456, ContractCompany = emptyCompany, SellingShop = emptyShop, OrderItems = emptyOrderItems};
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("123456", invoice.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_RequisitionistDocumentReference() {
			var customOrder = new Order { CustomerOrderNumber = "123456", ContractCompany = emptyCompany, SellingShop = emptyShop, OrderItems = emptyOrderItems};
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("123456", invoice.RequisitionistDocumentReference[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_IssueDate() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual(new DateTime(2009, 10, 30), invoice.IssueDate.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceTypeCode() {
			var customSettings = new SvefakturaConversionSettings { InvoiceTypeCode = "380" };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("380", invoice.InvoiceTypeCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceCurrencyCode() {
			var customSettings = new SvefakturaConversionSettings { InvoiceCurrencyCode = CurrencyCodeContentType.SEK };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual(CurrencyCodeContentType.SEK, invoice.InvoiceCurrencyCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_LineItemCountNumeric() {
			var customOrder = new Order{
			                           	ContractCompany = emptyCompany, 
			                           	SellingShop = emptyShop,
			                           	OrderItems = new List<OrderItem> {new OrderItem {ArticleDisplayName = "One"}, new OrderItem {ArticleDisplayName = "Two"}}
			                           };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual(2m, invoice.LineItemCountNumeric.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Note() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, 
			                           	OrderItems = emptyOrderItems,
			                           	ContractCompany = new Company{InvoiceFreeTextFormat = "Invoice free text"}
			                           };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("Invoice free text", invoice.Note.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Parsed_Note() {
			var customShop = new Shop {Name = "Synbutiken AB", Number = "9001"};
			var customOrder = new Order {
			                            	CustomerFirstName = "Adam",
			                            	CustomerLastName = "Bertil",
			                            	PersonalIdNumber = "197001015374",
			                            	CompanyUnit = "Avdelning 1234",
			                            	CompanyId = 123,
			                            	RstText="ABCDEFGH",
			                            	SellingShop = customShop,
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany =  new Company{InvoiceFreeTextFormat = "{CustomerName}{CustomerPersonalIdNumber}{CompanyUnit}{CustomerPersonalBirthDateString}{CustomerFirstName}{CustomerLastName}{BuyerCompanyId}{RST}{SellingShopName}{SellingShopNumber}"}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("Adam Bertil197001015374Avdelning 123419700101AdamBertil123ABCDEFGHSynbutiken AB9001", invoice.Note.Value);
		}
		#endregion

		#region BuyerParty
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_PostBox() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                           	ContractCompany = new Company {PostBox = "Box 7774", InvoiceFreeTextFormat = ""}
			                           };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("Box 7774", invoice.BuyerParty.Party.Address.Postbox.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyerparty_Address_Streetname() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company { StreetName = "Saab Aircraft Leasing", InvoiceFreeTextFormat = "" }
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("Saab Aircraft Leasing", invoice.BuyerParty.Party.Address.StreetName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_PostalZone() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company { Zip = "10396", InvoiceFreeTextFormat = "" }
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("10396", invoice.BuyerParty.Party.Address.PostalZone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_CityName() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company { City = "Stockholm", InvoiceFreeTextFormat = "" }
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("Stockholm", invoice.BuyerParty.Party.Address.CityName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_Department() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany,
			                            	SellingShop = emptyShop, 
			                            	OrderItems = emptyOrderItems,
			                            	CompanyUnit = "Avdelningen för avdelningar"
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("Avdelningen för avdelningar", invoice.BuyerParty.Party.Address.Department.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyName() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company { InvoiceCompanyName = "3250Saab Aircraft Leasing Holding AB" }
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("3250Saab Aircraft Leasing Holding AB", invoice.BuyerParty.Party.PartyName[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyIdentification() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {OrganizationNumber = "556573780501"}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("556573780501", invoice.BuyerParty.Party.PartyIdentification[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_Name() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany, SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	CustomerFirstName = "Adam",
			                            	CustomerLastName = "Bertil"
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("Adam Bertil", invoice.BuyerParty.Party.Contact.Name.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_FirstName_Missing() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany,
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	CustomerLastName = "Bertil"
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("Bertil", invoice.BuyerParty.Party.Contact.Name.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_LastName_Missing() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany,
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	CustomerFirstName = "Adam",
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("Adam", invoice.BuyerParty.Party.Contact.Name.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_Phone() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany,
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	Phone = "080123456"
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("080123456", invoice.BuyerParty.Party.Contact.Telephone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_Email() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany,
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	Email = "adam.bertil@saab.se"
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("adam.bertil@saab.se", invoice.BuyerParty.Party.Contact.ElectronicMail.Value);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyTaxSchemes_VAT_And_SWT() {
			var customOrder =
				new Order {
				          	SellingShop = emptyShop,
				          	OrderItems = emptyOrderItems,
				          	ContractCompany =
				          		new Company {
				          		            	TaxAccountingCode = "SE5560360793",
				          		            	OrganizationNumber = "5560360793",
				          		            	City = "JÄRFÄLLA",
				          		            	Country = new Country {OrganizationCountryCodeId = SwedenCountryCodeNumber}
				          		            }
				          };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			var vatTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			var swtTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.IsNotNull(vatTaxScheme);
			Assert.IsNotNull(swtTaxScheme);
			Assert.AreEqual("SE5560360793", vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("5560360793", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("JÄRFÄLLA", swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual(2, invoice.BuyerParty.Party.PartyTaxScheme.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyTaxSchemes_VAT() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {TaxAccountingCode = "SE5560360793"}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			var vatTaxSchemeFound = invoice.BuyerParty.Party.PartyTaxScheme.Exists(x => x.TaxScheme.ID.Value.Equals("VAT") && x.CompanyID.Value.Equals("SE5560360793"));
			Assert.IsTrue(vatTaxSchemeFound);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyTaxSchemes_SWT() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {
			                            	                              	OrganizationNumber = "5560360793",
			                            	                              	City = "JÄRFÄLLA",
			                            	                              	Country = new Country {OrganizationCountryCodeId = SwedenCountryCodeNumber}
			                            	                              }
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			var swtTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.IsNotNull(swtTaxScheme);
			Assert.AreEqual("5560360793", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("JÄRFÄLLA", swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual(1, invoice.BuyerParty.Party.PartyTaxScheme.Count);
		}

		#endregion

		#region SellerParty
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_StreetName() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	SellingOrganizationStreetName = "Gatan 123"
			                                                      };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("Gatan 123", invoice.SellerParty.Party.Address.StreetName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_PostBox() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	SellingOrganizationPostBox = "Box 111"
			                                                      };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("Box 111", invoice.SellerParty.Party.Address.Postbox.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_PostalZone() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	SellingOrganizationPostalCode = "26422"
			                                                      };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("26422", invoice.SellerParty.Party.Address.PostalZone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_CityName() {
			var customSettings = new SvefakturaConversionSettings
			{
				SellingOrganizationCity = "Klippan"
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("Klippan", invoice.SellerParty.Party.Address.CityName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_Country_IdentificationCode() {
			var customSettings = new SvefakturaConversionSettings
			{
				SellingOrganizationCountry = new SFTICountryType{ IdentificationCode = new CountryIdentificationCodeType{ Value = CountryIdentificationCodeContentType.SE, name="Sverige" } },
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, invoice.SellerParty.Party.Address.Country.IdentificationCode.Value);
			Assert.AreEqual("Sverige", invoice.SellerParty.Party.Address.Country.IdentificationCode.name);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyName() {
			var customSettings = new SvefakturaConversionSettings
			{
				SellingOrganizationName = "Synhälsan Svenska Aktiebolag"
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("Synhälsan Svenska Aktiebolag", invoice.SellerParty.Party.PartyName[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyIdentification() {
			var customSettings = new SvefakturaConversionSettings
			{
				SellingOrganizationNumber = "5562626100"
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("5562626100", invoice.SellerParty.Party.PartyIdentification[0].ID.Value);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Contact() {
			var customOrder = new Order {
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = emptyOrderItems,
			                            	SellingShop = 
			                            		new Shop {
			                            		         	ContactFirstName = "Herr",
			                            		         	ContactLastName = "Försäljare",
			                            		         	Phone = "040123456",
			                            		         	Fax = "040234567",
			                            		         	Email = "info@synbutiken.se",
                                                            Name = "Synbutiken AB"
			                            		         }
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("info@synbutiken.se", invoice.SellerParty.Party.Contact.ElectronicMail.Value);
			Assert.AreEqual("Synbutiken AB (Herr Försäljare)", invoice.SellerParty.Party.Contact.Name.Value);
			Assert.AreEqual("040234567", invoice.SellerParty.Party.Contact.Telefax.Value);
			Assert.AreEqual("040123456", invoice.SellerParty.Party.Contact.Telephone.Value);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_AccountsContact() {
			var customSettings = new SvefakturaConversionSettings
			{
				SellingOrganizationContactEmail = "info@synologen.se",
				SellingOrganizationContactName = "Lotta W",
				SellingOrganizationTelephone = "043513433",
				SellingOrganizationFax = "043513133"
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual("info@synologen.se", invoice.SellerParty.AccountsContact.ElectronicMail.Value);
			Assert.AreEqual("Lotta W", invoice.SellerParty.AccountsContact.Name.Value);
			Assert.AreEqual("043513133", invoice.SellerParty.AccountsContact.Telefax.Value);
			Assert.AreEqual("043513433", invoice.SellerParty.AccountsContact.Telephone.Value);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyTaxSchemes_VAT_And_SWT() {
			var customSettings = new SvefakturaConversionSettings
			{
				TaxAccountingCode = "SE556401196201",
				SellingOrganizationNumber = "556401196201",
				ExemptionReason = "Innehar F-skattebevis",
				SellingOrganizationCity = "Klippan",
				SellingOrganizationPostBox = "Box 111",
				SellingOrganizationCountry = new SFTICountryType{ IdentificationCode = new CountryIdentificationCodeType{ Value = CountryIdentificationCodeContentType.SE, name="Sverige" } },
				SellingOrganizationPostalCode = "26422",
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			var vatTaxScheme = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			var swtTaxScheme = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.IsNotNull(vatTaxScheme);
			Assert.IsNotNull(swtTaxScheme);
			Assert.AreEqual("SE556401196201", vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("556401196201", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("Box 111", swtTaxScheme.RegistrationAddress.Postbox.Value);
			Assert.AreEqual("Klippan", swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual("26422", swtTaxScheme.RegistrationAddress.PostalZone.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual("Sverige", swtTaxScheme.RegistrationAddress.Country.IdentificationCode.name);
			Assert.AreEqual(2,invoice.SellerParty.Party.PartyTaxScheme.Count);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyTaxSchemes_VAT() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	TaxAccountingCode = "SE556401196201",
			                                                      };
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			var vatTaxScheme = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			Assert.AreEqual("SE556401196201", vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual(1, invoice.SellerParty.Party.PartyTaxScheme.Count);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_Settings_PartyTaxSchemes_SWT() {
			var customSettings = new SvefakturaConversionSettings
			{
				SellingOrganizationNumber = "556401196201",
				ExemptionReason = "Innehar F-skattebevis",
				SellingOrganizationCity = "Klippan",
				SellingOrganizationPostBox = "Box 111",
				SellingOrganizationCountry = new SFTICountryType{ IdentificationCode = new CountryIdentificationCodeType{ Value = CountryIdentificationCodeContentType.SE, name="Sverige" } },
				SellingOrganizationPostalCode = "26422",
			};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			var swtTaxScheme = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.AreEqual("556401196201", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("Innehar F-skattebevis",swtTaxScheme.ExemptionReason.Value);
			Assert.AreEqual("Box 111", swtTaxScheme.RegistrationAddress.Postbox.Value);
			Assert.AreEqual("Klippan", swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual("26422", swtTaxScheme.RegistrationAddress.PostalZone.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual("Sverige", swtTaxScheme.RegistrationAddress.Country.IdentificationCode.name);
			Assert.AreEqual(1, invoice.SellerParty.Party.PartyTaxScheme.Count);
		}
		#endregion

		#region Payment Means
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialAccount_Id_BankGiro_And_PostGiro() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	BankGiro = "56936677", 
			                                                      	Postgiro = "123456",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order {
			                            	SellingShop = emptyShop,
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {PaymentDuePeriod = 30}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual(2, invoice.PaymentMeans.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialAccount_Id_BankGiro() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	BankGiro = "56936677",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order
			{
				SellingShop = emptyShop, OrderItems = emptyOrderItems,
				ContractCompany = new Company {PaymentDuePeriod = 30}
			};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("56936677", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialAccount_Id_PostGiro() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	Postgiro = "123456",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order
			{
				SellingShop = emptyShop, OrderItems = emptyOrderItems,
				ContractCompany = new Company {PaymentDuePeriod = 30}
			};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("123456", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_DuePaymentDate() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	BankGiro = "56936677",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order
			{
				SellingShop = emptyShop, OrderItems = emptyOrderItems,
				ContractCompany = new Company {PaymentDuePeriod = 30}
			};
			var expectedValue = new DateTime(2009, 10, 30).AddDays(30);
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual(expectedValue, invoice.PaymentMeans[0].DuePaymentDate.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_PaymentMeansTypeCode() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	BankGiro = "56936677",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order {
			                            	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {PaymentDuePeriod = 30}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, invoice.PaymentMeans[0].PaymentMeansTypeCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution_BankGiro() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	BankGiro = "56936677",
			                                                      	BankgiroBankIdentificationCode = "BGABSESS",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order {
			                            	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {PaymentDuePeriod = 30}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("BGABSESS", invoice.PaymentMeans[0].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution() {
			var customSettings = new SvefakturaConversionSettings {
			                                                      	Postgiro = "123456",
			                                                      	PostgiroBankIdentificationCode = "PGSISESS",
			                                                      	InvoiceIssueDate = new DateTime(2009, 10, 30)
			                                                      };
			var customOrder = new Order {
			                            	SellingShop = emptyShop, OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {PaymentDuePeriod = 30}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("PGSISESS", invoice.PaymentMeans[0].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
		}
		#endregion

		#region Payment Terms
		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoicePaymentTermsTextFormat() {
			var customSettings = new SvefakturaConversionSettings { InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagar netto" };
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = new Company {PaymentDuePeriod = 29}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("29 dagar netto", invoice.PaymentTerms.Note.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoiceExpieryPenaltySurcharge() {
			var customSettings = new SvefakturaConversionSettings {InvoiceExpieryPenaltySurchargePercent = 12.5m};
			var invoice = General.CreateInvoiceSvefaktura(emptyOrder , customSettings);
			Assert.AreEqual(12.5m, invoice.PaymentTerms.PenaltySurchargePercent.Value);
		}
		#endregion

		#region TaxTotal
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_TotalTaxAmount() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> {
			                            	                                 	new OrderItem {NoVAT = false, DisplayTotalPrice = 1000f},
			                            	                                 	new OrderItem {NoVAT = true, DisplayTotalPrice = 90.33f}
			                            	                                 }
			                            };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual(250, invoice.TaxTotal[0].TotalTaxAmount.Value);
			Assert.AreEqual("SEK", invoice.TaxTotal[0].TotalTaxAmount.amountCurrencyID);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_VATAmount() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> {
			                            	                                 	new OrderItem {NoVAT = false},
			                            	                                 	new OrderItem {NoVAT = true}
			                            	                                 }
			                            };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			var taxCategoryS = invoice.TaxTotal[0].TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("S"));
			var taxCategoryE = invoice.TaxTotal[0].TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("E"));
			Assert.IsNotNull(taxCategoryS);
			Assert.IsNotNull(taxCategoryE);
			Assert.AreEqual("S", taxCategoryS.TaxCategory.ID.Value);
			Assert.AreEqual(25.00m, taxCategoryS.TaxCategory.Percent.Value);
			Assert.AreEqual("VAT", taxCategoryS.TaxCategory.TaxScheme.ID.Value);
			Assert.AreEqual("E", taxCategoryE.TaxCategory.ID.Value);
			Assert.AreEqual(0m, taxCategoryE.TaxCategory.Percent.Value);
			Assert.AreEqual("VAT", taxCategoryE.TaxCategory.TaxScheme.ID.Value);
			Assert.AreEqual(2, invoice.TaxTotal[0].TaxSubTotal.Count);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_VATFree() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, 
			                           	ContractCompany = emptyCompany,
			                           	OrderItems = new List<OrderItem> {
			                           	                                 	new OrderItem {NoVAT = true}
			                           	                                 }};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			var taxCategoryE = invoice.TaxTotal[0].TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("E"));
			Assert.IsNotNull(taxCategoryE);
			Assert.AreEqual(0, invoice.TaxTotal[0].TotalTaxAmount.Value);
			Assert.AreEqual("E", taxCategoryE.TaxCategory.ID.Value);
			Assert.AreEqual(0m, taxCategoryE.TaxCategory.Percent.Value);
			Assert.AreEqual("VAT", taxCategoryE.TaxCategory.TaxScheme.ID.Value);
			Assert.AreEqual(1, invoice.TaxTotal[0].TaxSubTotal.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_Complete() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, 
			                           	ContractCompany = emptyCompany,
			                           	OrderItems = new List<OrderItem> {
			                           	                                 	new OrderItem {NoVAT = true, DisplayTotalPrice = 125f},
			                           	                                 	new OrderItem {NoVAT = false, DisplayTotalPrice = 250f},
			                           	                                 	new OrderItem {NoVAT = true, DisplayTotalPrice = 500f},
			                           	                                 	new OrderItem {NoVAT = false, DisplayTotalPrice = 1000f},
			                           	                                 }
			                           };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			var withTaxSubTotal = invoice.TaxTotal[0].TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("S"));
			var noTaxSubTotal = invoice.TaxTotal[0].TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("E"));
			Assert.IsNotNull(withTaxSubTotal);
			Assert.IsNotNull(noTaxSubTotal);
			Assert.AreEqual(1, invoice.TaxTotal.Count);
			Assert.AreEqual(2, invoice.TaxTotal[0].TaxSubTotal.Count);

		}
		#endregion

		#region LegalTotal
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_TaxInclusiveTotalAmount() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	OrderItems = emptyOrderItems,
			                            	ContractCompany = emptyCompany,
			                            	InvoiceSumIncludingVAT = 123456.45
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual(123456.45m, invoice.LegalTotal.TaxInclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.TaxInclusiveTotalAmount.amountCurrencyID);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_TaxExclusiveTotalAmount() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, 
			                           	OrderItems = emptyOrderItems,
			                           	ContractCompany = emptyCompany,
			                           	InvoiceSumExcludingVAT = 123456.4545
			                           };
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual(123456.4545m, invoice.LegalTotal.TaxExclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.TaxExclusiveTotalAmount.amountCurrencyID);
		}
		#endregion

		#region InvoiceRows
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_Description() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, 
			                           	ContractCompany = emptyCompany,
			                           	OrderItems = new List<OrderItem> { new OrderItem { ArticleDisplayName = "Lacryvisc"}}
			                           };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("Lacryvisc", invoice.InvoiceLine[0].Item.Description.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_SellersItemIdentification() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> { new OrderItem { ArticleDisplayNumber = "987654" }}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual("987654", invoice.InvoiceLine[0].Item.SellersItemIdentification.ID.Value);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_InvoicedQuantity() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> { new OrderItem { NumberOfItems = 3 }}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual(3, invoice.InvoiceLine[0].InvoicedQuantity.Value);
			Assert.AreEqual("styck", invoice.InvoiceLine[0].InvoicedQuantity.quantityUnitCode);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_BasePrice_PriceAmount_And_CurrencyID() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> {new OrderItem {SinglePrice = 36.85f}}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual(36.85f, invoice.InvoiceLine[0].Item.BasePrice.PriceAmount.Value);
			Assert.AreEqual("SEK", invoice.InvoiceLine[0].Item.BasePrice.PriceAmount.amountCurrencyID);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_LineExtensionAmount_And_CurrencyID() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> { new OrderItem { DisplayTotalPrice = 110.55f }}
			                            };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.AreEqual(110.55f, invoice.InvoiceLine[0].LineExtensionAmount.Value);
			Assert.AreEqual("SEK", invoice.InvoiceLine[0].LineExtensionAmount.amountCurrencyID);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_TaxCategory_Has_Normal_Tax() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> {new OrderItem { NoVAT =  false} }
			                            };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("S", invoice.InvoiceLine[0].Item.TaxCategory[0].ID.Value);
			Assert.AreEqual(25m, invoice.InvoiceLine[0].Item.TaxCategory[0].Percent.Value);
			Assert.AreEqual("VAT", invoice.InvoiceLine[0].Item.TaxCategory[0].TaxScheme.ID.Value);

		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_TaxCategory_Is_Tax_Free() {
			var customOrder = new Order {
			                            	SellingShop = emptyShop, 
			                            	ContractCompany = emptyCompany,
			                            	OrderItems = new List<OrderItem> { new OrderItem { NoVAT =  true} }
			                            };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = General.CreateInvoiceSvefaktura(customOrder, customSettings);
			Assert.AreEqual("E", invoice.InvoiceLine[0].Item.TaxCategory[0].ID.Value);
			Assert.AreEqual(0m, invoice.InvoiceLine[0].Item.TaxCategory[0].Percent.Value);
			Assert.AreEqual("VAT", invoice.InvoiceLine[0].Item.TaxCategory[0].TaxScheme.ID.Value);
		}
		//TODO: Try to make single assertive
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_ID() {
			var customOrder = new Order{
			                           	SellingShop = emptyShop, 
			                           	ContractCompany = emptyCompany,
			                           	OrderItems = new List<OrderItem> { new OrderItem(), new OrderItem(), new OrderItem() }
			                           };
			var invoice = General.CreateInvoiceSvefaktura(customOrder, emptySettings);
			Assert.IsNotNull(invoice.InvoiceLine[0].ID);
			Assert.IsNotNull(invoice.InvoiceLine[1].ID);
			Assert.IsNotNull(invoice.InvoiceLine[2].ID);
			Assert.AreEqual("1", invoice.InvoiceLine[0].ID.Value);
			Assert.AreEqual("2", invoice.InvoiceLine[1].ID.Value);
			Assert.AreEqual("3", invoice.InvoiceLine[2].ID.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_AdditionalDocumentReference_ID() {
			var customCompany = new Company {Id = 123};
			var customOrder = new Order { ContractCompany = customCompany, SellingShop = emptyShop, OrderItems = emptyOrderItems};
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual("123", invoice.AdditionalDocumentReference[0].ID.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_TaxPointDate() {
			var customOrder = new Order { CreatedDate = new DateTime(2009,11,18), ContractCompany = emptyCompany, SellingShop = emptyShop, OrderItems = emptyOrderItems};
			var invoice = General.CreateInvoiceSvefaktura(customOrder , emptySettings);
			Assert.AreEqual(new DateTime(2009,11,18), invoice.TaxPointDate.Value);
		}


		#endregion
	}
}