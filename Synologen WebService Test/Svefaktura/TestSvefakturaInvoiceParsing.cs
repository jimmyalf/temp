using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura {
	[TestFixture]
	public class TestInvoiceParsing {
		private readonly OrderRow emptyOrder = new OrderRow();
		private readonly List<IOrderItem> emptyOrderItemList = new List<IOrderItem>();
		private readonly CompanyRow emptyCompany = new CompanyRow();
		private readonly ShopRow emptyShop = new ShopRow();
		private readonly SvefakturaConversionSettings emptySettings = new SvefakturaConversionSettings();

		[TestFixtureSetUp]
		public void Setup() { }

		[Test]
		public void Test_Create_Invoice_Parameter_Checks_For_Null_And_Throws_Exceptions() {
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(null,		emptyOrderItemList, emptyCompany,	emptyShop,	emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	null,				emptyCompany,	emptyShop,	emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	emptyOrderItemList, null,			emptyShop,	emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	emptyOrderItemList, emptyCompany,	null,		emptySettings));
			Assert.Throws<ArgumentNullException>(() => Utility.General.CreateInvoiceSvefaktura(emptyOrder,	emptyOrderItemList, emptyCompany,	emptyShop,	null));
		}

		#region General Invoice
		[Test]
		public void Test_Create_Invoice_Sets_ID() {
			var customOrder = new OrderRow { InvoiceNumber = 123456 };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("123456", invoice.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_RequisitionistDocumentReference() {
			var customOrder = new OrderRow { CustomerOrderNumber = "123456"};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("123456", invoice.RequisitionistDocumentReference[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_IssueDate() {
		    var customSettings = new SvefakturaConversionSettings {
		        InvoiceIssueDate = new DateTime(2009, 10, 30)
		    };
		    var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
		    Assert.AreEqual(new DateTime(2009, 10, 30), invoice.IssueDate.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceTypeCode() {
			var customSettings = new SvefakturaConversionSettings {
				InvoiceTypeCode = "380"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("380", invoice.InvoiceTypeCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceCurrencyCode() {
			var customSettings = new SvefakturaConversionSettings {
				InvoiceCurrencyCode = CurrencyCodeContentType.SEK
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual(CurrencyCodeContentType.SEK, invoice.InvoiceCurrencyCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_LineItemCountNumeric() {
			var customOrderLines = new List<IOrderItem> {new OrderItemRow{ArticleDisplayName = "One"}, new OrderItemRow{ArticleDisplayName = "Two"}};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderLines, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(2m, invoice.LineItemCountNumeric.Value);
		}

		#endregion

		#region BuyerParty
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_PostBox() {
			var customCompany = new CompanyRow {
				PostBox = "Box 7774"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("Box 7774", invoice.BuyerParty.Party.Address.Postbox.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyerparty_Address_Streetname() {
			var customCompany = new CompanyRow {
				StreetName = "Saab Aircraft Leasing"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("Saab Aircraft Leasing", invoice.BuyerParty.Party.Address.StreetName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_PostalZone() {
			var customCompany = new CompanyRow {
				Zip = "10396"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("10396", invoice.BuyerParty.Party.Address.PostalZone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_CityName() {
			var customCompany = new CompanyRow {
				City = "Stockholm"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("Stockholm", invoice.BuyerParty.Party.Address.CityName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address_Department() {
			var customOrder = new OrderRow {
				CompanyUnit = "Avdelningen för avdelningar"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Avdelningen för avdelningar", invoice.BuyerParty.Party.Address.Department.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyName() {
			var customCompany = new CompanyRow {
				InvoiceCompanyName = "3250Saab Aircraft Leasing Holding AB"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("3250Saab Aircraft Leasing Holding AB", invoice.BuyerParty.Party.PartyName[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyIdentification() {
			var customCompany = new CompanyRow {
				OrganizationNumber = "556573780501"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("556573780501", invoice.BuyerParty.Party.PartyIdentification[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact() {
			var customOrder = new OrderRow {
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				Phone = "080123456",
                Email = "adam.bertil@saab.se"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Adam Bertil", invoice.BuyerParty.Party.Contact.Name.Value);
			Assert.AreEqual("080123456", invoice.BuyerParty.Party.Contact.Telephone.Value);
			Assert.AreEqual("adam.bertil@saab.se", invoice.BuyerParty.Party.Contact.ElectronicMail.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_FirstName_Missing() {
			var customOrder = new OrderRow {
				CustomerLastName = "Bertil",
				Phone = "080123456",
                Email = "adam.bertil@saab.se"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Bertil", invoice.BuyerParty.Party.Contact.Name.Value);
			Assert.AreEqual("080123456", invoice.BuyerParty.Party.Contact.Telephone.Value);
			Assert.AreEqual("adam.bertil@saab.se", invoice.BuyerParty.Party.Contact.ElectronicMail.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_LastName_Missing() {
			var customOrder = new OrderRow {
				CustomerFirstName = "Adam",
				Phone = "080123456",
                Email = "adam.bertil@saab.se"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Adam", invoice.BuyerParty.Party.Contact.Name.Value);
			Assert.AreEqual("080123456", invoice.BuyerParty.Party.Contact.Telephone.Value);
			Assert.AreEqual("adam.bertil@saab.se", invoice.BuyerParty.Party.Contact.ElectronicMail.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyTaxSchemes_VAT_And_SWT() {
			var customCompany = new CompanyRow {
				TaxAccountingCode = "SE5560360793",
				OrganizationNumber = "5560360793",
				ExemptionReason = "F-skattebevis finns",
                City = "JÄRFÄLLA",
				OrganizationCountryCode = CountryIdentificationCodeContentType.SE
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			var vatTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			var swtTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.IsNotNull(vatTaxScheme);
			Assert.IsNotNull(swtTaxScheme);
			Assert.AreEqual("SE5560360793", vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("5560360793", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("F-skattebevis finns", swtTaxScheme.ExemptionReason.Value);
			Assert.AreEqual("JÄRFÄLLA", swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual(2, invoice.BuyerParty.Party.PartyTaxScheme.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyTaxSchemes_VAT() {
			var customCompany = new CompanyRow {
				TaxAccountingCode = "SE5560360793",
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			var vatTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			Assert.IsNotNull(vatTaxScheme);
			Assert.AreEqual("SE5560360793", vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual(1, invoice.BuyerParty.Party.PartyTaxScheme.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_PartyTaxSchemes_SWT() {
			var customCompany = new CompanyRow {
				OrganizationNumber = "5560360793",
				ExemptionReason = "F-skattebevis finns",
                City = "JÄRFÄLLA",
				OrganizationCountryCode = CountryIdentificationCodeContentType.SE
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			var swtTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.IsNotNull(swtTaxScheme);
			Assert.AreEqual("5560360793", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("F-skattebevis finns", swtTaxScheme.ExemptionReason.Value);
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
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Gatan 123", invoice.SellerParty.Party.Address.StreetName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_PostBox() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationPostBox = "Box 111"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Box 111", invoice.SellerParty.Party.Address.Postbox.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_PostalZone() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationPostalCode = "26422"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("26422", invoice.SellerParty.Party.Address.PostalZone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_CityName() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationCity = "Klippan"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Klippan", invoice.SellerParty.Party.Address.CityName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address_Country_IdentificationCode() {
		    var customSettings = new SvefakturaConversionSettings {
		        SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE
		    };
		    var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
		    Assert.AreEqual(CountryIdentificationCodeContentType.SE, invoice.SellerParty.Party.Address.Country.IdentificationCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyName() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationName = "Synhälsan Svenska Aktiebolag"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Synhälsan Svenska Aktiebolag", invoice.SellerParty.Party.PartyName[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyIdentification() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationNumber = "5562626100"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("5562626100", invoice.SellerParty.Party.PartyIdentification[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Contact() {
			var customShop = new ShopRow {
                ContactFirstName = "Herr",
				ContactLastName = "Försäljare",
                Phone = "040123456",
				Fax = "040234567", 
				Email = "info@synbutiken.se"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, customShop, emptySettings);
			Assert.AreEqual("info@synbutiken.se", invoice.SellerParty.Party.Contact.ElectronicMail.Value);
			Assert.AreEqual("Herr Försäljare", invoice.SellerParty.Party.Contact.Name.Value);
			Assert.AreEqual("040234567", invoice.SellerParty.Party.Contact.Telefax.Value);
			Assert.AreEqual("040123456", invoice.SellerParty.Party.Contact.Telephone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_AccountsContact() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationContactEmail = "info@synologen.se",
                SellingOrganizationContactName = "Lotta W",
				SellingOrganizationTelephone = "043513433",
				SellingOrganizationFax = "043513133"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("info@synologen.se", invoice.SellerParty.AccountsContact.ElectronicMail.Value);
			Assert.AreEqual("Lotta W", invoice.SellerParty.AccountsContact.Name.Value);
			Assert.AreEqual("043513133", invoice.SellerParty.AccountsContact.Telefax.Value);
			Assert.AreEqual("043513433", invoice.SellerParty.AccountsContact.Telephone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyTaxSchemes_VAT_And_SWT() {
			var customSettings = new SvefakturaConversionSettings {
				TaxAccountingCode = "SE556401196201",
				SellingOrganizationNumber = "556401196201",
				ExemptionReason = "Innehar F-skattebevis",
                SellingOrganizationCity = "Klippan",
                SellingOrganizationPostBox = "Box 111",
				SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE,
                SellingOrganizationPostalCode = "26422",
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
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
			Assert.AreEqual(2,invoice.SellerParty.Party.PartyTaxScheme.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyTaxSchemes_VAT() {
			var customSettings = new SvefakturaConversionSettings {
				TaxAccountingCode = "SE556401196201",
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			var vatTaxScheme = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			Assert.AreEqual("SE556401196201", vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual(1, invoice.SellerParty.Party.PartyTaxScheme.Count);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_PartyTaxSchemes_SWT() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationNumber = "556401196201",
				ExemptionReason = "Innehar F-skattebevis",
                SellingOrganizationCity = "Klippan",
                SellingOrganizationPostBox = "Box 111",
				SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE,
                SellingOrganizationPostalCode = "26422",
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			var swtTaxScheme = invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.AreEqual("556401196201", swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual("Innehar F-skattebevis",swtTaxScheme.ExemptionReason.Value);
			Assert.AreEqual("Box 111", swtTaxScheme.RegistrationAddress.Postbox.Value);
			Assert.AreEqual("Klippan", swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual("26422", swtTaxScheme.RegistrationAddress.PostalZone.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual(1, invoice.SellerParty.Party.PartyTaxScheme.Count);
		}
		#endregion

		#region Payment Means
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialAccount_Id_BankGiro_And_PostGiro() {
			var customSettings = new SvefakturaConversionSettings{BankGiro = "56936677", Postgiro = "123456"};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("56936677", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
			Assert.AreEqual("123456", invoice.PaymentMeans[1].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialAccount_Id_BankGiro() {
			var customSettings = new SvefakturaConversionSettings { BankGiro = "56936677" };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("56936677", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialAccount_Id_PostGiro() {
			var customSettings = new SvefakturaConversionSettings { Postgiro = "123456" };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("123456", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_DuePaymentDate() {
			var customSettings = new SvefakturaConversionSettings {
				BankGiro = "56936677", 
				Postgiro = "123456",
				InvoiceIssueDate = new DateTime(2009, 10, 30)
			};
			var customCompany = new CompanyRow{PaymentDuePeriod = 30};
			var expectedValue = new DateTime(2009, 10, 30).AddDays(30);
			
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, customSettings);
			Assert.AreEqual(expectedValue, invoice.PaymentMeans[0].DuePaymentDate.Value);
			Assert.AreEqual(expectedValue, invoice.PaymentMeans[1].DuePaymentDate.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_PaymentMeansTypeCode() {
			var customSettings = new SvefakturaConversionSettings { BankGiro = "56936677", Postgiro = "123456" };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, invoice.PaymentMeans[0].PaymentMeansTypeCode.Value);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, invoice.PaymentMeans[1].PaymentMeansTypeCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution() {
			var customSettings = new SvefakturaConversionSettings {
				BankGiro = "56936677",
				BankgiroBankIdentificationCode = "BGABSESS",
				Postgiro = "123456",
				PostgiroBankIdentificationCode = "PGSISESS"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("BGABSESS", invoice.PaymentMeans[0].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
			Assert.AreEqual("PGSISESS", invoice.PaymentMeans[1].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
		}
		#endregion

		#region Payment Terms
		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoicePaymentTermsTextFormat() {
			var customSettings = new SvefakturaConversionSettings { InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagar netto" };
			var customCompany = new CompanyRow {PaymentDuePeriod = 29, };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, customSettings);
			Assert.AreEqual("29 dagar netto", invoice.PaymentTerms.Note.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoiceExpieryPenaltySurcharge() {
			var customSettings = new SvefakturaConversionSettings {InvoiceExpieryPenaltySurchargePercent = 12.5m};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual(12.5m, invoice.PaymentTerms.PenaltySurchargePercent.Value);
		}
		#endregion

		#region TaxTotal
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_TotalTaxAmount() {
			var customOrder = new OrderRow {
				InvoiceSumIncludingVAT = 12345.789,
				InvoiceSumExcludingVAT = 11005.456
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(1340.333, invoice.TaxTotal[0].TotalTaxAmount.Value);
			Assert.AreEqual("SEK", invoice.TaxTotal[0].TotalTaxAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_VATAmount() {
			var customSettings = new SvefakturaConversionSettings {
				VATAmount = 0.25m
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
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
		[Test]
		public void Test_Create_Invoice_Sets_TaxTotal_VATFree() {
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			var taxCategoryE = invoice.TaxTotal[0].TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("E"));
			Assert.IsNotNull(taxCategoryE);
			Assert.AreEqual("E", taxCategoryE.TaxCategory.ID.Value);
			Assert.AreEqual(0m, taxCategoryE.TaxCategory.Percent.Value);
			Assert.AreEqual("VAT", taxCategoryE.TaxCategory.TaxScheme.ID.Value);
			Assert.AreEqual(1, invoice.TaxTotal[0].TaxSubTotal.Count);
		}
		#endregion

		#region LegalTotal
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_TaxInclusiveTotalAmount() {
			var customOrder = new OrderRow { InvoiceSumIncludingVAT = 123456.45 };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(123456.45m, invoice.LegalTotal.TaxInclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.TaxInclusiveTotalAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_TaxExclusiveTotalAmount() {
			var customOrder = new OrderRow { InvoiceSumExcludingVAT = 123456.4545 };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(123456.4545m, invoice.LegalTotal.TaxExclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.TaxExclusiveTotalAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_RoundOffAmount() {
			var customOrder = new OrderRow { RoundOffAmount = 0.38m };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.IsNotNull(invoice.LegalTotal);
			Assert.IsNotNull(invoice.LegalTotal.RoundOffAmount);
			Assert.AreEqual(0.38m, invoice.LegalTotal.RoundOffAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.RoundOffAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_RoundOffAmount_Nulled() {
			var customOrder1 = new OrderRow { RoundOffAmount = null };
			var customOrder2 = new OrderRow { RoundOffAmount = null, InvoiceSumExcludingVAT = 123456.4545 };
			var invoice1 = Utility.General.CreateInvoiceSvefaktura(customOrder1, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			var invoice2 = Utility.General.CreateInvoiceSvefaktura(customOrder2, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.IsNull(invoice1.LegalTotal);
			Assert.IsNotNull(invoice2.LegalTotal);
			Assert.IsNull(invoice2.LegalTotal.RoundOffAmount);
		}
		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_RoundOffAmount_Zero() {
			var customOrder1 = new OrderRow { RoundOffAmount = 0 };
			var customOrder2 = new OrderRow { RoundOffAmount = 0, InvoiceSumExcludingVAT = 123456.4545  };
			var invoice1 = Utility.General.CreateInvoiceSvefaktura(customOrder1, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			var invoice2 = Utility.General.CreateInvoiceSvefaktura(customOrder2, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.IsNull(invoice1.LegalTotal);
			Assert.IsNotNull(invoice2.LegalTotal);
			Assert.IsNull(invoice2.LegalTotal.RoundOffAmount);
		}
		#endregion

		#region InvoiceRows
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_Description() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					ArticleDisplayName = "Lacryvisc"
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Lacryvisc", invoice.InvoiceLine[0].Item.Description.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_SellersItemIdentification() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					ArticleDisplayNumber = "987654"
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("987654", invoice.InvoiceLine[0].Item.SellersItemIdentification.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_InvoicedQuantity() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					NumberOfItems = 3
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(3, invoice.InvoiceLine[0].InvoicedQuantity.Value);
			Assert.AreEqual("styck", invoice.InvoiceLine[0].InvoicedQuantity.quantityUnitCode);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_BasePrice_PriceAmount_And_CurrencyID() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					SinglePrice = 36.85f
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(36.85f, invoice.InvoiceLine[0].Item.BasePrice.PriceAmount.Value);
			Assert.AreEqual("SEK", invoice.InvoiceLine[0].Item.BasePrice.PriceAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_LineExtensionAmount_And_CurrencyID() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					DisplayTotalPrice = 110.55f
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(110.55f, invoice.InvoiceLine[0].LineExtensionAmount.Value);
			Assert.AreEqual("SEK", invoice.InvoiceLine[0].LineExtensionAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_TaxCategory_Has_Normal_Tax() {
			var customOrderItemList = new List<IOrderItem> { new OrderItemRow { NoVAT =  false} };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("S", invoice.InvoiceLine[0].Item.TaxCategory[0].ID.Value);
			Assert.AreEqual(25m, invoice.InvoiceLine[0].Item.TaxCategory[0].Percent.Value);
			Assert.AreEqual("VAT", invoice.InvoiceLine[0].Item.TaxCategory[0].TaxScheme.ID.Value);

		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_TaxCategory_Is_Tax_Free() {
			var customOrderItemList = new List<IOrderItem> { new OrderItemRow { NoVAT =  true} };
			var customSettings = new SvefakturaConversionSettings {VATAmount = 0.25m};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("E", invoice.InvoiceLine[0].Item.TaxCategory[0].ID.Value);
			Assert.AreEqual(0m, invoice.InvoiceLine[0].Item.TaxCategory[0].Percent.Value);
			Assert.AreEqual("VAT", invoice.InvoiceLine[0].Item.TaxCategory[0].TaxScheme.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_ID() {
			var customOrderItemList = new List<IOrderItem> { new OrderItemRow(), new OrderItemRow(), new OrderItemRow() };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.IsNotNull(invoice.InvoiceLine[0].ID);
			Assert.IsNotNull(invoice.InvoiceLine[1].ID);
			Assert.IsNotNull(invoice.InvoiceLine[2].ID);
			Assert.AreEqual("1", invoice.InvoiceLine[0].ID.Value);
			Assert.AreEqual("2", invoice.InvoiceLine[1].ID.Value);
			Assert.AreEqual("3", invoice.InvoiceLine[2].ID.Value);
		}


		#endregion
	}
}