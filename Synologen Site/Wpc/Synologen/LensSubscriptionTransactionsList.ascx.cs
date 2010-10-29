using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{
	[PresenterBinding(typeof(ListTransactionsPresenter))]
	public partial class LensSubscriptionTransactionsList : MvpUserControl<ListTransactionModel>, IListTransactionView
	{
		protected void Page_Load(object sender, EventArgs e) { }
	}
}