using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class SaveCustomerPresenter : OrderBasePresenter<ISaveCustomerView>
    {
        private readonly IOrderCustomerRepository _orderCustomerRepository;
        private readonly IOrderRepository _orderRepository;
    	private readonly IViewParser _viewParser;
    	private readonly IRoutingService _routingService;
    	private readonly IShopRepository _shopRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
    	private readonly ISynologenMemberService _synologenMemberService;

    	public SaveCustomerPresenter(
			ISaveCustomerView view, 
			IOrderCustomerRepository orderCustomerRepository, 
			IOrderRepository orderRepository, 
			IViewParser viewParser, 
			IRoutingService routingService,
			IShopRepository shopRepository,
            ISubscriptionRepository subscriptionRepository,
			ISynologenMemberService synologenMemberService) : base(view, synologenMemberService)
        {
            _orderCustomerRepository = orderCustomerRepository;
    	    _subscriptionRepository = subscriptionRepository;
    	    _orderRepository = orderRepository;
        	_viewParser = viewParser;
    		_routingService = routingService;
    		_shopRepository = shopRepository;
    		_synologenMemberService = synologenMemberService;
    		View.Load += View_Load;
    		View.Submit += View_Submit;
    		View.Abort += View_Abort;
			View.Previous += View_Previous;
        }

    	public void View_Previous(object sender, EventArgs e)
    	{
            if(RequestOrderId.HasValue)
            {
                RemoveOrderAndSubscription(RequestOrderId.Value);
            }
    		Redirect(View.PreviousPageId);
    	}

        public void View_Abort(object sender, EventArgs e)
    	{
            if (RequestOrderId.HasValue)
            {
                RemoveOrderAndSubscription(RequestOrderId.Value);
            }
			Redirect(View.AbortPageId);
    	}

    	public void View_Load(object o, EventArgs eventArgs)
    	{
			if(RequestOrderId.HasValue)
			{
				var order = _orderRepository.Get(RequestOrderId.Value);
				CheckAccess(order.Shop);
				UpdateViewModel(order.Customer.Id, order.Customer.PersonalIdNumber);
			}
			else
			{
    			UpdateViewModel(RequestCustomerId, RequestPersonalIdNumber);
			}
    	}

		private void UpdateViewModel(int? customerId, string personalIdNumber)
		{
			if(customerId.HasValue)
			{
				var customerIdValue = customerId.Value;
				var customer = _orderCustomerRepository.Get(customerIdValue);
    			View.Model.AddressLineOne = customer.AddressLineOne;
				View.Model.AddressLineTwo = customer.AddressLineTwo;
				View.Model.City = customer.City;
				View.Model.Email = customer.Email;
				View.Model.FirstName = customer.FirstName;
				View.Model.LastName = customer.LastName;
				View.Model.MobilePhone = customer.MobilePhone;
				View.Model.Notes = customer.Notes;
				View.Model.PersonalIdNumber = customer.PersonalIdNumber;
				View.Model.Phone = customer.Phone;
				View.Model.PostalCode = customer.PostalCode;
			}
			else
			{
				View.Model.PersonalIdNumber = personalIdNumber;
				View.Model.DisplayCustomerMissingMessage = true;
			}
		}

        public void View_Submit(object o, SaveCustomerEventArgs args)
        {
        	var customer = GetCustomer(args);
			_orderCustomerRepository.Save(customer);

			if (RequestOrderId.HasValue)
			{
				Redirect(View.NextPageId, new { order = RequestOrderId.Value });
			}
			else
			{
				Redirect(View.NextPageId, new {customer = customer.Id});
			}
        }

		private OrderCustomer GetCustomer(SaveCustomerEventArgs args)
		{
			if(RequestOrderId.HasValue)
			{
				var customer = _orderRepository.Get(RequestOrderId.Value).Customer;
				_viewParser.Fill(customer, args);
				return customer;
			}
			if(RequestCustomerId.HasValue)
			{
				var customer = _orderCustomerRepository.Get(RequestCustomerId.Value);
				_viewParser.Fill(customer, args);
				return customer;
			}
			var shop = _shopRepository.Get(ShopId);
			return _viewParser.Parse(args, shop);
		}

		private void Redirect(int pageId, object routeData = null)
		{
			var url = _routingService.GetPageUrl(pageId, routeData);
			HttpContext.Response.Redirect(url);
		}

        public override void ReleaseView()
        {
			View.Load -= View_Load;
            View.Submit -= View_Submit;
    		View.Abort -= View_Abort;
			View.Previous -= View_Previous;
        }

    	private int? RequestCustomerId
    	{
    		get { return HttpContext.Request.Params["customer"].ToNullableInt(); }
    	}

    	private string RequestPersonalIdNumber
    	{
    		get { return HttpContext.Request.Params["personalIdNumber"]; }
    	}

    	private int? RequestOrderId
    	{
    		get { return HttpContext.Request.Params["order"].ToNullableInt(); }
    	}

    	private int ShopId
    	{
    		get { return _synologenMemberService.GetCurrentShopId(); }
    	}

        private void RemoveOrderAndSubscription(int orderId)
        {
            var order = _orderRepository.Get(orderId);
            var isNewSubscription = order.SelectedPaymentOption.Type.Equals(PaymentOptionType.Subscription_Autogiro_New);

            var subscription = order.SubscriptionPayment != null ? order.SubscriptionPayment.Subscription : null;

            _orderRepository.DeleteOrderAndSubscriptionItem(order);
            if (isNewSubscription && subscription != null) _subscriptionRepository.Delete(subscription);
        }
    }
}