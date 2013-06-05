using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using PercentType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;
using QuantityType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class InvoiceLinesBuilder : SvefakturaBuilderBase, ISvefakturaBuilder
    {
        public InvoiceLinesBuilder(SvefakturaConversionSettings settings, SvefakturaFormatter formatter)
            : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.InvoiceLine = order.OrderItems.Select(Convert).ToList();
            invoice.LineItemCountNumeric = GetLineCount(invoice.InvoiceLine.Count + 1);
            invoice.TaxTotal = GetTaxTotal(invoice.InvoiceLine);
        }

        public SFTIInvoiceLineType Convert(IOrderItem orderItem, int index)
        {
            return new SFTIInvoiceLineType
            {
                Item = GetItem(orderItem),
                InvoicedQuantity = new QuantityType { Value = orderItem.NumberOfItems, quantityUnitCode = "styck" },
                LineExtensionAmount = new ExtensionAmountType { Value = (decimal)orderItem.DisplayTotalPrice, amountCurrencyID = "SEK" },
                ID = GetItemId(index + 1),
                Note = GetTextEntity<NoteType>(orderItem.Notes)
            };            
        }

        protected virtual List<SFTITaxTotalType> GetTaxTotal(List<SFTIInvoiceLineType> invoiceLines)
        {
            var subtotals = GetTaxSubTotals(invoiceLines);
            var totalTaxAmount = subtotals.Sum(x => x.TaxAmount.Value);
            return new List<SFTITaxTotalType>
            {
                new SFTITaxTotalType
                {
                    TaxSubTotal = subtotals,
                    TotalTaxAmount = new TaxAmountType { Value = totalTaxAmount, amountCurrencyID = "SEK" }
                }
            };
        }

        protected virtual List<SFTITaxSubTotalType> GetTaxSubTotals(IEnumerable<SFTIInvoiceLineType> invoiceLines)
        {
            if (invoiceLines == null)
            {
                return new List<SFTITaxSubTotalType>();
            }

            return new List<SFTITaxSubTotalType>(
                from p in invoiceLines
                group p by p.Item.TaxCategory
                into g
                select
                    new SFTITaxSubTotalType
                    {
                        TaxCategory = g.Key[0],
                        TaxableAmount = new AmountType { Value = g.Sum(p => GetTaxableAmount(p)), amountCurrencyID = "SEK" },
                        TaxAmount = new TaxAmountType { Value = g.Sum(p => GetTaxAmount(p)), amountCurrencyID = "SEK" }
                    });
        }

        protected virtual decimal GetTaxAmount(SFTIInvoiceLineType invoiceLine)
        {
            var taxableAmount = GetTaxableAmount(invoiceLine);
            return taxableAmount * (GetInvoiceLineTaxPercent(invoiceLine) / 100);
        }

        protected virtual decimal GetInvoiceLineTaxPercent(SFTIInvoiceLineType invoiceLine)
        {
            return invoiceLine
                .With(x => x.Item)
                .With(x => x.TaxCategory)
                .With(x => x.FirstOrDefault())
                .Return(x => x.Percent.Value, Settings.VATAmount);
        }

        protected virtual decimal GetTaxableAmount(SFTIInvoiceLineType invoiceLine)
        {
            decimal returnValue = 0;

            if (invoiceLine.LineExtensionAmount != null)
            {
                returnValue += invoiceLine.LineExtensionAmount.Value;
            }

            return returnValue;
        }

        protected virtual LineItemCountNumericType GetLineCount(int count)
        {
            return count <= 0 ? null : new LineItemCountNumericType { Value = count };
        }

        protected virtual SFTISimpleIdentifierType GetItemId(int count)
        {
            return new SFTISimpleIdentifierType { Value = count.ToString() };
        }

        protected virtual SFTIItemType GetItem(IOrderItem orderItem)
        {
            return new SFTIItemType
            {
                Description = new DescriptionType { Value = orderItem.ArticleDisplayName },
                SellersItemIdentification = GetSellerId(orderItem),
                BasePrice = GetBasePrice(orderItem),
                TaxCategory = GetTaxCategory(orderItem)
            };
        }

        protected virtual SFTIItemIdentificationType GetSellerId(IOrderItem orderItem)
        {
            return new SFTIItemIdentificationType { ID = new IdentifierType { Value = orderItem.ArticleDisplayNumber } };
        }

        protected virtual SFTIBasePriceType GetBasePrice(IOrderItem orderItem)
        {
            return new SFTIBasePriceType
            {
                PriceAmount = new PriceAmountType
                {
                    Value = (decimal)orderItem.SinglePrice,
                    amountCurrencyID = "SEK"
                }
            };
        }

        protected virtual List<SFTITaxCategoryType> GetTaxCategory(IOrderItem orderItem)
        {
            if (orderItem.NoVAT)
            {
                return new List<SFTITaxCategoryType>
                {
                    new SFTITaxCategoryType
                    {
                        ID = new IdentifierType { Value = "E" },
                        Percent = new PercentType { Value = 0 },
                        TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "VAT" } },
                        ExemptionReason = new ReasonType { Value = Settings.VATFreeReasonMessage }
                    }
                };
            }

            return new List<SFTITaxCategoryType>
            {
                new SFTITaxCategoryType
                {
                    ID = new IdentifierType { Value = "S" },
                    Percent = new PercentType { Value = Settings.VATAmount * 100 },
                    TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "VAT" } },
                }
            };
        }
    }
}