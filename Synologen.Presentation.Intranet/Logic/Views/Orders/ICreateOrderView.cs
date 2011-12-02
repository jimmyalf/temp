using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
    public interface ICreateOrderView : IView<CreateOrderModel>
    {
        int NextPageId { get; set; }
    	event EventHandler<SelectedSupplierEventArgs> SelectedSupplier;
    	event EventHandler<CreateOrderEventArgs> Submit;
    	event EventHandler<SelectedCategoryEventArgs> SelectedCategory;
    }
}