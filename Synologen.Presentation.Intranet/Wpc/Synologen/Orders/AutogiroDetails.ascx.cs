using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(AutogiroDetailsPresenter))]
    public partial class AutogiroDetails : OrderUserControl<AutogiroDetailsModel, AutogiroDetailsEventArgs>, IAutogiroDetailsView
    {
    	public override event EventHandler<EventArgs> Previous;
    	public override event EventHandler<EventArgs> Abort;
		public override event EventHandler<AutogiroDetailsEventArgs> Submit;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}