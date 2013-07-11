using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class EBrev_InvoiceInformationBuilder : InvoiceInformationBuilder, ISvefakturaPartBuilder
    {
        public EBrev_InvoiceInformationBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter)
            : base(settings, formatter) { }

        protected override string GetFreeText(IOrder order)
        {
            return order.ParseFreeText();
        }
    }
}