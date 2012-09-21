using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;

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
		}

        private void Fill_Form(object sender, AutogiroDetailsInvalidFormEventArgs e)
        {
            View.Model.BankAccountNumber = e.BankAccountNumber;
            View.Model.ClearingNumber = e.ClearingNumber;
            View.Model.ProductPrice = e.FeePrice.ToString();
            View.Model.FeePrice = e.ProductPrice.ToString();
            View.Model.SelectedSubscriptionOption = e.NumberOfPaymentsSelectedValue;
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
				UpdateViewModel(order.SubscriptionPayment);
			}
			//Set values from selected account in previous step
			else if(order.SelectedPaymentOption.SubscriptionId.HasValue)
			{
				var selectedSubscription = _subscriptionRepository.Get(order.SelectedPaymentOption.SubscriptionId.Value);
			    View.Model.BankAccountNumber = selectedSubscription.BankAccountNumber;
			    View.Model.ClearingNumber = selectedSubscription.ClearingNumber;
			}
    	}

		private void UpdateViewModel(SubscriptionItem subscriptionItem)
		{
            View.Model.BankAccountNumber = subscriptionItem.Subscription.BankAccountNumber;
            View.Model.ClearingNumber = subscriptionItem.Subscription.ClearingNumber; 
            View.Model.ProductPrice = subscriptionItem.Value.Product.ToString("0.00");
            View.Model.FeePrice = subscriptionItem.Value.Fee.ToString("0.00");
			View.Model.TotalWithdrawal = subscriptionItem.Value.Total.ToString("0.00");
			View.Model.Montly = subscriptionItem.MonthlyWithdrawal.Total.ToString("0.00");
			if(subscriptionItem.IsOngoing)
			{
				View.Model.CustomMonthlyFeeAmount = subscriptionItem.MonthlyWithdrawal.Fee.ToString("0.00");
				View.Model.CustomMonthlyProductAmount = subscriptionItem.MonthlyWithdrawal.Product.ToString("0.00");
			}
			
            View.Model.SelectedSubscriptionOption = 0;

			if(subscriptionItem.WithdrawalsLimit.IsEither(3, 6, 12))
			{
				View.Model.SelectedSubscriptionOption = subscriptionItem.WithdrawalsLimit;
			}
			else if(subscriptionItem.WithdrawalsLimit == null)
			{
				View.Model.SelectedSubscriptionOption = AutogiroDetailsModel.OngoingSubscription;
			}
			else
			{
			    View.Model.SelectedSubscriptionOption = AutogiroDetailsModel.UseCustomNumberOfWithdrawalsId;
			    View.Model.CustomSubscriptionTime = subscriptionItem.WithdrawalsLimit;
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