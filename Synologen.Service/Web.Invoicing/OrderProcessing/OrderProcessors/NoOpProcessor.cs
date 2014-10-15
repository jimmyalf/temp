using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Validators;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Synologen.Service.Web.Invoicing.Services;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NoOpProcessor : OrderProcessorBase
    {
        public NoOpProcessor(ISqlProvider provider, IFtpService ftpService, IMailService mailService, IFileService fileService, IOrderProcessConfiguration orderProcessConfiguration)
            : base(provider, ftpService, mailService, fileService, orderProcessConfiguration)
        {
        }

        public override OrderProcessResult Process(IList<IOrder> ordersToProcess)
        {
            var result = new OrderProcessResult();
            if (!ordersToProcess.Any())
            {
                return result;
            }

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

        private void ProcessOrder(IOrder order)
        {

            UpdateOrderStatus(order.Id);
            AddOrderHistory(order.Id, order.InvoiceNumber, "Not sent because this order is of type 'No Operational'");
        }

        public override bool IHandle(InvoicingMethod method)
        {
            return method == InvoicingMethod.NoOp;
        }
    }
}