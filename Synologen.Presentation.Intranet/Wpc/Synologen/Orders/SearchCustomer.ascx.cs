using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(SearchCustomerPresenter))]
    public partial class SearchCustomer : OrderUserControl<SearchCustomerEventArgs>, ISearchCustomerView
    {
    	public override event EventHandler<EventArgs> Previous;
    	public override event EventHandler<EventArgs> Abort;
		public override event EventHandler<SearchCustomerEventArgs> Submit;

		protected void Page_Load(object sender, EventArgs e)
		{
			btnNextStep.Click += btnNextStep_OnClick;
			btnCancel.Click += btnCancel_OnClick;
		}

    	private void btnCancel_OnClick(object sender, EventArgs e)
    	{
    		if(Abort == null) return;
			Abort(this, e);
    	}

    	private void btnNextStep_OnClick(object sender, EventArgs e)
    	{
			if (Submit == null) return;
    		Submit(this, new SearchCustomerEventArgs{PersonalIdNumber = txtPersonalIdNumber.Text});
    	}
    }
}