using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public interface IOrderProcessor
    {
        OrderProcessResult Process(IList<IOrder> ordersToProcess);
        IList<IOrder> FilterOrdersMatchingInvoiceType(IList<IOrder> orders);
        IList<IOrder> FilterOrdersMatchingInvoiceType(IList<Order> orders);
        bool IHandle(InvoicingMethod method);
    }
}