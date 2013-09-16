using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Synologen.Service.Web.Invoicing.OrderProcessing
{
    public class OrderProcessResult
    {
        public OrderProcessResult()
        {
            SentOrdersIds = new List<int>();
            FailedOrders = new List<FailedOrderSendout>();
        }

        public void AddSentOrderId(int orderId)
        {
            SentOrdersIds.Add(orderId);
        }

        public void AddSentOrderRange(IEnumerable<IOrder> orders)
        {
            foreach (var order in orders)
            {
                AddSentOrderId(order.Id);
            }
        }

        public void AddFailedOrderId(int orderId, Exception ex)
        {
            FailedOrders.Add(new FailedOrderSendout { OrderId = orderId, Exception = ex });
        }

        public bool Success 
        { 
            get { return !FailedOrders.Any(); }
        }

        public IList<int> SentOrdersIds { get; private set; }
        public IList<FailedOrderSendout> FailedOrders { get; private set; }

        public static OrderProcessResult operator +(OrderProcessResult first, OrderProcessResult second)
        {
            var sentOrderIds = first.SentOrdersIds.Concat(second.SentOrdersIds).ToList();
            var failedOrders = first.FailedOrders.Concat(second.FailedOrders).ToList();
            return new OrderProcessResult
            {
                FailedOrders = failedOrders,
                SentOrdersIds = sentOrderIds
            };
        }
    }

    public class FailedOrderSendout
    {
        public int OrderId { get; set; }
        public Exception Exception { get; set; }
    }
}