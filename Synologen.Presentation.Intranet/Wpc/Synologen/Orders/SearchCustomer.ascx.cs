using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(SearchCustomerPresenter))]
    public partial class SearchCustomer : MvpUserControl, ISearchCustomerView
    {
		public int EditCustomerPageId { get; set; }
    	public event EventHandler<SearchCustomerEventArgs> Submit;

		protected void Page_Load(object sender, EventArgs e)
		{
			btnNextStep.Click += btnNextStep_OnClick;
		}

    	private void btnNextStep_OnClick(object sender, EventArgs e)
    	{
			if (Submit == null) return;
    		Submit(this, new SearchCustomerEventArgs{PersonalIdNumber = txtPersonalIdNumber.Text});
    	}
    }
}