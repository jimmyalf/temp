using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ListTransactionsPresenter))]
	public partial class TransactionsList : MvpUserControl<ListTransactionModel>, IListTransactionView
	{
		protected void Page_Load(object sender, EventArgs e) { }
	}
}