using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ListSubscriptionErrorsPresenter))]
	public partial class ErrorList : MvpUserControl<ListSubscriptionErrorModel>, IListSubscriptionErrorView
	{
		public event EventHandler<SetErrorHandledEventArgs> SetHandled;

		protected void Page_Load(object sender, EventArgs args) { }

		public void SetHandled_ItemCommand(object sender, RepeaterCommandEventArgs e)
		{
			var args = new SetErrorHandledEventArgs { ErrorId = int.Parse(e.CommandName) };
			SetHandled(this, args);
		}

	}
}