using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(PaymentOptionsPresenter))]
    public partial class PaymentOptions : OrderUserControl<PaymentOptionsModel, PaymentOptionsEventArgs>, IPaymentOptionsView
    {
    	public override event EventHandler<PaymentOptionsEventArgs> Submit;
    	public override event EventHandler<EventArgs> Abort;
    	public override event EventHandler<EventArgs> Previous;

    	protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}