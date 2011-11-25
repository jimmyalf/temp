using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class CreateOrderPresenter : Presenter<ICreateOrderView>
    {
        public CreateOrderPresenter(ICreateOrderView view) : base(view)
        {
        }

        public override void ReleaseView()
        {
            
        }
    }
}