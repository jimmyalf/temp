using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
    [TestFixture]
    public class Parse_Invoice_Rows : SvefakturaTestBase
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
            _invoice = BuildInvoice<InvoiceLinesBuilder>(_order);
        }

        [Test]
        public void Test_Create_Invoice_Sets_InvoiceLine_Properties() 
        {
            _invoice.InvoiceLine.And(_invoiceItems).Do((invoiceLine, orderItem) =>
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
            Func<OrderItem, string> getExpectedTaxCategory = orderItem => orderItem.NoVAT ? "E" : "S";
            Func<OrderItem, decimal> getExpectedVatPercent = orderItem => orderItem.NoVAT ? 0 : (_settings.VATAmount * 100);
            _invoice.InvoiceLine.And(_invoiceItems).Do((invoiceLine, orderItem) =>
            {
                Assert.AreEqual(getExpectedTaxCategory(orderItem), invoiceLine.Item.TaxCategory.First().ID.Value);
                Assert.AreEqual(getExpectedVatPercent(orderItem), invoiceLine.Item.TaxCategory.First().Percent.Value);
                Assert.AreEqual("VAT", invoiceLine.Item.TaxCategory.First().TaxScheme.ID.Value);
            });
        }

        [Test]
        public void Test_Create_Invoice_Sets_InvoiceLine_ID() 
        {
            _invoice.InvoiceLine.ForEach().DoWithIndex((index, invoiceLine) =>
            {
                var expectedInvoiceLineId = (index + 1).ToString();
                Assert.IsNotNull(invoiceLine.ID);
                Assert.AreEqual(expectedInvoiceLineId, invoiceLine.ID.Value);
            });
        }

        [Test]
        public void Test_Create_Invoice_Sets_LineItemCountNumeric()
        {
            Assert.AreEqual(_order.OrderItems.Count, _invoice.LineItemCountNumeric.Value);
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

        [Test]
        public void Parse_TotalTaxAmount()
        {
            var tax = _invoiceItems.Where(x => !x.NoVAT).Sum(x => x.DisplayTotalPrice) * (float)Settings.VATAmount;
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