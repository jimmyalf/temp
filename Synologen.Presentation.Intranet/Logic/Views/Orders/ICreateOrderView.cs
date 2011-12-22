using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
    public interface ICreateOrderView : IOrderView<CreateOrderModel, CreateOrderEventArgs>
    {
        event EventHandler<SelectedSomethingEventArgs> SelectedArticleType;
        event EventHandler<SelectedSomethingEventArgs> SelectedSupplier;
    	event EventHandler<SelectedSomethingEventArgs> SelectedCategory;
        event EventHandler<SelectedSomethingEventArgs> SelectedArticle;
    }
}