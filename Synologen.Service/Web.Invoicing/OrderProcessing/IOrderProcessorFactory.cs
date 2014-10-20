using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors;

namespace Synologen.Service.Web.Invoicing.OrderProcessing
{
    public interface IOrderProcessorFactory
    {
        IOrderProcessor GetOrderProcessorFor(InvoicingMethod method);
    }
}