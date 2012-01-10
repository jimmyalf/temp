using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class PaymentOptionsPresenter : Presenter<IPaymentOptionsView>
    {
    	private readonly IViewParser _viewParser;
    	private readonly IOrderRepository _orderRepository;
    	private readonly ISynologenMemberService _synologenMemberService;
    	private readonly ISubscriptionRepository _subscriptionRepository;

    	public PaymentOptionsPresenter(
			IPaymentOptionsView view, 
			IViewParser viewParser,
			IOrderRepository orderRepository, 
			ISynologenMemberService synologenMemberService, 
			ISubscriptionRepository subscriptionRepository) : base(view)
        {
    		_viewParser = viewParser;
    		_orderRepository = orderRepository;
    		_synologenMemberService = synologenMemberService;
    		_subscriptionRepository = subscriptionRepository;
    		View.Load += View_Load;
    		View.Abort += View_Abort;
        	View.Submit += View_Submit;
			View.Previous += View_Previous;
        }
       
        public void View_Load(object sender, EventArgs eventArgs)
    	{
    		var orderId = HttpContext.Request.Params["order"].ToInt();
            var order = _orderRepository.Get(orderId);
    	    var customer = order.Customer;
            View.Model.Subscriptions = GetSubscriptionList(customer);
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
    		var orderId = HttpContext.Request.Params["order"].ToInt();
    		var order = _orderRepository.Get(orderId);
			_orderRepository.Delete(order);
			Redirect(View.AbortPageId, order.Id);
    	}

    	public void View_Submit(object sender, PaymentOptionsEventArgs args)
    	{
    		var orderId = HttpContext.Request.Params["order"].ToInt();
    		var order = _orderRepository.Get(orderId);
    		UpdateOrderWithPaymentOption(order, args);
			_orderRepository.Save(order);
			Redirect(View.NextPageId, order.Id);
    	}

    	public void View_Previous(object sender, EventArgs eventArgs)
    	{
			var orderId = HttpContext.Request.Params["order"].ToInt();
    		Redirect(View.PreviousPageId, orderId);
    	}

		private void UpdateOrderWithPaymentOption(Order order, PaymentOptionsEventArgs args)
		{
			if(args.SubscriptionId.HasValue)
			{
				order.SelectedPaymentOption.Type = PaymentOptionType.Subscription_Autogiro_Existing;
				order.SelectedPaymentOption.SubscriptionId = args.SubscriptionId.Value;
			}
			else
			{
				order.SelectedPaymentOption.Type = PaymentOptionType.Subscription_Autogiro_New;
				order.SelectedPaymentOption.SubscriptionId = null;
			}
		}

		private void Redirect(int pageId, int orderId)
		{
			var url = _synologenMemberService.GetPageUrl(pageId);
			var redirectUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = url, OrderId = orderId});
			HttpContext.Response.Redirect(redirectUrl);
		}

		private IEnumerable<ListItem> GetSubscriptionList(OrderCustomer customer)
		{
    		var subscriptions = _subscriptionRepository.FindBy(new ActiveAndConsentedSubscriptionsForCustomerCritieria(customer.Id));
			Func<Subscription, ListItem> parser = subscription => new ListItem(subscription.BankAccountNumber, subscription.Id);
			return _viewParser.Parse(subscriptions, parser).Concat(new[] {new ListItem("Skapa nytt konto", 0)});
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