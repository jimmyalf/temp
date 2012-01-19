using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class CreateOrderConfirmationPresenter : Presenter<ICreateOrderConfirmationView>
    {
    	private readonly IRoutingService _routingService;
        private readonly IOrderRepository _orderRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionItemRepository _subscriptionItemRepository;

    	public CreateOrderConfirmationPresenter(ICreateOrderConfirmationView view, IRoutingService routingService, IOrderRepository orderRepository, ISubscriptionRepository subscriptionRepository, ISubscriptionItemRepository subscriptionItemRepository) : base(view)
    	{
    	    _orderRepository = orderRepository;
    	    _subscriptionRepository = subscriptionRepository;
        	_routingService = routingService;
    	    _subscriptionItemRepository = subscriptionItemRepository;
    		WireupEvents();
        }

		private void WireupEvents()
		{
        	View.Previous += View_Previous;
			View.Abort += View_Abort;
		    View.Load += View_Load;
		}

        public void View_Load(object sender, EventArgs e)
        {
            if(RequestOrderId.HasValue)
            {
                var order = _orderRepository.Get(RequestOrderId.Value);

                View.Model.Address = String.Format("{0} {1}", order.Customer.AddressLineOne??"", order.Customer.AddressLineTwo??"");
                View.Model.City = order.Customer.City;
                View.Model.Email = order.Customer.Email ?? "";
                View.Model.FirstName = order.Customer.FirstName;
                View.Model.LastName = order.Customer.LastName;
                View.Model.MobilePhone = order.Customer.MobilePhone ?? "";
                View.Model.PersonalIdNumber = order.Customer.PersonalIdNumber;
                View.Model.PostalCode = order.Customer.PostalCode;
                View.Model.Telephone = order.Customer.Phone ?? "";

                View.Model.LeftAddition = order.LensRecipe.Addition.Left != null ? order.LensRecipe.Addition.Left.ToString() : "";
                View.Model.LeftAxis = order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Left.ToString() : "";
                View.Model.LeftPower = order.LensRecipe.Power != null ? order.LensRecipe.Power.Left.ToString() : "";
                View.Model.LeftBaseCurve = order.LensRecipe.BaseCurve.Left != null ? order.LensRecipe.BaseCurve.Left.ToString() : "";
                View.Model.LeftDiameter = order.LensRecipe.Diameter.Left != null ? order.LensRecipe.Diameter.Left.ToString() : "";
                View.Model.LeftCylinder = order.LensRecipe.Cylinder.Left != null ? order.LensRecipe.Cylinder.Left.ToString() : "";
                View.Model.RightAddition = order.LensRecipe.Addition.Right != null ? order.LensRecipe.Addition.Right.ToString() : "";
                View.Model.RightAxis = order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Right.ToString() : "";
                View.Model.RightPower = order.LensRecipe.Power != null ? order.LensRecipe.Power.Right.ToString() : "";
                View.Model.RightBaseCurve = order.LensRecipe.BaseCurve.Right != null ? order.LensRecipe.BaseCurve.Right.ToString() : "";
                View.Model.RightDiameter = order.LensRecipe.Diameter.Right != null ? order.LensRecipe.Diameter.Right.ToString() : "";
                View.Model.RightCylinder = order.LensRecipe.Cylinder.Right != null ? order.LensRecipe.Cylinder.Right.ToString() : "";

                View.Model.Article = order.Article.Name;
                
                //TODO: are these correct??
                //View.Model.PaymentOption = GetPaymentOptionString(order.SelectedPaymentOption.Type); 
                View.Model.DeliveryOption = GetDeliveryOptionString(order.ShippingType);
                View.Model.Amount = order.SubscriptionPayment.TaxedAmount + " kr";
                View.Model.SubscriptionTime = GetSubscriptionTimeString(order.SubscriptionPayment.NumberOfPayments);
            }
            
        }

        public void View_Previous(object sender, EventArgs e)
    	{
    		Redirect(View.PreviousPageId, new {order = RequestOrderId});
    	}

    	public void View_Abort(object sender, EventArgs e)
    	{
            //if (RequestOrderId.HasValue)
            //{
            //    var order = _orderRepository.Get(RequestOrderId.Value);
            //
            //    _subscriptionItemRepository.Delete(order.SubscriptionPayment);
            //
            //    if (order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_New)
            //    {
            //        _subscriptionRepository.Delete(order.SubscriptionPayment.Subscription);
            //    }
            //    else if (order.SelectedPaymentOption.Type == PaymentOptionType.Subscription_Autogiro_Existing)
            //    {
            //        _subscriptionItemRepository.Delete(order.SubscriptionPayment);
            //    }
            //
            //    _orderRepository.Delete(order);
            //}
    	    Redirect(View.AbortPageId);
    	}

        private int? RequestOrderId
        {
            get { return HttpContext.Request.Params["order"].ToNullableInt(); }
        }

		private void Redirect(int pageId, object requestParameters = null)
		{
		    var url = _routingService.GetPageUrl(pageId, requestParameters);
		    HttpContext.Response.Redirect(url);
		}

    	public override void ReleaseView()
        {
			View.Previous -= View_Previous;
			View.Abort -= View_Abort;
        }


        #region parsers

        public string GetDeliveryOptionString(OrderShippingOption option)
        {
            switch (option)
            {
                case OrderShippingOption.ToStore:
                    return "Till butik";
                case OrderShippingOption.ToCustomer:
                    return "Till kund";
                case OrderShippingOption.DeliveredInStore:
                    return "Levererad i butik";
                default:
                    return "";
            }
        }

        public string GetSubscriptionTimeString(int? numberOfPayments)
        {
            if (numberOfPayments == null) return "Fortlöpande";
            return String.Format("{0} månader", numberOfPayments.Value);
        }

        #endregion

    }
}