using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
	public interface ISearchCustomerView : IView
	{
		event EventHandler<SearchCustomerEventArgs> Submit;
		int EditCustomerPageId { get; set; }
	}
}