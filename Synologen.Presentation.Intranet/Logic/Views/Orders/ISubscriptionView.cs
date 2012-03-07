using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
	public interface ISubscriptionView : IView<SubscriptionModel> 
	{
		event EventHandler<HandleErrorEventArgs> HandleError;
		event EventHandler<EventArgs> StopSubscription;
		event EventHandler<EventArgs> StartSubscription;
		int ReturnPageId { get; set; }
		int CorrectionPageId { get; set; }
		int SubscriptionItemDetailPageId { get; set; }
		int SubscriptionResetPageId { get; set; }
	}
}