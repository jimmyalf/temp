using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class AutogiroDetailsPresenter : Presenter<IAutogiroDetailsView>
    {
    	private readonly IViewParser _viewParser;
    	private readonly IRoutingService _routingService;
    	private readonly IOrderRepository _orderRepository;
    	private readonly ISubscriptionRepository _subscriptionRepository;
    	private readonly ISubscriptionItemRepository _subscriptionItemRepository;

    	public AutogiroDetailsPresenter(
			IAutogiroDetailsView view, 
			IViewParser viewParser,
			IRoutingService routingService, 
			IOrderRepository orderRepository, 
			ISubscriptionRepository subscriptionRepository,
			ISubscriptionItemRepository subscriptionItemRepository) : base(view)
        {
    		_viewParser = viewParser;
    		_routingService = routingService;
    		_orderRepository = orderRepository;
    		_subscriptionRepository = subscriptionRepository;
    		_subscriptionItemRepository = subscriptionItemRepository;
    		WireupEvents();
        }

		private void WireupEvents()
		{
        	View.Load += View_Load;
			View.Abort += View_Abort;
			View.Submit += View_Submit;
			View.Previous += View_Previous;
		}
		
    	public void View_Load(object sender, EventArgs e)
    	{
    		var order = _orderRepository.Get(OrderId);
    		View.Model.CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
    	}
		
		public void View_Previous(object sender, EventArgs e)
		{
			Redirect(View.PreviousPageId, "?order=" + OrderId);
		}

    	public void View_Submit(object sender, AutogiroDetailsEventArgs e)
    	{
    		StoreSubscriptionData(e);
            Redirect(View.NextPageId);
    	}

    	public void View_Abort(object sender, EventArgs e)
    	{
    		var order = _orderRepository.Get(OrderId);
			_orderRepository.Delete(order);
    		Redirect(View.AbortPageId);
    	}

    	public override void ReleaseView()
        {
        	View.Load -= View_Load;
			View.Abort -= View_Abort;
			View.Submit -= View_Submit;
			View.Previous -= View_Previous;
        }

		private void StoreSubscriptionData(AutogiroDetailsEventArgs e)
		{
			var order = _orderRepository.Get(OrderId);

			//Store/Get subscription
			var subscription = GetSubscription(e, order);

			//Store subscriptionItem
    		var subscriptionItem = _viewParser.Parse(e, subscription);
    		_subscriptionItemRepository.Save(subscriptionItem);

			//Update order
			order.SubscriptionPayment = subscriptionItem;
			_orderRepository.Save(order);
		}

		private Subscription GetSubscription(AutogiroDetailsEventArgs e, Order order)
		{
			if(order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_New)
			{
    			var subscription = _viewParser.Parse(e, order.Customer);
				_subscriptionRepository.Save(subscription);
				return subscription;
			}
			if(order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_Existing && order.SelectedPaymentOption.SubscriptionId.HasValue)
			{
				return _subscriptionRepository.Get(order.SelectedPaymentOption.SubscriptionId.Value);
			}
			throw new ApplicationException("Cannot figure out what subscription to use");
		}

		private void Redirect(int pageId, string queryString = null)
		{
			var url = _routingService.GetPageUrl(pageId);
			HttpContext.Response.Redirect(url+queryString);
		}

    	private int OrderId
    	{
			get { return HttpContext.Request.Params["order"].ToInt(); }
    	}
    }
}