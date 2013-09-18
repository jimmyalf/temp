using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using PercentType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;
using QuantityType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class InvoiceLinesBuilder : PartBuilderBase, ISvefakturaPartBuilder
    {
        public InvoiceLinesBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter)
            : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            invoice.InvoiceLine = order.OrderItems.Select(Convert).ToList();
            invoice.LineItemCountNumeric = new LineItemCountNumericType { Value = invoice.InvoiceLine.Count };
            invoice.TaxTotal = GetTaxTotal(invoice);
            invoice.LegalTotal = GetLegalTotal(invoice.TaxTotal, order);
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

        protected virtual SFTILegalTotalType GetLegalTotal(List<SFTITaxTotalType> taxTotals, IOrder order)
        {
            if (order.InvoiceSumIncludingVAT <= 0 && order.InvoiceSumExcludingVAT <= 0)
            {
                return null;
            }

            var taxTotal = taxTotals.Where(x => x.TotalTaxAmount != null).Sum(x => x.TotalTaxAmount.Value);
            var legalTotal = new SFTILegalTotalType
            {
                LineExtensionTotalAmount = GetLineExtensionAmount(order),
                TaxExclusiveTotalAmount = GetAmountInSEK<TotalAmountType>(order.InvoiceSumExcludingVAT),
                TaxInclusiveTotalAmount = GetAmountInSEK<TotalAmountType>(order.InvoiceSumIncludingVAT),
            };

            var roundOff = legalTotal.TaxInclusiveTotalAmount.Value - (taxTotal + legalTotal.TaxExclusiveTotalAmount.Value);
            if (roundOff != 0)
            {
                legalTotal.RoundOffAmount = GetAmountInSEK<AmountType>(roundOff);
            }

            return legalTotal;
        }

        protected virtual ExtensionTotalAmountType GetLineExtensionAmount(IOrder order)
        {
            var result = (decimal)order.OrderItems.Sum(x => x.DisplayTotalPrice);
            return (result <= 0) ? null : GetAmountInSEK<ExtensionTotalAmountType>(result);
        }

        protected virtual List<SFTITaxTotalType> GetTaxTotal(SFTIInvoiceType invoice)
        {
            var subtotals = GetTaxSubTotals(invoice);
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

        protected virtual List<SFTITaxSubTotalType> GetTaxSubTotals(SFTIInvoiceType invoice)
        {
            var joinedLists = new List<SFTITaxSubTotalType>();
            var allowanceChargeSubTotals = GetTaxSubTotals(invoice.AllowanceCharge, x => x.TaxCategory, GetTaxableAmount, GetTaxAmount);
            var invoiceLinesSubTotals = GetTaxSubTotals(invoice.InvoiceLine, x => x.Item.TaxCategory, GetTaxableAmount, GetTaxAmount);
            joinedLists.AddRange(allowanceChargeSubTotals);
            joinedLists.AddRange(invoiceLinesSubTotals);
            return joinedLists.GroupBy(x => x.TaxCategory.ID.Value).Select(g => new SFTITaxSubTotalType
            {
                TaxCategory = g.First().TaxCategory,
                TaxableAmount = new AmountType { Value = g.Sum(p => p.TaxableAmount.Value), amountCurrencyID = "SEK" },
                TaxAmount = new TaxAmountType { Value = g.Sum(p => p.TaxAmount.Value), amountCurrencyID = "SEK" }
            }).ToList();
        }

        protected virtual List<SFTITaxSubTotalType> GetTaxSubTotals<T>(List<T> items, Func<T, List<SFTITaxCategoryType>> getTaxCategories, Func<T, decimal> taxableAmount, Func<T, decimal> taxAmount)
        {
            if (items == null)
            {
                return new List<SFTITaxSubTotalType>();
            }

            return items.GroupBy(getTaxCategories).Select(g => new SFTITaxSubTotalType
            {
                TaxCategory = g.Key[0],
                TaxableAmount = new AmountType { Value = g.Sum(taxableAmount), amountCurrencyID = "SEK" },
                TaxAmount = new TaxAmountType { Value = g.Sum(taxAmount), amountCurrencyID = "SEK" }
            }).ToList();            
        }

        protected virtual decimal GetTaxAmount(SFTIInvoiceLineType invoiceLine)
        {
            var taxableAmount = GetTaxableAmount(invoiceLine);
            return taxableAmount * (GetTaxPercent(invoiceLine, x => x.Item.TaxCategory) / 100);
        }

        protected virtual decimal GetTaxAmount(SFTIAllowanceChargeType allowanceCharge)
        {
            var taxableAmount = GetTaxableAmount(allowanceCharge);
            return taxableAmount * (GetTaxPercent(allowanceCharge, x => x.TaxCategory) / 100);
        }

        protected virtual decimal GetTaxPercent<T>(T item, Func<T, List<SFTITaxCategoryType>> getTaxCategories) where T : class 
        {
            if (item == null)
            {
                return Settings.VATAmount;
            }

            return getTaxCategories(item)
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

        protected virtual decimal GetTaxableAmount(SFTIAllowanceChargeType allowanceCharge)
        {
            decimal returnValue = 0;
            if (allowanceCharge.Amount != null)
            {
                returnValue = GetSignedAllowanceChargeAmount(allowanceCharge);
            }

            return returnValue;
        }

        protected virtual decimal GetSignedAllowanceChargeAmount(SFTIAllowanceChargeType allowanceCharge)
        {
            if (allowanceCharge == null || allowanceCharge.ChargeIndicator == null || allowanceCharge.Amount == null)
            {
                return 0;
            }

            return allowanceCharge.ChargeIndicator.Value
                       ? allowanceCharge.Amount.Value
                       : (allowanceCharge.Amount.Value * -1);
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