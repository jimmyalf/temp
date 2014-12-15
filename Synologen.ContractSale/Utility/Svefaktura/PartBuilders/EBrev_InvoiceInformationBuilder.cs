using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class EBrev_InvoiceInformationBuilder : InvoiceInformationBuilder
    {
        public EBrev_InvoiceInformationBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter)
            : base(settings, formatter) { }

        protected override string GetFreeText(IOrder order)
        {
            return order.ParseFreeText();
        }

        protected override List<SFTIDocumentReferenceType> GetRequisitionDocumentReference(IOrder order)
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

        protected override List<SFTIDocumentReferenceType> GetAdditionalDocumentReference(IOrder order)
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