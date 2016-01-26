using System;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(ListSubscriptionErrorsPresenter))]
	public partial class ErrorList : MvpUserControl<ListSubscriptionErrorModel>, IListSubscriptionErrorView
	{
		public event EventHandler<SetErrorHandledEventArgs> SetHandled;

		public void SetHandled_ItemCommand(object sender, RepeaterCommandEventArgs e)
		{
			var args = new SetErrorHandledEventArgs{ ErrorId = e.CommandName.ToInt() };
			SetHandled(this, args);
		}
	}
}