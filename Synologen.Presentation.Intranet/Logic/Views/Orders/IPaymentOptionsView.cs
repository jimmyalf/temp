using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
    public interface IPaymentOptionsView : IView<PaymentOptionsModel>
    {
		int PreviousPageId { get; set; }
		int NextPageId { get; set; }
		int AbortPageId { get; set; }
        event EventHandler<PaymentOptionsEventArgs> Submit;
    	event EventHandler<EventArgs> Abort;
    }
}