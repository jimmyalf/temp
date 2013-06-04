using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Builders
{
    public class PaymentTermsBuilder : ISvefakturaBuilder
    {
        //TryAddPaymentTerms(invoice, settings, order.ContractCompany);
        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
            throw new NotImplementedException();
            /*
            if (AllAreNullOrEmpty(settings.InvoicePaymentTermsTextFormat, settings.InvoiceExpieryPenaltySurchargePercent)) return;
			var text = ParseInvoicePaymentTermsFormat(settings.InvoicePaymentTermsTextFormat, company);
			invoice.PaymentTerms = new SFTIPaymentTermsType 
			{
            	Note = TryGetValue(text, new NoteType {Value = text}),
            	PenaltySurchargePercent = TryGetValue(
					settings.InvoiceExpieryPenaltySurchargePercent, 
					new SurchargePercentType
					{
						Value = settings.InvoiceExpieryPenaltySurchargePercent.GetValueOrDefault()
					})
            };
             */
        }
    }
}