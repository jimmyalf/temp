using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing
{
    public class OrderProcessorFactory : IOrderProcessorFactory
    {
        private readonly ISqlProvider _provider;
        private readonly EDIConversionSettings _ediSettings;
        private readonly IFtpService _ftpService;
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;
        private readonly IOrderProcessConfiguration _configuration;
        private readonly ISvefakturaConversionSettings _svefakturaSettings;
        private readonly I_PDF_OrderInvoiceConversionSettings _pdfInvoiceOrderSettings;

        public OrderProcessorFactory(ISqlProvider provider, EDIConversionSettings ediSettings, ISvefakturaConversionSettings svefakturaSettings, I_PDF_OrderInvoiceConversionSettings pdfInvoiceOrderSettings, IFtpService ftpService, IFileService fileService, IMailService mailService, IOrderProcessConfiguration configuration)
        {
            _provider = provider;
            _ediSettings = ediSettings;
            _ftpService = ftpService;
            _fileService = fileService;
            _mailService = mailService;
            _configuration = configuration;
            _svefakturaSettings = svefakturaSettings;
            _pdfInvoiceOrderSettings = pdfInvoiceOrderSettings;
            OrderProcessors = InstantiateOrderProcessors();
        }

        public IList<IOrderProcessor> OrderProcessors { get; set; }

        public IOrderProcessor GetOrderProcessorFor(InvoicingMethod method)
        {
            return OrderProcessors.Single(x => x.IHandle(method));
        }

        protected IList<IOrderProcessor> InstantiateOrderProcessors()
        {
            return new List<IOrderProcessor>
            {
                new EdiOrderProcessor(_provider, _ediSettings, _ftpService, _mailService, _fileService, _configuration),
                new LetterOrderProcessor(_provider, _svefakturaSettings, _ftpService, _mailService, _fileService, _configuration),
                new SvefakturaOrderProcessor(_provider, _svefakturaSettings, _ftpService, _mailService, _fileService, _configuration),
                new SAAB_SvefakturaOrderProcessor(_provider, _svefakturaSettings, _ftpService, _mailService, _fileService, _configuration),
                new NoOpProcessor(_provider, _ftpService, _mailService, _fileService, _configuration),
                new PDF_InvoiceOrderProcessor(_provider, _pdfInvoiceOrderSettings, _ftpService, _mailService, _fileService, _configuration),
            };
        }
    }
}