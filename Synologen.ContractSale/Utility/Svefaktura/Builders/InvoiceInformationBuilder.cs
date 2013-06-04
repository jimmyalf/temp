using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using CodeType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes.CodeType;
using IdentifierType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes.IdentifierType;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class InvoiceInformationBuilder : SvefakturaBuilderBase, ISvefakturaBuilder
    {
        public InvoiceInformationBuilder(SvefakturaConversionSettings settings, SvefakturaFormatter formatter)
            : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
			var freeTextRows = order.ParseFreeText();

			invoice.Note = GetTextEntity<NoteType>(freeTextRows);
            invoice.IssueDate = new IssueDateType { Value = Settings.InvoiceIssueDate };
            invoice.InvoiceTypeCode = new CodeType { Value = Settings.InvoiceTypeCode };
            invoice.ID = new SFTISimpleIdentifierType { Value = order.InvoiceNumber.ToString() };
			invoice.AdditionalDocumentReference = new List<SFTIDocumentReferenceType>
			{
			    new SFTIDocumentReferenceType
			    {
			        ID = new IdentifierType
			        {
			            Value = order.ContractCompany.Id.ToString(), 
                        identificationSchemeAgencyName = "SFTI", 
                        identificationSchemeID = "ACD"
			        }
			    }
			};

			if (order.InvoiceSumIncludingVAT > 0 || order.InvoiceSumExcludingVAT > 0)
			{
				invoice.LegalTotal = GetLegalTotal(invoice, order);
			}

            if (!string.IsNullOrEmpty(order.CustomerOrderNumber))
            {
                invoice.RequisitionistDocumentReference = new List<SFTIDocumentReferenceType>
                {
                    new SFTIDocumentReferenceType { ID = new IdentifierType { Value = order.CustomerOrderNumber } }
                };
            }

            invoice.InvoiceCurrencyCode = new CurrencyCodeType
            {
                Value = Settings.InvoiceCurrencyCode.GetValueOrDefault()
            };
            invoice.TaxPointDate = new TaxPointDateType { Value = order.CreatedDate };
        }

        protected virtual SFTILegalTotalType GetLegalTotal(SFTIInvoiceType invoice, IOrder order)
        {
            var taxTotal = invoice.TaxTotal.Where(x => x.TotalTaxAmount != null).Sum(x => x.TotalTaxAmount.Value);
            var legalTotal = new SFTILegalTotalType
            {
                LineExtensionTotalAmount = GetLineExtensionAmount(order),
                TaxExclusiveTotalAmount = new TotalAmountType
                {
                    Value = (decimal)order.InvoiceSumExcludingVAT, amountCurrencyID = "SEK"
                },
                TaxInclusiveTotalAmount = new TotalAmountType
                {
                    Value = (decimal)order.InvoiceSumIncludingVAT, amountCurrencyID = "SEK"
                },
            };

            var roundOff = legalTotal.TaxInclusiveTotalAmount.Value - (taxTotal + legalTotal.TaxExclusiveTotalAmount.Value);
            if (roundOff != 0)
            {
                legalTotal.RoundOffAmount = new AmountType { Value = roundOff };
            }

            return legalTotal;
        }

        protected virtual ExtensionTotalAmountType GetLineExtensionAmount(IOrder order)
        {
            var result = (decimal)order.OrderItems.Sum(x => x.DisplayTotalPrice);
            return (result <= 0) ? null : new ExtensionTotalAmountType
            {
                Value = result,
                amountCurrencyID = "SEK",
            };
        }
    }
}