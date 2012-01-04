using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class CreateOrderConfirmationPresenter : Presenter<ICreateOrderConfirmationView>
    {
    	private readonly IRoutingService _routingService;

    	public CreateOrderConfirmationPresenter(ICreateOrderConfirmationView view, IRoutingService routingService) : base(view)
        {
        	_routingService = routingService;
        	View.Previous -= View_Previous;
			View.Abort -= View_Abort;
        }

    	private void View_Previous(object sender, EventArgs e)
    	{
    		Redirect(View.PreviousPageId, new {order = OrderId});
    	}

    	private void View_Abort(object sender, EventArgs e)
    	{
			//TODO: Delete order
    		//var order = _orderRepository.Get(OrderId);
			//_orderRepository.Delete(order);
    		Redirect(View.AbortPageId);
    	}

    	public override void ReleaseView()
        {

        }

    	private int OrderId
    	{
			get { return HttpContext.Request.Params["order"].ToInt(); }
    	}

		private void Redirect(int pageId, object requestParameters = null)
		{
		    var url = _routingService.GetPageUrl(pageId, requestParameters);
		    HttpContext.Response.Redirect(url);
		}
    }
}