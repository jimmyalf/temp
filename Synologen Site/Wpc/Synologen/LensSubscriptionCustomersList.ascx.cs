using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{
	[PresenterBinding(typeof(ListCustomersPresenter))] 
	public partial class LensSubscriptionCustomersList : MvpUserControl<ListCustomersModel>, IListCustomersView
	{
		public event EventHandler<SearchEventArgs> SearchList;
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
			SearchList(this, new SearchEventArgs
			                 	{
			                 		SearchTerm = txtSearch.Text
			                 	});

		}
	}


}