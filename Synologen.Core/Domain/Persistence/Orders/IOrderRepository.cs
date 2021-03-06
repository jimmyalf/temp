using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
    	void DeleteOrderAndSubscriptionItem(Order order);
    }
}