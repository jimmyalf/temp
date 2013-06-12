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
    public class InvoiceInformationBuilder : SvefakturaBuilder, ISvefakturaBuilder
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
            invoice.AdditionalDocumentReference = GetAdditionalDocumentReference(order);
		    
            invoice.RequisitionistDocumentReference = GetRequisitionDocumentReference(order);
            invoice.InvoiceCurrencyCode = new CurrencyCodeType { Value = Settings.InvoiceCurrencyCode.GetValueOrDefault() };
            invoice.TaxPointDate = new TaxPointDateType { Value = order.CreatedDate };
        }

        protected virtual List<SFTIDocumentReferenceType> GetRequisitionDocumentReference(IOrder order)
        {
            if (string.IsNullOrEmpty(order.CustomerOrderNumber))
            {
                return null;
            }

            return new List<SFTIDocumentReferenceType>
            {
                new SFTIDocumentReferenceType { ID = new IdentifierType { Value = order.CustomerOrderNumber } }
            };        
        }

        protected virtual List<SFTIDocumentReferenceType> GetAdditionalDocumentReference(IOrder order)
        {
            return new List<SFTIDocumentReferenceType>
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
        }
    }
}