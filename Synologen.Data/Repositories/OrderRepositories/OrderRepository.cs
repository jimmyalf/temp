using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;

namespace Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories
{
    public class OrderRepository : NHibernateRepository<Order>, IOrderRepository 
    {
        public OrderRepository(ISession session) : base(session)
        {
        }


    	public void DeleteOrderAndSubscriptionItem(Order order)
    	{
			if(order.SubscriptionPayment != null)
			{
    			var subscriptionItem = order.SubscriptionPayment;
    			order.SubscriptionPayment = null;
				Save(order);
    			Session.Delete(subscriptionItem);
			}
			Delete(order);
    	}
    }
}