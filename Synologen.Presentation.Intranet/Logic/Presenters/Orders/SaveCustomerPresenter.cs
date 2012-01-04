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
    	private readonly IViewParser _viewParser;
    	private readonly ISynologenMemberService _synologenMemberService;

    	public SaveCustomerPresenter(ISaveCustomerView view, IOrderCustomerRepository orderCustomerRepository, IViewParser viewParser, ISynologenMemberService synologenMemberService) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
        	_viewParser = viewParser;
    		_synologenMemberService = synologenMemberService;
    		View.Load += View_Load;
    		View.Submit += View_Submit;
    		View.Abort += View_Abort;
			View.Previous += View_Previous;
        }

    	public void View_Previous(object sender, EventArgs e)
    	{
    		Redirect(View.PreviousPageId);
    	}

    	public void View_Abort(object sender, EventArgs e)
    	{
    		Redirect(View.AbortPageId);
    	}

    	public void View_Load(object o, EventArgs eventArgs)
    	{
    		var customerId = HttpContext.Request.Params["customer"];
			var personalIdNumber = HttpContext.Request.Params["personalIdNumber"];
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
        	Redirect(View.NextPageId, customer.Id);
        }

    	private void Redirect(int pageId, int? customerId = null)
    	{
    		var url = _synologenMemberService.GetPageUrl(pageId);
			if (customerId.HasValue) url += "?customer=" + customerId;
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