using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
	public interface ISubscriptionItemView : IView<SubscriptionItemModel> 
	{
		event EventHandler<SubmitSubscriptionItemEventArgs> Submit;
	    event EventHandler Stop;
	    event EventHandler Start;
		int ReturnPageId { get; set; }
	}
}