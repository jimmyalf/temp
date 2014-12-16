using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
    public interface ICreateOrderView : IOrderView<CreateOrderModel, OrderChangedEventArgs>
    {
        event EventHandler<OrderChangedEventArgs> SelectedArticleType;
        event EventHandler<OrderChangedEventArgs> SelectedSupplier;
    	event EventHandler<OrderChangedEventArgs> SelectedCategory;
        event EventHandler<OrderChangedEventArgs> SelectedArticle;
    	event EventHandler<OrderChangedEventArgs> SelectedOnlyOneEye;
    }
}