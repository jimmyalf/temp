using System;
using Spinit.Extensions;
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
    	private readonly IRoutingService _routingService;
    	private readonly IOrderRepository _orderRepository;

    	public AutogiroDetailsPresenter(IAutogiroDetailsView view, IRoutingService routingService, IOrderRepository orderRepository) : base(view)
        {
        	_routingService = routingService;
    		_orderRepository = orderRepository;
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
            Redirect(View.PreviousPageId);
    	}

    	public void View_Submit(object sender, AutogiroDetailsEventArgs e)
    	{
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