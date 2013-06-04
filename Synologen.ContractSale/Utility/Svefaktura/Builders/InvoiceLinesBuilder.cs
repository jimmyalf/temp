using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class InvoiceLinesBuilder : ISvefakturaBuilder
    {
        //TryAddInvoiceLines(settings, invoice, order.OrderItems, settings.VATAmount);
        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            throw new NotImplementedException();
            /*			
             * if(invoice.InvoiceLine == null) invoice.InvoiceLine = new List<SFTIInvoiceLineType>();
			var lineItemCount = 0;
			foreach (var orderItem in orderItems){
				lineItemCount++;
				invoice.InvoiceLine.Add(
					new SFTIInvoiceLineType
					{
						Item = new SFTIItemType
						{
							Description = TryGetValue(orderItem.ArticleDisplayName, new DescriptionType {Value = orderItem.ArticleDisplayName}),
							SellersItemIdentification = TryGetValue(orderItem.ArticleDisplayNumber, new SFTIItemIdentificationType {ID = new IdentifierType {Value = orderItem.ArticleDisplayNumber}}),
							BasePrice = new SFTIBasePriceType {
							                                  	PriceAmount = TryGetValue(orderItem.SinglePrice, new PriceAmountType {Value = (decimal) orderItem.SinglePrice, amountCurrencyID = "SEK"})
							                                  },
							TaxCategory = new List<SFTITaxCategoryType> 
							{
								(orderItem.NoVAT) 
									?  GetTaxCategory("E", 0, "VAT", settings.VATFreeReasonMessage) 
									: GetTaxCategory("S", VATAmount*100, "VAT", settings.VATFreeReasonMessage)
							}
						},
						InvoicedQuantity = new QuantityType{Value = orderItem.NumberOfItems, quantityUnitCode = "styck"},
						LineExtensionAmount = new ExtensionAmountType { Value = (decimal) orderItem.DisplayTotalPrice, amountCurrencyID="SEK" },
						ID = new SFTISimpleIdentifierType{Value = lineItemCount.ToString()},
						Note = TryGetValue(orderItem.Notes, new NoteType{Value=orderItem.Notes})
					}
					);
			}
			invoice.LineItemCountNumeric = (lineItemCount <= 0) ? null : new LineItemCountNumericType {Value = lineItemCount};
			TryAddTaxTotal(invoice, settings);*/
        }
    }
}