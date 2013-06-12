using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
	[TestFixture]
	public class TestSvefakturaFormatting : SvefakturaTestBase
    {
		private readonly Order _emptyOrder = new Order
		{
		    ContractCompany = new Company { InvoiceFreeTextFormat = string.Empty }, 
            SellingShop = new Shop(), 
            OrderItems = new List<OrderItem>()
		};
		private readonly Shop _emptyShop = new Shop();
		private readonly List<OrderItem> _emptyOrderItems = new List<OrderItem>();
	    private readonly Company _emptyCompany = new Company { InvoiceFreeTextFormat = string.Empty };

		[SetUp]
		public void Setup() { }

		[Test]
		public void Test_Telephone_Formatting_With_Country_Code()
		{
		    var customOrder = new Order
		    {
		        SellingShop = _emptyShop,
		        OrderItems = _emptyOrderItems,
		        ContractCompany = _emptyCompany,
		        Phone = "+46 (0) 123 - 456789"
		    };
		    var invoice = BuildInvoice<BuyerPartyBuilder>(customOrder);
			invoice.BuyerParty.Party.Contact.Telephone.Value.ShouldBe("+46123456789");
		}

		[Test]
		public void Test_Telephone_Formatting_Without_Country_Code()
		{
		    var customOrder = new Order
		    {
		        SellingShop = _emptyShop,
		        OrderItems = _emptyOrderItems,
		        ContractCompany = _emptyCompany,
		        Phone = "0123 - 456789"
		    };
            var invoice = BuildInvoice<BuyerPartyBuilder>(customOrder);
			invoice.BuyerParty.Party.Contact.Telephone.Value.ShouldBe("0123456789");
		}

		[Test]
		public void Test_Fax_Formatting_With_Country_Code()
		{
		    Settings = new SvefakturaConversionSettings
		    {
		        Contact = new SFTIContactType { Telefax = new TelefaxType { Value = "+46 (0) 123 - 456789" } }
		    };
		    var invoice = BuildInvoice<SellerPartyBuilder>(_emptyOrder);
			invoice.SellerParty.AccountsContact.Telefax.Value.ShouldBe("+46123456789");
		}

		[Test]
		public void Test_Fax_Formatting_Without_Country_Code()
		{
		    Settings = new SvefakturaConversionSettings
		    {
		        Contact = new SFTIContactType { Telefax = new TelefaxType { Value = "0123 - 456789" } }
		    };
            var invoice = BuildInvoice<SellerPartyBuilder>(_emptyOrder);
			invoice.SellerParty.AccountsContact.Telefax.Value.ShouldBe("0123456789");
		}

		[Test]
		public void Test_PostGiro_Formatting()
		{
            Settings = new SvefakturaConversionSettings
            {
                InvoiceIssueDate = DateTime.Now,
                Postgiro = "SE 555-654.123 - 645",
                PostgiroBankIdentificationCode = "TEST"
            };
		    var invoice = BuildInvoice<PaymentMeansBuilder>(_emptyOrder);
			invoice.PaymentMeans.First().PayeeFinancialAccount.ID.Value.ShouldBe("SE555654123645");
		}

		[Test]
		public void Test_BankGiro_Formatting()
		{
		    Settings = new SvefakturaConversionSettings
		    {
		        InvoiceIssueDate = DateTime.Now,
		        BankGiro = "SE 555-654.123 - 645",
		        BankgiroBankIdentificationCode = "TEST"
		    };

		    var invoice = BuildInvoice<PaymentMeansBuilder>(_emptyOrder);
			invoice.PaymentMeans.First().PayeeFinancialAccount.ID.Value.ShouldBe("SE555654123645");
		}

		[Test]
		public void Test_TaxAccountingCode_Formatting_Seller()
		{
		    Settings = new SvefakturaConversionSettings { TaxAccountingCode = "SE 555-654.123 - 645" };
		    var invoice = BuildInvoice<SellerPartyBuilder>(_emptyOrder);
			invoice.SellerParty.Party.PartyTaxScheme[0].CompanyID.Value.ShouldBe("SE555654123645");
		}

		[Test]
		public void Test_TaxAccountingCode_Formatting_Buyer()
		{
		    var customOrder = new Order
		    {
		        SellingShop = _emptyShop,
		        OrderItems = _emptyOrderItems,
		        ContractCompany = new Company
		        {
		            TaxAccountingCode = "SE 555-654.123 - 645", 
                    InvoiceFreeTextFormat = string.Empty
		        }
		    };
		    var invoice = BuildInvoice<BuyerPartyBuilder>(customOrder);
			invoice.BuyerParty.Party.PartyTaxScheme[0].CompanyID.Value.ShouldBe("SE555654123645");
		}

		[Test]
		public void Test_OrganizationNumber_Formatting_Seller()
		{
		    Settings = new SvefakturaConversionSettings
		    {
		        TaxAccountingCode = "ABC",
		        SellingOrganizationNumber = "SE 555-654.123 - 645"
		    };
		    _emptyOrder.SellingShop = new Shop { OrganizationNumber = "SE 555-654.123 - 987" };

		    var invoice = BuildInvoice<SellerPartyBuilder>(_emptyOrder);
			invoice.SellerParty.Party.PartyTaxScheme[1].CompanyID.Value.ShouldBe("SE555654123645");
            invoice.SellerParty.Party.PartyIdentification[0].ID.Value.ShouldBe("SE555654123987");
		}

		[Test]
		public void Test_OrganizationNumber_Formatting_Buyer()
		{
		    var customOrder = new Order
		    {
		        SellingShop = _emptyShop,
		        OrderItems = _emptyOrderItems,
		        ContractCompany = new Company
		        {
		            TaxAccountingCode = "ABC",
		            OrganizationNumber = "SE 555-654.123 - 645",
		            InvoiceFreeTextFormat = string.Empty
		        }
		    };
		    var invoice = BuildInvoice<BuyerPartyBuilder>(customOrder);
			invoice.BuyerParty.Party.PartyTaxScheme[1].CompanyID.Value.ShouldBe("SE555654123645");
		    invoice.BuyerParty.Party.PartyIdentification[0].ID.Value.ShouldBe("SE555654123645");
		}
	}
}