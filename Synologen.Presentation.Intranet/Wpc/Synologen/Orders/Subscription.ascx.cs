using System;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	[PresenterBinding(typeof(SubscriptionPresenter))] 
	public partial class Subscription : MvpUserControl<SubscriptionModel>, ISubscriptionView
	{
		public event EventHandler<HandleErrorEventArgs> HandleError;

		protected void SetHandled_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if(HandleError == null) return;
			var errorId = e.CommandName.ToInt();
			HandleError(this, new HandleErrorEventArgs(errorId));
		}
	}
}