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

		#region Settings / Seller
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
				BankGiro = "56936677",
				BankgiroBankIdentificationCode = "BGABSESS",
				Postgiro = "123456",
				PostgiroBankIdentificationCode = "PGSISESS"
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
		[Test]
		public void Test_Create_Invoice_Sets_Settings_IssueDate() {
			var customSettings = new SvefakturaConversionSettings {
				InvoiceIssueDate = new DateTime(2009, 10, 30)
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual(new DateTime(2009, 10, 30), invoice.IssueDate.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_InvoiceType() {
			var customSettings = new SvefakturaConversionSettings {
				InvoiceTypeCode = "380"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			Assert.AreEqual("380", invoice.InvoiceTypeCode.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Settings_InvoiceDueDate() {
			var customSettings = new SvefakturaConversionSettings {
				InvoiceDaysFromIssueUntilDueDate = 30,
				InvoiceIssueDate = new DateTime(2009, 09, 30)
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, emptyShop, customSettings);
			var expectedValue = new DateTime(2009, 09, 30).AddDays(30);
			Assert.AreEqual(expectedValue, invoice.PaymentMeans[0].DuePaymentDate.Value);
		}
		#endregion

		//#region Seller Contact
		//[Test]
		//public void Test_Create_Invoice_Sets_Shop_Name() {
		//    var customShop = new ShopRow {
		//        Name = "Optiker Testsson"
		//    };
		//    var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, customShop, emptySettings);
		//    Assert.AreEqual("Optiker Testsson", invoice.SellerParty.Party.PartyName[0].Value);
		//}
		//[Test]
		//public void Test_Create_Invoice_Sets_Shop_Address() {
		//    var customShop = new ShopRow {
		//        Address = "Box 0123",
		//        Address2 = "Storgatan 10",
		//        City = "Stockholm",
		//        Zip = "65465"
		//    };
		//    var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, customShop, emptySettings);
		//    Assert.AreEqual("Storgatan 10", invoice.SellerParty.Party.Address.StreetName.Value);
		//    Assert.AreEqual("Box 0123", invoice.SellerParty.Party.Address.Postbox.Value);
		//    Assert.AreEqual("65465", invoice.SellerParty.Party.Address.PostalZone.Value);
		//    Assert.AreEqual("Stockholm", invoice.SellerParty.Party.Address.CityName.Value);
		//}
		//[Test]
		//public void Test_Create_Invoice_Sets_Shop_Contact() {
		//    var customShop = new ShopRow {
		//        ContactFirstName = "Adam",
		//        ContactLastName = "Bertil",
		//        Email = "adam.bertil@syn.se",
		//        Phone = "089876543",
		//        Fax = "088765432"
		//    };
		//    var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, emptyCompany, customShop, emptySettings);
		//    Assert.AreEqual("Adam Bertil", invoice.SellerParty.Party.Contact.Name.Value);
		//    Assert.AreEqual("088765432", invoice.SellerParty.Party.Contact.Telefax.Value);
		//    Assert.AreEqual("089876543", invoice.SellerParty.Party.Contact.Telephone.Value);
		//    Assert.AreEqual("adam.bertil@syn.se", invoice.SellerParty.Party.Contact.ElectronicMail.Value);
		//}

		//#endregion

		#region Buyer
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_Address1_And_Address2() {
			var customCompany = new CompanyRow {
				Address1 = "Saab Aircraft Leasing",
				Address2 = "Box 7774"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual(2, invoice.BuyerParty.Party.Address.AddressLine.Count);
			Assert.AreEqual("Saab Aircraft Leasing", invoice.BuyerParty.Party.Address.AddressLine[0].Value);
			Assert.AreEqual("Box 7774", invoice.BuyerParty.Party.Address.AddressLine[1].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_Address1_() {
			var customCompany = new CompanyRow {
				Address1 = "Saab Aircraft Leasing"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual(1, invoice.BuyerParty.Party.Address.AddressLine.Count);
			Assert.AreEqual("Saab Aircraft Leasing", invoice.BuyerParty.Party.Address.AddressLine[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_Address2() {
			var customCompany = new CompanyRow {
				Address2 = "Box 7774"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual(1, invoice.BuyerParty.Party.Address.AddressLine.Count);
			Assert.AreEqual("Box 7774", invoice.BuyerParty.Party.Address.AddressLine[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_PostalCode() {
			var customCompany = new CompanyRow {
				Zip = "10396"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("10396", invoice.BuyerParty.Party.Address.PostalZone.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_City() {
			var customCompany = new CompanyRow {
				City = "Stockholm"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("Stockholm", invoice.BuyerParty.Party.Address.CityName.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_TaxAccountingCode() {
			var customCompany = new CompanyRow {
				TaxAccountingCode = "SE556573780501"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("SE556573780501", invoice.BuyerParty.Party.PartyTaxScheme[0].CompanyID.Value);
			Assert.AreEqual("VAT", invoice.BuyerParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_Company_Name() {
			var customCompany = new CompanyRow {
				InvoiceCompanyName = "3250Saab Aircraft Leasing Holding AB"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("3250Saab Aircraft Leasing Holding AB", invoice.BuyerParty.Party.PartyName[0].Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Buyer_OrganizationNumber() {
			var customCompany = new CompanyRow {
				OrganizationNumber = "556573780501"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, emptyOrderItemList, customCompany, emptyShop, emptySettings);
			Assert.AreEqual("556573780501", invoice.BuyerParty.Party.PartyIdentification[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_CompanyUnit() {
			var customOrder = new OrderRow {
				CompanyUnit = "Avdelningen för avdelningar"
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Avdelningen för avdelningar", invoice.BuyerParty.Party.Address.Department.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Contact() {
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
		public void Test_Create_Invoice_Sets_Contact_With_FirstName_Missing() {
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
		public void Test_Create_Invoice_Sets_Contact_With_LastName_Missing() {
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

		#endregion

		#region General Invoice
		[Test]
		public void Test_Create_Invoice_Sets_Invoice_Number() {
			var customOrder = new OrderRow { InvoiceNumber = 123456 };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("123456", invoice.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Invoice_Sum_Including_VAT() {
			var customOrder = new OrderRow { InvoiceSumIncludingVAT = 123456.45 };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(123456.45m, invoice.LegalTotal.TaxInclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.TaxInclusiveTotalAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Invoice_Sum_Excluding_VAT() {
			var customOrder = new OrderRow { InvoiceSumExcludingVAT = 123456.4545 };
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(123456.4545m, invoice.LegalTotal.TaxExclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", invoice.LegalTotal.TaxExclusiveTotalAmount.amountCurrencyID);
		}
		[Test]
		public void Test_Create_Invoice_Sets_Customer_Order_Number() {
			var customOrder = new OrderRow { CustomerOrderNumber = "123456"};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("123456", invoice.RequisitionistDocumentReference[0].ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_TotalTaxAmount() {
			var customOrder = new OrderRow {
				InvoiceSumIncludingVAT = 12345.789,
				InvoiceSumExcludingVAT = 11005.456
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(customOrder, emptyOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(1340.333, invoice.TaxTotal[0].TotalTaxAmount.Value);
			Assert.AreEqual("SEK", invoice.TaxTotal[0].TotalTaxAmount.amountCurrencyID);
		}

		#endregion

		#region InvoiceRows
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceRow_ArticleName() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					ArticleDisplayName = "Lacryvisc"
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("Lacryvisc", invoice.InvoiceLine[0].Item.Description.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceRow_ArticleNumber() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					ArticleDisplayNumber = "987654"
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual("987654", invoice.InvoiceLine[0].Item.StandardItemIdentification.ID.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_InvoiceRow_Quantity() {
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
		public void Test_Create_Invoice_Sets_InvoiceRow_SingleItemPrice() {
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
		public void Test_Create_Invoice_Sets_InvoiceRow_TotalRowPrice() {
			var customOrderItemList = new List<IOrderItem> {
				new OrderItemRow {
					DisplayTotalPrice = 110.55f
				}
			};
			var invoice = Utility.General.CreateInvoiceSvefaktura(emptyOrder, customOrderItemList, emptyCompany, emptyShop, emptySettings);
			Assert.AreEqual(110.55f, invoice.InvoiceLine[0].LineExtensionAmount.Value);
			Assert.AreEqual("SEK", invoice.InvoiceLine[0].LineExtensionAmount.amountCurrencyID);
		}

		#endregion
	}
}