using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class SaveCustomerPresenter : Presenter<ISaveCustomerView>
    {
        private readonly IOrderCustomerRepository _orderCustomerRepository;
        private readonly IOrderRepository _orderRepository;
    	private readonly IViewParser _viewParser;
    	private readonly ISynologenMemberService _synologenMemberService;

    	public SaveCustomerPresenter(ISaveCustomerView view, IOrderCustomerRepository orderCustomerRepository, IOrderRepository orderRepository, IViewParser viewParser, ISynologenMemberService synologenMemberService) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
    	    _orderRepository = orderRepository;
        	_viewParser = viewParser;
    		_synologenMemberService = synologenMemberService;
    		View.Load += View_Load;
    		View.Submit += View_Submit;
    		View.Abort += View_Abort;
			View.Previous += View_Previous;
        }

    	public void View_Previous(object sender, EventArgs e)
    	{
    		RedirectWithCustomerId(View.PreviousPageId);
    	}

    	public void View_Abort(object sender, EventArgs e)
    	{
    		RedirectWithCustomerId(View.AbortPageId);
    	}

    	public void View_Load(object o, EventArgs eventArgs)
    	{
    		string customerId;
			string personalIdNumber;
            if(HttpContext.Request.Params["customer"] != null)
            {
                customerId = HttpContext.Request.Params["customer"];
                personalIdNumber = HttpContext.Request.Params["personalIdNumber"];
            }
            else
            {
                var order = _orderRepository.Get(Convert.ToInt32(HttpContext.Request.Params["order"]));
                customerId = order.Customer.Id.ToString();
                personalIdNumber = order.Customer.PersonalIdNumber;
                View.Model.OrderId = order.Id;
            }
    	    UpdateCustomer(customerId, personalIdNumber);
    	}

		private void UpdateCustomer(string customerId, string personalIdNumber)
		{
			if(customerId == null)
			{
				View.Model.PersonalIdNumber = personalIdNumber;
				View.Model.DisplayCustomerMissingMessage = true;
			}
			else
			{
				var customerIdValue = Convert.ToInt32(customerId);
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
    			View.Model.CustomerId = customer.Id;
			}
		}

        public void View_Submit(object o, SaveCustomerEventArgs args)
        {
        	OrderCustomer customer;
			if(args.CustomerId.HasValue)
			{
				customer = _orderCustomerRepository.Get(args.CustomerId.Value);
				_viewParser.Fill(customer, args);
			}
			else
			{
				customer = _viewParser.Parse(args);
			}
			_orderCustomerRepository.Save(customer);

            if(args.OrderId != null)
            {
                RedirectWithOrderId(View.NextPageId, args.OrderId);
            }
            else
            {
                RedirectWithCustomerId(View.NextPageId, customer.Id);    
            }
        }

    	private void RedirectWithCustomerId(int pageId, int? customerId = null)
    	{
    		var url = _synologenMemberService.GetPageUrl(pageId);
			if (customerId.HasValue) url += "?customer=" + customerId;
			HttpContext.Response.Redirect(url);
    	}

        private void RedirectWithOrderId(int pageId, int? orderId = null)
        {
            var url = _synologenMemberService.GetPageUrl(pageId);
            if (orderId.HasValue) url += "?order=" + orderId;
            HttpContext.Response.Redirect(url);
        }

        public override void ReleaseView()
        {
			View.Load -= View_Load;
            View.Submit -= View_Submit;
    		View.Abort -= View_Abort;
			View.Previous -= View_Previous;
        }
    }
}