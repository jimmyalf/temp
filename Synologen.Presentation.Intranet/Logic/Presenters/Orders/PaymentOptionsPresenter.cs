using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class PaymentOptionsPresenter : OrderBasePresenter<IPaymentOptionsView>
    {
    	private readonly IViewParser _viewParser;
    	private readonly IOrderRepository _orderRepository;
    	private readonly IRoutingService _routingService;
    	private readonly ISubscriptionRepository _subscriptionRepository;

    	public PaymentOptionsPresenter(
			IPaymentOptionsView view, 
			IViewParser viewParser,
			IOrderRepository orderRepository, 
			IRoutingService routingService,
			ISynologenMemberService synologenMemberService,
			ISubscriptionRepository subscriptionRepository) : base(view, synologenMemberService)
        {
    		_viewParser = viewParser;
    		_orderRepository = orderRepository;
    		_routingService = routingService;
    		_subscriptionRepository = subscriptionRepository;
    		View.Load += View_Load;
    		View.Abort += View_Abort;
        	View.Submit += View_Submit;
			View.Previous += View_Previous;
        }
       
        public void View_Load(object sender, EventArgs eventArgs)
    	{
            var order = _orderRepository.Get(RequestOrderId);
			CheckAccess(order.Shop);
    	    var customer = order.Customer;
            View.Model.Subscriptions = GetSubscriptionList(customer.Id);
            View.Model.SelectedOption = SetSelectedOption(order);
    		View.Model.CustomerName = customer.ParseName(x => x.FirstName, x => x.LastName);
    	}

		private int SetSelectedOption(Order order)
		{
			if (order.SelectedPaymentOption == null) return 0;
			if(order.SelectedPaymentOption.Type != PaymentOptionType.Subscription_Autogiro_Existing) return 0;
            return !order.SelectedPaymentOption.SubscriptionId.HasValue 
				? 0 
				: order.SelectedPaymentOption.SubscriptionId.Value;
		}

    	public void View_Abort(object sender, EventArgs eventArgs)
    	{
            var order = _orderRepository.Get(RequestOrderId);
            var isNewSubscription = order.SelectedPaymentOption.Type.Equals(PaymentOptionType.Subscription_Autogiro_New);

            var subscription = order.SubscriptionPayment != null ? order.SubscriptionPayment.Subscription : null;

            _orderRepository.DeleteOrderAndSubscriptionItem(order);
            if (isNewSubscription && subscription != null) _subscriptionRepository.Delete(subscription);

			Redirect(View.AbortPageId);
    	}

    	public void View_Submit(object sender, PaymentOptionsEventArgs args)
    	{
    		var order = _orderRepository.Get(RequestOrderId);
    		SetOrderPaymentOption(order, args);
			_orderRepository.Save(order);
			Redirect(View.NextPageId, new {order = order.Id});
    	}

    	public void View_Previous(object sender, EventArgs eventArgs)
    	{
    		Redirect(View.PreviousPageId, new {order = RequestOrderId});
    	}

		private void SetOrderPaymentOption(Order order, PaymentOptionsEventArgs args)
		{
			if(args.SubscriptionId.HasValue)
			{
				order.SelectedPaymentOption.Type = PaymentOptionType.Subscription_Autogiro_Existing;
				order.SelectedPaymentOption.SubscriptionId = args.SubscriptionId.Value;
			}
			else
			{
				order.SelectedPaymentOption.Type = PaymentOptionType.Subscription_Autogiro_New;
                if(order.SelectedPaymentOption.SubscriptionId == null)
                {
                    order.SelectedPaymentOption.SubscriptionId = null;
                }
			}
		}   

		private void Redirect(int pageId, object routeData = null)
		{
			var url = _routingService.GetPageUrl(pageId, routeData);
			HttpContext.Response.Redirect(url);
		}

		private IList<SubscriptionListItemModel> GetSubscriptionList(int customerId)
		{
    		return _subscriptionRepository.FindBy(new ActiveSubscriptionsForCustomerCritieria(customerId))
                .Select(x => new SubscriptionListItemModel(x))
                .ToList();
		}

    	private int RequestOrderId
    	{
    		get { return HttpContext.Request.Params["order"].ToInt(); }
    	}

        public override void ReleaseView()
        {
			View.Abort -= View_Abort;
			View.Submit -= View_Submit;
			View.Previous -= View_Previous;
			View.Load -= View_Load;
        }
    }
}