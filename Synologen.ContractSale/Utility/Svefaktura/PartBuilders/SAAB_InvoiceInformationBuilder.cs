using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class SAAB_InvoiceInformationBuilder : InvoiceInformationBuilder
    {
        public SAAB_InvoiceInformationBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter) 
            : base(settings, formatter) { }

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


    }
}