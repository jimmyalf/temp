using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura 
{
	[TestFixture]
	public class Parse_Validation
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;

		[SetUp]
		public void Setup()
		{
			_order = Factory.GetOrder();
			_settings = new SvefakturaConversionSettings();
		}

		[Test]
		public void Test_Create_Invoice_Parameter_Checks_Invoice_Missing() 
		{
			Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(null, _settings));
		}

		[Test]
		public void Test_Create_Invoice_Parameter_Checks_Settings_Missing() 
		{
			Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(_order, null));
		}
	}

	[TestFixture]
	public class Parse_General_Invoice_Data
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;
		private SFTIInvoiceType _invoice;

		[SetUp]
		public void Setup()
		{
			var company = Factory.GetCompany();
			var shop = Factory.GetShop();
			var orderItems = Factory.GetOrderItems();
			_order = Factory.GetOrder(company, shop, orderItems);
			_settings = Factory.GetSettings();
			_invoice = General.CreateInvoiceSvefaktura(_order,  _settings);
		}

		[Test]
		public void Test_Create_Invoice_Sets_ID() 
		{
			Assert.AreEqual(_order.InvoiceNumber.ToString(), _invoice.ID.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_RequisitionistDocumentReference() 
		{
			Assert.AreEqual(_order.CustomerOrderNumber, _invoice.RequisitionistDocumentReference[0].ID.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_IssueDate() 
		{

		    Assert.AreEqual(_settings.InvoiceIssueDate, _invoice.IssueDate.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_InvoiceTypeCode() 
		{
			Assert.AreEqual(_settings.InvoiceTypeCode, _invoice.InvoiceTypeCode.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_InvoiceCurrencyCode() 
		{
			Assert.AreEqual(_invoice.InvoiceCurrencyCode.Value, _invoice.InvoiceCurrencyCode.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_LineItemCountNumeric() 
		{
			Assert.AreEqual(_order.OrderItems.Count, _invoice.LineItemCountNumeric.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_Note() 
		{
			var expectedNote = _order.ContractCompany.InvoiceFreeTextFormat
				.ReplaceWith(new {CustomerName = _order.CustomerCombinedName})
				.ReplaceWith(new {CustomerPersonalIdNumber = _order.PersonalIdNumber})
				.ReplaceWith(new {CompanyUnit = _order.CompanyUnit})
				.ReplaceWith(new {CustomerPersonalBirthDateString = _order.PersonalBirthDateString})
				.ReplaceWith(new {CustomerFirstName = _order.CustomerFirstName})
				.ReplaceWith(new {CustomerLastName = _order.CustomerLastName})
				.ReplaceWith(new {RST = _order.RstText})
				.ReplaceWith(new {BuyerCompanyId = _order.CompanyId})
				.ReplaceWith(new {BankCode = _order.ContractCompany.BankCode})
				.ReplaceWith(new {SellingShopName = _order.SellingShop.Name})
				.ReplaceWith(new {SellingShopNumber = _order.SellingShop.Number});
		    Assert.AreEqual(expectedNote, _invoice.Note.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_TaxInclusiveTotalAmount() 
		{
			Assert.AreEqual(_order.InvoiceSumIncludingVAT, _invoice.LegalTotal.TaxInclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", _invoice.LegalTotal.TaxInclusiveTotalAmount.amountCurrencyID);
		}

		[Test]
		public void Test_Create_Invoice_Sets_LegalTotal_TaxExclusiveTotalAmount()
		{
			Assert.AreEqual(_order.InvoiceSumExcludingVAT, _invoice.LegalTotal.TaxExclusiveTotalAmount.Value);
			Assert.AreEqual("SEK", _invoice.LegalTotal.TaxExclusiveTotalAmount.amountCurrencyID);
		}
	}

	[TestFixture]
	public class Parse_Invoice_Buyer_Party
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;

		[SetUp]
		public void Setup()
		{
			var company = Factory.GetCompany();
			_order = Factory.GetOrder(company);
			_settings = Factory.GetSettings();
		}

		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Address()
		{
			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);

			Assert.AreEqual(_order.ContractCompany.PostBox, invoice.BuyerParty.Party.Address.Postbox.Value);
			Assert.AreEqual(_order.ContractCompany.StreetName, invoice.BuyerParty.Party.Address.StreetName.Value);
			Assert.AreEqual(_order.ContractCompany.Zip, invoice.BuyerParty.Party.Address.PostalZone.Value);
			Assert.AreEqual(_order.ContractCompany.City, invoice.BuyerParty.Party.Address.CityName.Value);
			Assert.AreEqual(_order.CompanyUnit, invoice.BuyerParty.Party.Address.Department.Value);
			Assert.AreEqual(_order.ContractCompany.InvoiceCompanyName, invoice.BuyerParty.Party.PartyName[0].Value);
			Assert.AreEqual(_order.ContractCompany.OrganizationNumber, invoice.BuyerParty.Party.PartyIdentification[0].ID.Value);
			Assert.AreEqual(_order.CustomerFirstName + " " + _order.CustomerLastName, invoice.BuyerParty.Party.Contact.Name.Value);
			Assert.AreEqual(_order.Phone, invoice.BuyerParty.Party.Contact.Telephone.Value);
			Assert.AreEqual(_order.Email, invoice.BuyerParty.Party.Contact.ElectronicMail.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_VAT_Tax_Scheme()
		{
			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);

			var vatTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			Assert.IsNotNull(vatTaxScheme);
			Assert.AreEqual(_order.ContractCompany.TaxAccountingCode, vatTaxScheme.CompanyID.Value);
			Assert.AreEqual("VAT", vatTaxScheme.TaxScheme.ID.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_SWT_Tax_Scheme()
		{
			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);

			var swtTaxScheme = invoice.BuyerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			Assert.IsNotNull(swtTaxScheme);
			Assert.AreEqual(_order.ContractCompany.OrganizationNumber, swtTaxScheme.CompanyID.Value);
			Assert.AreEqual("SWT", swtTaxScheme.TaxScheme.ID.Value);
			Assert.AreEqual(_order.ContractCompany.City, swtTaxScheme.RegistrationAddress.CityName.Value);
			Assert.AreEqual(CountryIdentificationCodeContentType.SE, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value);
			Assert.AreEqual(_order.ContractCompany.Country.Name, swtTaxScheme.RegistrationAddress.Country.IdentificationCode.name);
		}

		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_FirstName_Missing() 
		{
			_order.CustomerFirstName = null;

			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);

			Assert.AreEqual(_order.CustomerLastName, invoice.BuyerParty.Party.Contact.Name.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_BuyerParty_Contact_With_LastName_Missing() 
		{
			_order.CustomerLastName = null;

			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);

			Assert.AreEqual(_order.CustomerFirstName, invoice.BuyerParty.Party.Contact.Name.Value);
		}

	}

	[TestFixture]
	public class Parse_Invoice_Seller_Party
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;
		private Shop _shop;
		private SFTIInvoiceType _invoice;

		[SetUp]
		public void Setup()
		{
			var company = Factory.GetCompany();
			_shop = Factory.GetShop();
			_order = Factory.GetOrder(company, _shop);
			_settings = Factory.GetSettings();
			_invoice = General.CreateInvoiceSvefaktura(_order, _settings);
		}

		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Address()
		{
			_invoice.SellerParty.Party.Address.Postbox.Value.ShouldBe(_shop.Address);
			_invoice.SellerParty.Party.Address.StreetName.Value.ShouldBe(_shop.Address2);
			_invoice.SellerParty.Party.Address.PostalZone.Value.ShouldBe(_shop.Zip);
			_invoice.SellerParty.Party.Address.CityName.Value.ShouldBe(_shop.City);
			_invoice.SellerParty.Party.Address.Country.IdentificationCode.Value.ShouldBe(CountryIdentificationCodeContentType.SE);
			_invoice.SellerParty.Party.Address.Country.IdentificationCode.name.ShouldBe("Sverige");
		}

		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyName() 
		{
			_invoice.SellerParty.Party.PartyName.First().Value.ShouldBe(_shop.Name);
		}

		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyIdentification() 
		{
			 _invoice.SellerParty.Party.PartyIdentification.First().ID.Value.ShouldBe(_shop.OrganizationNumber);
		}

		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_Contact() 
		{
			_invoice.SellerParty.Party.Contact.Name.Value.ShouldBe(_order.SellingShop.ContactCombinedName);
			_invoice.SellerParty.Party.Contact.ElectronicMail.Value.ShouldBe(_shop.Email);
			_invoice.SellerParty.Party.Contact.Telefax.Value.ShouldBe(_shop.Fax);
			_invoice.SellerParty.Party.Contact.Telephone.Value.ShouldBe(_shop.Phone);
		}

		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_AccountsContact()
		{
			_invoice.SellerParty.AccountsContact.ElectronicMail.Value.ShouldBe(_settings.Contact.ElectronicMail.Value);
			_invoice.SellerParty.AccountsContact.Name.Value.ShouldBe(_settings.Contact.Name.Value);
			_invoice.SellerParty.AccountsContact.Telefax.Value.ShouldBe(_settings.Contact.Telefax.Value);
			_invoice.SellerParty.AccountsContact.Telephone.Value.ShouldBe(_settings.Contact.Telephone.Value);
		}


		[Test]
		public void Test_Create_Invoice_Sets_SellerParty_PartyTaxSchemes_VAT() 
		{
		    var vatTaxScheme = _invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("VAT"));
			vatTaxScheme.TaxScheme.ID.Value.ShouldBe("VAT");
		    vatTaxScheme.CompanyID.Value.ShouldBe(_settings.TaxAccountingCode);
		    vatTaxScheme.RegistrationAddress.Postbox.Value.ShouldBe(_settings.Adress.Postbox.Value);
			vatTaxScheme.RegistrationAddress.StreetName.Value.ShouldBe(_settings.Adress.StreetName.Value);
		    vatTaxScheme.RegistrationAddress.CityName.Value.ShouldBe(_settings.Adress.CityName.Value);
		    vatTaxScheme.RegistrationAddress.PostalZone.Value.ShouldBe(_settings.Adress.PostalZone.Value);
		    vatTaxScheme.RegistrationAddress.Country.IdentificationCode.name.ShouldBe(_settings.Adress.Country.IdentificationCode.name);
			vatTaxScheme.RegistrationAddress.Country.IdentificationCode.Value.ShouldBe(_settings.Adress.Country.IdentificationCode.Value);
			vatTaxScheme.RegistrationName.Value.ShouldBe(_settings.SellingOrganizationName);
		}

		[Test]
		public void Test_Create_Invoice_Sets_Settings_PartyTaxSchemes_SWT() 
		{
		    var swtTaxScheme = _invoice.SellerParty.Party.PartyTaxScheme.Find(x => x.TaxScheme.ID.Value.Equals("SWT"));
			swtTaxScheme.TaxScheme.ID.Value.ShouldBe("SWT");
		    swtTaxScheme.CompanyID.Value.ShouldBe(_settings.SellingOrganizationNumber);
		    swtTaxScheme.ExemptionReason.Value.ShouldBe(_settings.ExemptionReason);
			swtTaxScheme.RegistrationAddress.CityName.Value.ShouldBe(_settings.RegistrationAdress.CityName.Value);
			swtTaxScheme.RegistrationAddress.Country.IdentificationCode.name.ShouldBe(_settings.RegistrationAdress.Country.IdentificationCode.name);
			swtTaxScheme.RegistrationAddress.Country.IdentificationCode.Value.ShouldBe(_settings.RegistrationAdress.Country.IdentificationCode.Value);

		}
	}

	[TestFixture]
	public class Parse_Invoice_Rows
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;
		private IEnumerable<OrderItem> _invoiceItems;

		[SetUp]
		public void Setup()
		{
			var company = Factory.GetCompany();
			var shop = Factory.GetShop();
			_invoiceItems = Factory.GetOrderItems();
			_order = Factory.GetOrder(company, shop, _invoiceItems);
			_settings = Factory.GetSettings();
		}

		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Properties() 
		{
		    var invoice = General.CreateInvoiceSvefaktura(_order, _settings);
			invoice.InvoiceLine.And(_invoiceItems).Do((invoiceLine, orderItem) =>
			{
				Assert.AreEqual(orderItem.ArticleDisplayName, invoiceLine.Item.Description.Value);
				Assert.AreEqual(orderItem.ArticleDisplayNumber, invoiceLine.Item.SellersItemIdentification.ID.Value);
				Assert.AreEqual(orderItem.SinglePrice, invoiceLine.Item.BasePrice.PriceAmount.Value);
				Assert.AreEqual("SEK", invoiceLine.Item.BasePrice.PriceAmount.amountCurrencyID);
				Assert.AreEqual(orderItem.NumberOfItems, invoiceLine.InvoicedQuantity.Value);
				Assert.AreEqual("styck", invoiceLine.InvoicedQuantity.quantityUnitCode);
				Assert.AreEqual(orderItem.DisplayTotalPrice, invoiceLine.LineExtensionAmount.Value);
				Assert.AreEqual("SEK", invoiceLine.LineExtensionAmount.amountCurrencyID);
			});
		}

		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_Item_TaxCategory() 
		{
			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);
			Func<OrderItem,string> getExpectedTaxCategory = orderItem => orderItem.NoVAT ? "E" : "S";
			Func<OrderItem,decimal> getExpectedVatPercent = orderItem => orderItem.NoVAT ? 0 : (_settings.VATAmount * 100);
			invoice.InvoiceLine.And(_invoiceItems).Do((invoiceLine, orderItem) =>
			{
				Assert.AreEqual(getExpectedTaxCategory(orderItem), invoiceLine.Item.TaxCategory.First().ID.Value);
				Assert.AreEqual(getExpectedVatPercent(orderItem), invoiceLine.Item.TaxCategory.First().Percent.Value);
				Assert.AreEqual("VAT", invoiceLine.Item.TaxCategory.First().TaxScheme.ID.Value);
			});
		}

		[Test]
		public void Test_Create_Invoice_Sets_InvoiceLine_ID() 
		{
			var invoice = General.CreateInvoiceSvefaktura(_order, _settings);
			invoice.InvoiceLine.ForEach().DoWithIndex((index, invoiceLine) =>
			{
				var expectedInvoiceLineID = (index + 1).ToString();
				Assert.IsNotNull(invoiceLine.ID);
				Assert.AreEqual(expectedInvoiceLineID, invoiceLine.ID.Value);
			});
		}
	}

	[TestFixture]
	public class Parse_Invoice_Payment_Means
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;
		private SFTIInvoiceType _invoice;
		private Company _company;

		[SetUp]
		public void Setup()
		{
			_company = Factory.GetCompany();
			_order = Factory.GetOrder(_company);
			_settings = Factory.GetSettings();
			_invoice = General.CreateInvoiceSvefaktura(_order, _settings);
		}

		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution_BankGiro() 
		{
			var paymentMeans = _invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "BGABSESS");
			Assert.AreEqual("BGABSESS", paymentMeans.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
			Assert.AreEqual("56936677", paymentMeans.PayeeFinancialAccount.ID.Value);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, paymentMeans.PaymentMeansTypeCode.Value);
			Assert.AreEqual(_settings.InvoiceIssueDate.AddDays(_company.PaymentDuePeriod), _invoice.PaymentMeans[0].DuePaymentDate.Value);
		}
		[Test]
		public void Test_Create_Invoice_Sets_PaymentMeans_FinancialInstitution_PostGiro() 
		{
			var paymentMeans = _invoice.PaymentMeans.Find(x => x.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value == "PGSISESS");
			Assert.AreEqual("PGSISESS", paymentMeans.PayeeFinancialAccount.FinancialInstitutionBranch.FinancialInstitution.ID.Value);
			Assert.AreEqual("123456", paymentMeans.PayeeFinancialAccount.ID.Value);
			Assert.AreEqual(PaymentMeansCodeContentType.Item1, paymentMeans.PaymentMeansTypeCode.Value);
			Assert.AreEqual(_settings.InvoiceIssueDate.AddDays(_company.PaymentDuePeriod), _invoice.PaymentMeans[0].DuePaymentDate.Value);
		}
	}

	[TestFixture]
	public class Parse_Invoice_Payment_Terms
	{
		private SvefakturaConversionSettings _settings;
		private SFTIInvoiceType _invoice;
		private Company _company;

		[SetUp]
		public void Setup()
		{
			_company = Factory.GetCompany();
			var order = Factory.GetOrder(_company);
			_settings = Factory.GetSettings();
			_invoice = General.CreateInvoiceSvefaktura(order, _settings);
		}

		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoicePaymentTermsTextFormat()
		{
			var expectedText = _settings.InvoicePaymentTermsTextFormat.Replace("{InvoiceNumberOfDueDays}", _company.PaymentDuePeriod.ToString());
			Assert.AreEqual(expectedText, _invoice.PaymentTerms.Note.Value);
		}

		[Test]
		public void Test_Create_Invoice_Sets_PaymentTerms_InvoiceExpieryPenaltySurcharge() 
		{
			Assert.AreEqual(_settings.InvoiceExpieryPenaltySurchargePercent, _invoice.PaymentTerms.PenaltySurchargePercent.Value);
		}
	}

	[TestFixture]
	public class Parse_Tax_Totals
	{
		private Order _order;
		private SvefakturaConversionSettings _settings;
		private IEnumerable<OrderItem> _invoiceItems;
		private SFTIInvoiceType _invoice;

		[SetUp]
		public void Setup()
		{
			var company = Factory.GetCompany();
			var shop = Factory.GetShop();
			_invoiceItems = Factory.GetOrderItems();
			_order = Factory.GetOrder(company, shop, _invoiceItems);
			_settings = Factory.GetSettings();
			_invoice = General.CreateInvoiceSvefaktura(_order,  _settings);
		}

		[Test]
		public void Parse_TotalTaxAmount()
		{
			var tax = _invoiceItems.Where(x => !x.NoVAT).Sum(x => x.DisplayTotalPrice) * (float)_settings.VATAmount;
			Assert.AreEqual(tax, _invoice.TaxTotal[0].TotalTaxAmount.Value);
			Assert.AreEqual("SEK", _invoice.TaxTotal[0].TotalTaxAmount.amountCurrencyID);
		}

		[Test]
		public void Parse_TaxFree_TaxCategory() 
		{
			var taxCategoryE = _invoice.TaxTotal.First().TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("E"));
			Assert.IsNotNull(taxCategoryE);
			Assert.AreEqual("E", taxCategoryE.TaxCategory.ID.Value);
			Assert.AreEqual(0m, taxCategoryE.TaxCategory.Percent.Value);
			Assert.AreEqual("VAT", taxCategoryE.TaxCategory.TaxScheme.ID.Value);
		}

		[Test]
		public void Parse_StandardTax_TaxCategory() 
		{
			var taxCategoryS = _invoice.TaxTotal.First().TaxSubTotal.Find(x => x.TaxCategory.ID.Value.Equals("S"));
			Assert.IsNotNull(taxCategoryS);
			Assert.AreEqual("S", taxCategoryS.TaxCategory.ID.Value);
			Assert.AreEqual(25.00m, taxCategoryS.TaxCategory.Percent.Value);
			Assert.AreEqual("VAT", taxCategoryS.TaxCategory.TaxScheme.ID.Value);
		}

	}
}