using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(PaymentOptionsPresenter))]
    public partial class PaymentOptions : MvpUserControl<PaymentOptionsModel>, IPaymentOptionsView
    {
    	public int PreviousPageId { get; set; }
    	public int NextPageId { get; set; }
    	public int AbortPageId { get; set; }
    	public event EventHandler<PaymentOptionsEventArgs> Submit;
    	public event EventHandler<EventArgs> Abort;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}