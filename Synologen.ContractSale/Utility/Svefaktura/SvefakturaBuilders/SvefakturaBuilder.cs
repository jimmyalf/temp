using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders
{
    public class SvefakturaBuilder : ISvefakturaBuilder
    {
        private readonly ISvefakturaFormatter _formatter;
        private readonly ISvefakturaConversionSettings _settings;
        private readonly ISvefakturaBuilderValidator _builderValidator;

        public SvefakturaBuilder(ISvefakturaFormatter formatter, ISvefakturaConversionSettings settings, ISvefakturaBuilderValidator builderValidator)
        {
            _formatter = formatter;
            _settings = settings;
            _builderValidator = builderValidator;
        }

        public SFTIInvoiceType Build(IOrder order)
        {
            _builderValidator.Validate(order);
            _builderValidator.Validate(_settings);

            var invoice = new SFTIInvoiceType();
            new SellerPartyBuilder(_settings, _formatter).Build(order, invoice);
            new BuyerPartyBuilder(_settings, _formatter).Build(order, invoice);
            new PaymentMeansBuilder(_settings, _formatter).Build(order, invoice);
            new InvoiceLinesBuilder(_settings, _formatter).Build(order, invoice);
            new InvoiceInformationBuilder(_settings, _formatter).Build(order, invoice);
            new PaymentTermsBuilder(_settings, _formatter).Build(order, invoice);
            return invoice;
        }        
    }
}