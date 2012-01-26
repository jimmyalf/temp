using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StructureMap;
using Synologen.OrderEmailSender.Services;
using Order = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Order;

namespace Synologen.OrderEmailSender
{
    
    public class OrderSender
    {
        private readonly ISendOrderService _orderSenderService;
        private readonly IOrderRepository _orderRepository;
        
        public OrderSender()
        {   
            _orderRepository = ObjectFactory.GetInstance<IOrderRepository>();
            _orderSenderService = new SendOrderService();
        }

        public void SendOrders()
        {
            var orders = _orderRepository.GetAll().Where(x => x.SendEmailForThisOrder);

            foreach (var order in orders)
            {
                UpdateOrderEmailId(_orderSenderService.SendOrderByEmail(order), order);
            }
        }

        private void UpdateOrderEmailId(int emailId, Order order)
        {
            order.SpinitServicesEmailId = emailId;
            order.SendEmailForThisOrder = false;
            _orderRepository.Save(order);
        }
    }
}