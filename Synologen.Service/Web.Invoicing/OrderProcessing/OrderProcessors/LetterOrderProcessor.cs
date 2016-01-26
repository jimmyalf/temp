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
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public class LetterOrderProcessor : OrderProcessorBase
    {
        protected const string SvefakturaListFileNameFormat = "Synologen-{0}-{1} {2}.xml";
        private readonly EBrev_SvefakturaBuilder _eBrevSvefakturaBuilder;

        public LetterOrderProcessor(ISqlProvider provider, ISvefakturaConversionSettings settings, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration) : base(provider,ftpService, mailService, fileService,orderProcessConfiguration)
        {
            _eBrevSvefakturaBuilder = new EBrev_SvefakturaBuilder(new SvefakturaFormatter(), settings, new EBrev_SvefakturaBuilderValidator());            
        }

        public override OrderProcessResult Process(IList<IOrder> ordersToProcess)
        {
            var result = new OrderProcessResult();
            if (!ordersToProcess.Any())
            {
                return result;
            }

            var ftpStatusMessage = SendLetterInvoices(ordersToProcess, out result);
            foreach (var order in ordersToProcess)
            {
                UpdateOrderStatus(order.Id);
                AddOrderHistory(order.Id, order.InvoiceNumber, ftpStatusMessage);
            }

            return result;
        }

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.LetterInvoice;
        }

        protected string SendLetterInvoices(IEnumerable<IOrder> orders, out OrderProcessResult result)
        {
            result = new OrderProcessResult();
            var invoices = new SFTIInvoiceList { Invoices = new List<SFTIInvoiceType>() };
            foreach (var order in orders)
            {
                var invoice = _eBrevSvefakturaBuilder.Build(order);

                var ruleViolations = SvefakturaValidator.ValidateInvoice(invoice).ToList();
                if (ruleViolations.Any())
                {
                    var exceptionMessage = string.Format("Invoice[{0}]/Order[{1}] failed validation: {2}", order.InvoiceNumber, order.Id, SvefakturaValidator.FormatRuleViolations(ruleViolations));
                    var exception = new WebserviceException(exceptionMessage);
                    result.AddFailedOrderId(order.Id, exception);
                }
                else
                {
                    invoices.Invoices.Add(invoice);
                    result.AddSentOrderId(order.Id);
                }
            }

            var encoding = OrderProcessConfiguration.FTPCustomEncodingCodePage;
            var postOfficeheader = GetPostOfficeHeader();
            var invoiceStringContent = SvefakturaSerializer.Serialize(invoices, encoding, "\r\n", Formatting.Indented, postOfficeheader);
            var invoiceFileName = GenerateInvoiceFileName(invoices);
            if (OrderProcessConfiguration.SaveSvefakturaFileCopy)
            {
                TrySaveContentToDisk(invoiceFileName, invoiceStringContent);
            }
            
            return UploadTextFileToFTP(invoiceFileName, invoiceStringContent);
        }

        protected string GenerateInvoiceFileName(SFTIInvoiceList invoices)
        {
            var maxId = invoices.Invoices.Max(x => x.ID.Value);
            var minId = invoices.Invoices.Min(x => x.ID.Value);
            return string.Format(SvefakturaListFileNameFormat, minId, maxId, DateTime.Now.ToString(DateFormat));
        }
        
        protected PostOfficeHeader GetPostOfficeHeader()
        {
            var sender = new EdiAddress(OrderProcessConfiguration.PostnetSender);
            var recipient = new EdiAddress(OrderProcessConfiguration.PostnetRecipient);
            var messageType = OrderProcessConfiguration.PostnetMessageType;
            return new PostOfficeHeader("POSTNET", messageType, sender, recipient);

            //var sender = OrderProcessConfiguration.PostnetSender;
            //var recipient = OrderProcessConfiguration.PostnetRecipient;
            //var messageType = OrderProcessConfiguration.PostnetMessageType;
            //const string HeaderFormat = "<?POSTNET SND=\"{0}\" REC=\"{1}\" MSGTYPE=\"{2}\"?>";
            //return string.Format(HeaderFormat, sender, recipient, messageType);
        }
    }
}