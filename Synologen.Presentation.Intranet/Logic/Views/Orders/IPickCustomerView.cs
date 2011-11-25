using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
    public interface IPickCustomerView : IView<PickCustomerModel>
    {
        event EventHandler<PickCustomerEventArgs> Submit;
        event EventHandler<FetchCustomerDataByPersonalIdEventArgs> FetchCustomerByPersonalIdNumber;
    }
}