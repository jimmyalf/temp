using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class AutogiroDetailsPresenter : Presenter<IAutogiroDetailsView>
    {

        private readonly ISynologenMemberService _synologenMemberService;

        public AutogiroDetailsPresenter(IAutogiroDetailsView view, ISynologenMemberService synologenMemberService) : base(view)
        {
            _synologenMemberService = synologenMemberService;

        	View.Load += View_Load;
			View.Abort += View_Abort;
			View.Submit += View_Submit;
			View.Previous += View_Previous;
        }

    	public void View_Previous(object sender, EventArgs e)
    	{
            var url = _synologenMemberService.GetPageUrl(View.PreviousPageId);
            HttpContext.Response.Redirect(url);
    	}

    	public void View_Submit(object sender, AutogiroDetailsEventArgs e)
    	{
            var url = _synologenMemberService.GetPageUrl(View.NextPageId);
            HttpContext.Response.Redirect(url);
    	}

    	public void View_Abort(object sender, EventArgs e)
    	{
            var url = _synologenMemberService.GetPageUrl(View.AbortPageId);
            HttpContext.Response.Redirect(url);
    	}

    	public void View_Load(object sender, EventArgs e)
    	{
    		throw new NotImplementedException();
    	}

    	public override void ReleaseView()
        {
			View.Load += View_Load;
        }
    }
}