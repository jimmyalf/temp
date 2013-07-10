using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class PaymentTermsBuilder : PartBuilderBase, ISvefakturaPartBuilder
    {
        public PaymentTermsBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter)
            : base(settings, formatter) { }

        public void Build(IOrder order, SFTIInvoiceType invoice)
        {
			invoice.PaymentTerms = new SFTIPaymentTermsType 
			{
                Note = GetNote(Settings, order),
            	PenaltySurchargePercent = new SurchargePercentType
				{
					Value = Settings.InvoiceExpieryPenaltySurchargePercent.GetValueOrDefault()
				}
            };
        }

        protected virtual NoteType GetNote(ISvefakturaConversionSettings settings, IOrder order)
        {
            var text = ParseInvoicePaymentTermsFormat(Settings.InvoicePaymentTermsTextFormat, order.ContractCompany);
            return GetTextEntity<NoteType>(text);
        }

        protected virtual string ParseInvoicePaymentTermsFormat(string format, ICompany company)
        {
            if (format == null || company == null)
            {
                return null;
            }

            return format.Replace("{InvoiceNumberOfDueDays}", company.PaymentDuePeriod.ToString());
        }
    }
}