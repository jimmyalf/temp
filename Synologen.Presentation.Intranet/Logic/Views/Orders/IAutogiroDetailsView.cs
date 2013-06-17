using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
    public interface IAutogiroDetailsView : IOrderView<AutogiroDetailsModel,AutogiroDetailsEventArgs>
    {
        event EventHandler<AutogiroDetailsEventArgs> FillForm;
		event EventHandler<AutogiroDetailsEventArgs> SelectedSubscriptionTimeChanged;
    }
}