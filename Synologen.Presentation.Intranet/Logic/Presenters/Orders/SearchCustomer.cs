using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class SearchCustomerPresenter : Presenter<ISearchCustomerView>
	{
		private readonly IRoutingService _routingService;
		private readonly IOrderCustomerRepository _orderCustomerRepository;

		public SearchCustomerPresenter(ISearchCustomerView view, IRoutingService routingService, IOrderCustomerRepository orderCustomerRepository) : base(view)
		{
			_routingService = routingService;
			_orderCustomerRepository = orderCustomerRepository;
			View.Submit += View_Submit;
			View.Abort += View_Abort;
		}

		public void View_Submit(object sender, SearchCustomerEventArgs e)
		{
			var customer = _orderCustomerRepository
				.FindBy(new CustomerDetailsFromPersonalIdNumberCriteria {PersonalIdNumber = e.PersonalIdNumber})
				.FirstOrDefault();
			if(customer == null) Redirect(View.NextPageId, new {personalIdNumber = e.PersonalIdNumber});
			else Redirect(View.NextPageId, new {customer = customer.Id});
		}

		public void View_Abort(object sender, EventArgs e)
		{
			Redirect(View.AbortPageId);
		}

		private void Redirect(int pageId, object routeData = null)
		{
			var url = _routingService.GetPageUrl(pageId, routeData);
			HttpContext.Response.Redirect(url);
		}

		public override void ReleaseView()
		{
			View.Submit -= View_Submit;
			View.Abort -= View_Abort;
		}
	}
}