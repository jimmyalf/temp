using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ListCustomersPresenter))] 
	public partial class CustomersList : MvpUserControl<ListCustomersModel>, IListCustomersView
	{
		public event EventHandler<SearchEventArgs> SearchList;
		//public event EventHandler<SearchAndSortEventArgs> SearchWithSortList;
		public int EditPageId { get; set;}

		protected void Page_Load(object sender, EventArgs e)
		{
			WireupEventProxy();
		}

		private void WireupEventProxy()
		{
			btnSearch.Click += (sender, e) => HandleEvent();
		}

		private void HandleEvent()
		{
			if (SearchList == null) return;
			SearchList(this, new SearchEventArgs {SearchTerm = txtSearch.Text});

		}
	}
}