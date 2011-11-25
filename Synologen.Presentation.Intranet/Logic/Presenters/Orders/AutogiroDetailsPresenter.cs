using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class AutogiroDetailsPresenter : Presenter<IAutogiroDetailsView>
    {
        public AutogiroDetailsPresenter(IAutogiroDetailsView view) : base(view)
        {
        }

        public override void ReleaseView()
        {

        }
    }
}