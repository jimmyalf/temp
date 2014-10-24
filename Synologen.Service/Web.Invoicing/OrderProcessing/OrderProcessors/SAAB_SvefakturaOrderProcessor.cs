using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Validators;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public class SAAB_SvefakturaOrderProcessor : SvefakturaOrderProcessor
    {
        public SAAB_SvefakturaOrderProcessor(ISqlProvider provider, ISvefakturaConversionSettings settings, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration)
            : base(provider, settings, ftpService, mailService, fileService, orderProcessConfiguration)
        {
            Settings = settings;
            SvefakturaBuilder = new SAAB_SvefakturaBuilder(new SvefakturaFormatter(), Settings, new SvefakturaBuilderValidator());
        }

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.SAAB_Svefaktura;
        }
    }
}