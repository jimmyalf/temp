using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class EditCustomerPresenter : OrderBasePresenter<IEditCustomerView>
	{
		private readonly IOrderCustomerRepository _orderCustomerRepository;
		private readonly IViewParser _viewParser;
		private readonly IRoutingService _routingService;

		public EditCustomerPresenter(
			IEditCustomerView view, 
			IOrderCustomerRepository orderCustomerRepository, 
			IViewParser viewParser,
			IRoutingService routingService,
			ISynologenMemberService synologenMemberService) : base(view, synologenMemberService)
		{
			_orderCustomerRepository = orderCustomerRepository;
			_viewParser = viewParser;
			_routingService = routingService;
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
			if(!RequestCustomerId.HasValue) return;
			var customer = _orderCustomerRepository.Get(RequestCustomerId.Value);
			CheckAccess(customer.Shop);
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
			View.Model.ReturnUrl = _routingService.GetPageUrl(View.ReturnPageId);
		}

		public void View_Submit(object sender, EditCustomerEventArgs editCustomerEventArgs)
		{
			if(!RequestCustomerId.HasValue) return;
			var customer = _orderCustomerRepository.Get(RequestCustomerId.Value);
			_viewParser.Fill(customer, editCustomerEventArgs);
			_orderCustomerRepository.Save(customer);
			Redirect(View.ReturnPageId);
		}

		private void Redirect(int pageId)
		{
			var url = _routingService.GetPageUrl(pageId);
			HttpContext.Response.Redirect(url);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
			View.Submit -= View_Submit;
		}

    	private int? RequestCustomerId
    	{
    		get { return HttpContext.Request.Params["customer"].ToNullableInt(); }
    	}
	}
}