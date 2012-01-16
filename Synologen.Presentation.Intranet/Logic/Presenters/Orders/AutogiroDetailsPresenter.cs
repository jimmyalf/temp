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
			ISynologenMemberService synologenMemberService) : base(view)
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
		}
		
    	public void View_Load(object sender, EventArgs e)
    	{
    		var order = _orderRepository.Get(OrderId);
    		View.Model.CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
    		View.Model.SelectedArticleName = order.Article.Name;
    		View.Model.IsNewSubscription = order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_New;
    		View.Model.EnableAutoWithdrawal = order.ShippingType.HasFlag(OrderShippingOption.ToCustomer);
			View.Model.AutoWithdrawalAmount = (order.AutoWithdrawalAmount.HasValue) ? order.AutoWithdrawalAmount.Value.ToString() : null;
      		
			//Set values from previously selected account
			if(order.SelectedPaymentOption.SubscriptionId.HasValue)
			{
				var selectedSubscription = _subscriptionRepository.Get(order.SelectedPaymentOption.SubscriptionId.Value);
			    View.Model.BankAccountNumber = selectedSubscription.BankAccountNumber;
			    View.Model.ClearingNumber = selectedSubscription.ClearingNumber; 
			}
			//Set values from previously saved view
			if(order.SubscriptionPayment != null) UpdateViewModel(order.SubscriptionPayment);
    	}

		private void UpdateViewModel(SubscriptionItem subscriptionItem)
		{
            View.Model.BankAccountNumber = subscriptionItem.Subscription.BankAccountNumber;
            View.Model.ClearingNumber = subscriptionItem.Subscription.ClearingNumber; 
            View.Model.TaxedAmount = subscriptionItem.TaxedAmount.ToString();
            View.Model.TaxfreeAmount = subscriptionItem.TaxFreeAmount.ToString();
            View.Model.SelectedSubscriptionOption =  0;
			if (subscriptionItem.NumberOfPayments == null) return;

			if(subscriptionItem.NumberOfPayments.Value.IsEither(3, 6, 12))
			{
				View.Model.SelectedSubscriptionOption = subscriptionItem.NumberOfPayments;
			}
			else
			{
				View.Model.SelectedSubscriptionOption = AutogiroDetailsModel.UseCustomNumberOfWithdrawalsId;
				View.Model.CustomSubscriptionTime = subscriptionItem.NumberOfPayments;
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
			var shop = _shopRepository.Get(ShopId);

			//Store/Get subscription
			var subscription = GetSubscription(e, order, shop);

			//Store subscriptionItem
    		var subscriptionItem = _viewParser.Parse(e, subscription);
    		_subscriptionItemRepository.Save(subscriptionItem);

			//Update order
			order.SubscriptionPayment = subscriptionItem;
		    order.SelectedPaymentOption.SubscriptionId = subscription.Id;
			order.AutoWithdrawalAmount = e.AutoWithdrawalAmount;
			_orderRepository.Save(order);
		}

		private Subscription GetSubscription(AutogiroDetailsEventArgs e, Order order, Shop shop)
		{
			if(order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_New)
			{
    			var subscription = _viewParser.Parse(e, order.Customer, shop);
				_subscriptionRepository.Save(subscription);
				return subscription;
			}
			if(order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_Existing && order.SelectedPaymentOption.SubscriptionId.HasValue)
			{
				return _subscriptionRepository.Get(order.SelectedPaymentOption.SubscriptionId.Value);
			}
			throw new ApplicationException("Cannot figure out what subscription to use");
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