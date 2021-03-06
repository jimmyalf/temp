using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Client.OrderEmailSender
{
    public class OrderSender
    {
        private readonly ISendOrderService _orderSenderService;
    	private readonly ILoggingService _loggingService;
    	private readonly IOrderRepository _orderRepository;
        
        public OrderSender(IOrderRepository orderRepository, ISendOrderService sendOrderService, ILoggingService loggingService)
        {   
            _orderRepository = orderRepository;
			_orderSenderService = sendOrderService;
        	_loggingService = loggingService;
        }

        public void SendOrders()
        {
        	var orders = (_orderRepository.FindBy(new AllOrdersToSendEmailForCriteria()) ?? Enumerable.Empty<Order>()).ToList();
			_loggingService.LogInfo("Found {0} orders to send email for", orders.Count());
        	var ordersGroupedByShopAndSupplier = orders.GroupBy(x => new {x.Shop, x.LensRecipe.ArticleSupplier});
        	foreach (var orderGroup in ordersGroupedByShopAndSupplier)
        	{
				try
				{
					var emailId = _orderSenderService.SendOrdersBatchedByShopAndSupplier(orderGroup, orderGroup.Key.ArticleSupplier);
					foreach (var order in orderGroup)
					{
						_loggingService.LogInfo("Sent email for order {0}: Got email id {1} from spinit services.", order.Id, emailId);
						order.SpinitServicesEmailId = emailId;
						_orderRepository.Save(order);	
					}
				}
				catch(Exception ex)
				{
					throw new ApplicationException(string.Format("Encountered an error while trying to send emails for shop {0} and supplier {1}", orderGroup.Key.Shop.Id, orderGroup.Key.ArticleSupplier.Id), ex);
				}
        	}
        }
    }
}