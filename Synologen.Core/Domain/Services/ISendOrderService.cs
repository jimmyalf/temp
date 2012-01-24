using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
    public interface ISendOrderService
    {
        int SendOrderByEmail(Order order);
    }
}