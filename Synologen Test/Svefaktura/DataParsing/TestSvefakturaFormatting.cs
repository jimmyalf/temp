using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.DataParsing{
	[TestFixture]
	public class TestSvefakturaFormatting : AssertionHelper{
		private readonly OrderRow emptyOrder = new OrderRow();
		private readonly IList<IOrderItem> emptyOrderItems = new List<IOrderItem>();
		private readonly SvefakturaConversionSettings emptySettings = new SvefakturaConversionSettings();
		private readonly CompanyRow emptyCompany = new CompanyRow();
		private readonly ShopRow emptyShop = new ShopRow();

		[SetUp]
		public void Setup() { }

		[Test]
		public void Test_Telephone_Formatting_With_Country_Code(){
			var customOrder = new OrderRow {Phone = "+46 (0) 123 - 456789"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(emptySettings, customOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.BuyerParty.Party.Contact.Telephone.Value, Is.EqualTo("+46123456789"));
		}
		[Test]
		public void Test_Telephone_Formatting_Without_Country_Code(){
			var customOrder = new OrderRow {Phone = "0123 - 456789"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(emptySettings, customOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.BuyerParty.Party.Contact.Telephone.Value, Is.EqualTo("0123456789"));
		}
		[Test]
		public void Test_Fax_Formatting_With_Country_Code(){
			var customSettings = new SvefakturaConversionSettings{SellingOrganizationFax = "+46 (0) 123 - 456789"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(customSettings, emptyOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.SellerParty.AccountsContact.Telefax.Value, Is.EqualTo("+46123456789"));
		}
		[Test]
		public void Test_Fax_Formatting_Without_Country_Code(){
			var customSettings = new SvefakturaConversionSettings{SellingOrganizationFax = "0123 - 456789"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(customSettings, emptyOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.SellerParty.AccountsContact.Telefax.Value, Is.EqualTo("0123456789"));
		}
		[Test]
		public void Test_PostGiro_Formatting(){
			var customSettings = new SvefakturaConversionSettings{Postgiro = "SE 555-654.123 - 645", InvoiceIssueDate = DateTime.Now};
			var invoice = Utility.Convert.ToSvefakturaInvoice(customSettings, emptyOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value, Is.EqualTo("SE555654123645"));
		}
		[Test]
		public void Test_BankGiro_Formatting(){
			var customSettings = new SvefakturaConversionSettings {BankGiro = "SE 555-654.123 - 645", InvoiceIssueDate = DateTime.Now};
			var invoice = Utility.Convert.ToSvefakturaInvoice(customSettings, emptyOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.PaymentMeans[0].PayeeFinancialAccount.ID.Value, Is.EqualTo("SE555654123645"));
		}
		[Test]
		public void Test_TaxAccountingCode_Formatting_Seller(){
			var customSettings = new SvefakturaConversionSettings { TaxAccountingCode = "SE 555-654.123 - 645"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(customSettings, emptyOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.SellerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo("SE555654123645"));
		}
		[Test]
		public void Test_TaxAccountingCode_Formatting_Buyer(){
			var customCompany = new CompanyRow{ TaxAccountingCode = "SE 555-654.123 - 645"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(emptySettings, emptyOrder, emptyOrderItems, customCompany, emptyShop);
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[0].CompanyID.Value, Is.EqualTo("SE555654123645"));
		}

		[Test]
		public void Test_OrganizationNumber_Formatting_Seller(){
			var customSettings = new SvefakturaConversionSettings { TaxAccountingCode="ABC", SellingOrganizationNumber = "SE 555-654.123 - 645"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(customSettings, emptyOrder, emptyOrderItems, emptyCompany, emptyShop);
			Expect(invoice.SellerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo("SE555654123645"));
			Expect(invoice.SellerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo("SE555654123645"));
		}
		[Test]
		public void Test_OrganizationNumber_Formatting_Buyer(){
			var customCompany = new CompanyRow{ TaxAccountingCode="ABC", OrganizationNumber = "SE 555-654.123 - 645"};
			var invoice = Utility.Convert.ToSvefakturaInvoice(emptySettings, emptyOrder, emptyOrderItems, customCompany, emptyShop);
			Expect(invoice.BuyerParty.Party.PartyTaxScheme[1].CompanyID.Value, Is.EqualTo("SE555654123645"));
			Expect(invoice.BuyerParty.Party.PartyIdentification[0].ID.Value, Is.EqualTo("SE555654123645"));
		}

	}
}