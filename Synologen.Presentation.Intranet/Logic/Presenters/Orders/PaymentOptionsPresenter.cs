using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class PaymentOptionsPresenter : Presenter<IPaymentOptionsView>
    {
    	private readonly IOrderRepository _orderRepository;
    	private readonly ISynologenMemberService _synologenMemberService;

    	public PaymentOptionsPresenter(IPaymentOptionsView view, IOrderRepository orderRepository, ISynologenMemberService synologenMemberService) : base(view)
        {
        	_orderRepository = orderRepository;
    		_synologenMemberService = synologenMemberService;
    		View.Abort += View_Abort;
        	View.Submit += View_Submit;
        }

    	public void View_Submit(object sender, PaymentOptionsEventArgs args)
    	{
    		var orderId = HttpContext.Request.Params["order"].ToInt();
    		var order = _orderRepository.Get(orderId);
    		UpdateOrderWithPaymentOption(order, args);
			_orderRepository.Save(order);
			Redirect(View.NextPageId, order.Id);
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
			}
		}

        public override void ReleaseView()
        {
			View.Abort -= View_Abort;
			View.Submit -= View_Submit;
        }

    	public void View_Abort(object sender, EventArgs eventArgs)
    	{
    		var orderId = HttpContext.Request.Params["order"].ToInt();
    		var order = _orderRepository.Get(orderId);
			_orderRepository.Delete(order);
			Redirect(View.AbortPageId, order.Id);
    	}

		private void Redirect(int pageId, int orderId)
		{
			var url = _synologenMemberService.GetPageUrl(pageId);
			var redirectUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = url, OrderId = orderId});
			HttpContext.Response.Redirect(redirectUrl);
		}
    }
}