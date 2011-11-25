using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class PickCustomerPresenter : Presenter<IPickCustomerView>
    {
        private readonly IOrderCustomerRepository _orderCustomerRepository;
    	private readonly IViewParser _viewParser;
    	private readonly ISynologenMemberService _synologenMemberService;

    	public PickCustomerPresenter(IPickCustomerView view, IOrderCustomerRepository orderCustomerRepository, IViewParser viewParser, ISynologenMemberService synologenMemberService) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
        	_viewParser = viewParser;
    		_synologenMemberService = synologenMemberService;

    		View.Submit += View_Submit;
            View.FetchCustomerByPersonalIdNumber += FetchCustomerDataByPersonalIdNumber;
        }

        public void View_Submit(object o, PickCustomerEventArgs args)
        {
        	var customer = _viewParser.Parse(args);
			_orderCustomerRepository.Save(customer);
        	Redirect();
        }

    	private void Redirect()
    	{
    		var url = _synologenMemberService.GetPageUrl(View.NextPageId);
			HttpContext.Response.Redirect(url);
    	}

    	public void FetchCustomerDataByPersonalIdNumber(object o, FetchCustomerDataByPersonalIdEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void ReleaseView()
        {
            View.Submit -= View_Submit;
        }
    }

	
}