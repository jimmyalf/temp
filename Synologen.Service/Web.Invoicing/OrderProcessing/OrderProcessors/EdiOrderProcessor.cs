using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Synologen.Service.Web.Invoicing.Services;
using Convert = Spinit.Wpc.Synologen.Invoicing.Convert;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public class EdiOrderProcessor : OrderProcessorBase
    {
        private readonly EDIConversionSettings _ediSettings;

        protected const string EDIFileNameFormat = "Synologen-{0}-{1}-{2}.txt";

        public EdiOrderProcessor(ISqlProvider provider, EDIConversionSettings ediSettings, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration) 
            : base(provider,ftpService, mailService, fileService,orderProcessConfiguration)
        {
            _ediSettings = ediSettings;
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

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.EDI;
        }

        protected void ProcessOrder(IOrder order)
        {
            var ftpStatusMessage = SendEDIInvoice(order);
            UpdateOrderStatus(order.Id);
            AddOrderHistory(order.Id, order.InvoiceNumber, ftpStatusMessage);
        }

        protected string SendEDIInvoice(IOrder order)
        {
            try
            {
                var invoice = GenerateEDIInvoice(order);
                var invoiceFileName = GenerateInvoiceFileName(invoice);
                if (OrderProcessConfiguration.SaveEDIFileCopy)
                {
                    TrySaveContentToDisk(invoiceFileName, invoice.Parse().ToString());
                }

                var invoiceString = invoice.Parse().ToString();
                return UploadTextFileToFTP(invoiceFileName, invoiceString, order.ContractCompany.EDIFtpCredential);
            }
            catch (Exception ex)
            {
                throw LogAndCreateException("SynologenService.SendInvoice failed [OrderId: " + order.Id + "]", ex);
            }
        }

        protected static string GenerateInvoiceFileName(Invoice invoice)
        {
            var date = invoice.InterchangeHeader.DateOfPreparation.ToString(DateFormat);
            var referenceNumber = invoice.InterchangeControlReference;
            var invoiceNumber = invoice.DocumentNumber;
            return string.Format(EDIFileNameFormat, date, invoiceNumber, referenceNumber);
        }

        protected Invoice GenerateEDIInvoice(IOrder order)
        {
            var invoice = Convert.ToEDIInvoice(_ediSettings, order);
            return invoice;
        }
    }
}