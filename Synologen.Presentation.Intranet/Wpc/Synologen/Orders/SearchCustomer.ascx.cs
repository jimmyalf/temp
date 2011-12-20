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
		protected void Page_Load(object sender, EventArgs e)
		{
			btnNextStep.Click += btnNextStep_OnClick;
			btnCancel.Click += TryFireAbort;
		}

    	private void btnNextStep_OnClick(object sender, EventArgs e)
    	{
			TryFireSubmit(sender, new SearchCustomerEventArgs{PersonalIdNumber = txtPersonalIdNumber.Text});
    	}
    }
}