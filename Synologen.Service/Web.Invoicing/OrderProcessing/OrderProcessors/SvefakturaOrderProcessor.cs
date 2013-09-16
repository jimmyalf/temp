using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.PostOffice;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Validators;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public class SvefakturaOrderProcessor : OrderProcessorBase
    {
        private readonly ISvefakturaBuilder _svefakturaBuilder;
        private readonly ISvefakturaConversionSettings _settings;
        protected const string SvefakturaFileNameFormat = "Synologen-{0} {1}.xml";

        public SvefakturaOrderProcessor(ISqlProvider provider, ISvefakturaConversionSettings settings, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration) : base(provider,ftpService, mailService, fileService,orderProcessConfiguration)
        {
            _settings = settings;
            _svefakturaBuilder = new SvefakturaBuilder(new SvefakturaFormatter(), _settings, new SvefakturaBuilderValidator());
        }

        public override OrderProcessResult Process(IList<IOrder> ordersToProcess)
        {
            var result = new OrderProcessResult();
            if (!ordersToProcess.Any()) return result;

            foreach (var order in ordersToProcess)
            {
                try
                {
                    ProcessOrder(order);
                    result.AddSentOrderId(order.Id);
                }
                catch (Exception ex)
                {
                    result.AddFailedOrderId(order.Id, ex);
                }
            }
            return result;
        }

        protected void ProcessOrder(IOrder order)
        {
            var invoice = _svefakturaBuilder.Build(order);

            var ruleViolations = SvefakturaValidator.ValidateObject(invoice).ToList();
            if (ruleViolations.Any())
            {
                throw new WebserviceException("The invoice could not be validated: " + SvefakturaValidator.FormatRuleViolations(ruleViolations));
            }

            var invoiceStringContent = SerializeInvoice(invoice, order);
            var invoiceFileName = GetInvoiceFileName(invoice);

            if (OrderProcessConfiguration.SaveSvefakturaFileCopy)
            {
                TrySaveContentToDisk(invoiceFileName, invoiceStringContent);
            }

            var ftpStatusMessage =  UploadTextFileToFTP(invoiceFileName, invoiceStringContent);

            UpdateOrderStatus(order.Id);
            AddOrderHistory(order.Id, order.InvoiceNumber, ftpStatusMessage);
        }

        private string GetInvoiceFileName(SFTIInvoiceType invoice)
        {
            var date = DateTime.Now.ToString(DateFormat);
            var orderId = invoice.ID.Value;
            return string.Format(SvefakturaFileNameFormat, orderId, date);
        }

        protected string SerializeInvoice(SFTIInvoiceType invoice, IOrder order)
        {
            var encoding = OrderProcessConfiguration.FTPCustomEncodingCodePage;
            var header = BuildPostOfficeHeader(_settings.EDIAddress, order.ContractCompany.EDIRecipient);
            return SvefakturaSerializer.Serialize(invoice, encoding, "\r\n", Formatting.Indented, header);            
        }

        protected PostOfficeHeader BuildPostOfficeHeader(EdiAddress sender, EdiAddress recipient)
        {
            return new PostOfficeHeader("POSTEN", "SVEFAKTURA", sender, recipient);
        }

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.Svefaktura;
        }
    }



}