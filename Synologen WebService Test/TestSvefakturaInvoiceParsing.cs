using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test {
	[TestFixture]
	public class TestSvefakturaInvoiceParsing {
		private const string NewLine = "\r\n";
		private const string XmlFilePath = @"C:\Develop\WPC\Current-CustomerSpecific\Synologen\Synologen WebService Test\Mock\Svefaktura_example.xml";
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
		[Test]
		public void Test_Create_Invoice_Sets_Settings_BankGiro() {
			var customSettings = new SvefakturaConversionSettings { BankGiro = "56936677" };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("56936677", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_BankGiro_And_PostGiro() {
			var customSettings = new SvefakturaConversionSettings{BankGiro = "56936677", Postgiro = "123456"};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("56936677", invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value);
			Assert.AreEqual("123456", invoice.PaymentMeans[1].PayeeFinancialAccount.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_PaymentMeansTypeCode_Is_Set() {
			var customSettings = new SvefakturaConversionSettings { BankGiro = "56936677", Postgiro = "123456" };
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, invoice.PaymentMeans[0].PaymentMeansTypeCode.Value);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, invoice.PaymentMeans[1].PaymentMeansTypeCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Postgiro_BankGiro_BIC_Codes() {
			var customSettings = new SvefakturaConversionSettings {
				BankGiro = "56936677", BankGiroBankIdentificationCode = "BGABSESS",
				Postgiro = "123456", PostgiroBankIdentificationCode = "PGSISESS"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("BGABSESS", invoice.PaymentMeans[0].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
			Assert.AreEqual("PGSISESS", invoice.PaymentMeans[1].PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_VATAmount() {
			var customSettings = new SvefakturaConversionSettings {
				VATAmount = 0.5555m
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("S", invoice.TaxTotal[0].TaxSubTotal[0].TaxCategory.ID.Value);
			Assert.AreEqual(55.55m, invoice.TaxTotal[0].TaxSubTotal[0].TaxCategory.Percent.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Org_Address() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationAddress = "Box 111"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Box 111", invoice.SellerParty.Party.Address.StreetName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Org_City() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationCity = "Klippan"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Klippan", invoice.SellerParty.Party.Address.CityName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Org_CountryCode() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationCountryCode = CountryIdentificationCodeContentType.SE
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, invoice.SellerParty.Party.PartyTaxScheme[0].RegistrationAddress.Country.IdentificationCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Org_Name() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationName = "Synhälsan Svenska Aktiebolag"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("Synhälsan Svenska Aktiebolag", invoice.SellerParty.Party.PartyName[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Org_Number() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationNumber = "5562626100"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("5562626100", invoice.SellerParty.Party.PartyTaxScheme[0].CompanyID.Value);
			Assert.AreEqual("VAT", invoice.SellerParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_Org_PostalCode() {
			var customSettings = new SvefakturaConversionSettings {
				SellingOrganizationPostalCode = "26422"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("26422", invoice.SellerParty.Party.Address.PostalZone.Value);
		}


	}
}