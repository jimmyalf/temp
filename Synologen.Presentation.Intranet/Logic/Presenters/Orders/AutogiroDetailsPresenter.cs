using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class AutogiroDetailsPresenter : OrderBasePresenter<IAutogiroDetailsView>
    {
    	private readonly IViewParser _viewParser;
    	private readonly IRoutingService _routingService;
    	private readonly IOrderRepository _orderRepository;
    	private readonly ISubscriptionRepository _subscriptionRepository;
    	private readonly ISubscriptionItemRepository _subscriptionItemRepository;
    	private readonly IShopRepository _shopRepository;
    	private readonly ISynologenMemberService _synologenMemberService;

    	public AutogiroDetailsPresenter(
			IAutogiroDetailsView view, 
			IViewParser viewParser,
			IRoutingService routingService, 
			IOrderRepository orderRepository, 
			ISubscriptionRepository subscriptionRepository,
			ISubscriptionItemRepository subscriptionItemRepository,
			IShopRepository shopRepository,
			ISynologenMemberService synologenMemberService) : base(view, synologenMemberService)
        {
    		_viewParser = viewParser;
    		_routingService = routingService;
    		_orderRepository = orderRepository;
    		_subscriptionRepository = subscriptionRepository;
    		_subscriptionItemRepository = subscriptionItemRepository;
    		_shopRepository = shopRepository;
    		_synologenMemberService = synologenMemberService;
    		WireupEvents();
        }

		private void WireupEvents()
		{
        	View.Load += View_Load;
			View.Abort += View_Abort;
			View.Submit += View_Submit;
			View.Previous += View_Previous;
		    View.FillForm += Fill_Form;
			View.SelectedSubscriptionTimeChanged += Fill_Form;
		}

        private void Fill_Form(object sender, AutogiroDetailsEventArgs e)
        {
			View.Model.Initialize(e);
        }

        public void View_Load(object sender, EventArgs e)
    	{
    		var order = _orderRepository.Get(OrderId);
			CheckAccess(order.Shop);
    		View.Model.CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
    		View.Model.IsNewSubscription = order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_New;
      		
			//Set values from previously saved view
			if (order.SubscriptionPayment != null)
			{
				View.Model.Initialize(order.SubscriptionPayment);
			}
			//Set values from selected account in previous step
			else if(order.SelectedPaymentOption.SubscriptionId.HasValue)
			{
				var selectedSubscription = _subscriptionRepository.Get(order.SelectedPaymentOption.SubscriptionId.Value);
			    View.Model.BankAccountNumber = selectedSubscription.BankAccountNumber;
			    View.Model.ClearingNumber = selectedSubscription.ClearingNumber;
			}
    	}
		
		public void View_Previous(object sender, EventArgs e)
		{
			Redirect(View.PreviousPageId, new {order = OrderId});
		}

    	public void View_Submit(object sender, AutogiroDetailsEventArgs e)
    	{
    		StoreSubscriptionData(e);
    		Redirect(View.NextPageId, new {order = OrderId});
    	}

    	public void View_Abort(object sender, EventArgs e)
    	{
    		var order = _orderRepository.Get(OrderId);
    	    var isNewSubscription = order.SelectedPaymentOption.Type.Equals(PaymentOptionType.Subscription_Autogiro_New);

    	    var subscription = order.SubscriptionPayment != null ? order.SubscriptionPayment.Subscription : null;

    	    _orderRepository.DeleteOrderAndSubscriptionItem(order);
    	    if(isNewSubscription && subscription != null) _subscriptionRepository.Delete(subscription);
    		
            Redirect(View.AbortPageId);
    	}

    	public override void ReleaseView()
        {
        	View.Load -= View_Load;
			View.Abort -= View_Abort;
			View.Submit -= View_Submit;
			View.Previous -= View_Previous;
		    View.FillForm -= Fill_Form;
			View.SelectedSubscriptionTimeChanged -= Fill_Form;
        }

		private void StoreSubscriptionData(AutogiroDetailsEventArgs e)
		{
			var order = _orderRepository.Get(OrderId);
			var shop = _shopRepository.Get(ShopId);

			//Store/Get subscription
			var subscription = GetSubscription(e, order, shop);

			//Store subscriptionItem
            if(order.SubscriptionPayment == null)
            {
                var subscriptionItem = _viewParser.Parse(e, subscription);
                _subscriptionItemRepository.Save(subscriptionItem);
                order.SubscriptionPayment = subscriptionItem;
            }
            else
            {
                var subscriptionItem = order.SubscriptionPayment; 
                _viewParser.UpdateSubscriptionItem(e, subscriptionItem, subscription);
                order.SubscriptionPayment = subscriptionItem;
            }
		    
			//Update order
		    order.SelectedPaymentOption.SubscriptionId = subscription.Id;
			order.OrderTotalWithdrawalAmount = e.ProductPrice + e.FeePrice;
			_orderRepository.Save(order);
		}

		private Subscription GetSubscription(AutogiroDetailsEventArgs e, Order order, Shop shop)
		{
            if (order.SelectedPaymentOption.SubscriptionId.HasValue)
            {
                var subscription = _subscriptionRepository.Get(order.SelectedPaymentOption.SubscriptionId.Value);
                subscription.BankAccountNumber = e.BankAccountNumber;
                subscription.ClearingNumber = e.ClearingNumber;
                _subscriptionRepository.Save(subscription);
                return subscription;
            }
            else
            {
                var subscription = _viewParser.Parse(e, order.Customer, shop);
                _subscriptionRepository.Save(subscription);
                return subscription;
            }
		    
		}

		private void Redirect(int pageId, object requestParameters = null)
		{
		    var url = _routingService.GetPageUrl(pageId, requestParameters);
		    HttpContext.Response.Redirect(url);
		}

    	private int OrderId
    	{
			get { return HttpContext.Request.Params["order"].ToInt(); }
    	}

    	private int ShopId
    	{
    		get { return _synologenMemberService.GetCurrentShopId(); }
    	}
    }
}