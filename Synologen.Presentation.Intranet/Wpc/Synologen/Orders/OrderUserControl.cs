using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	public abstract class OrderUserControl<TModel, TEventArgs> : MvpUserControl<TModel>, IOrderView<TModel,TEventArgs> where TModel : class, new() where TEventArgs : EventArgs
	{
		public abstract event EventHandler<EventArgs> Previous;
		public abstract event EventHandler<EventArgs> Abort;
		public abstract event EventHandler<TEventArgs> Submit;
		public int AbortPageId { get; set; }
		public int PreviousPageId { get; set; }
		public int NextPageId { get; set; }
	}

	public abstract class OrderUserControl<TEventArgs> : MvpUserControl, IOrderView<TEventArgs> where TEventArgs : EventArgs
	{
		public abstract event EventHandler<EventArgs> Previous;
		public abstract event EventHandler<EventArgs> Abort;
		public abstract event EventHandler<TEventArgs> Submit;
		public int AbortPageId { get; set; }
		public int PreviousPageId { get; set; }
		public int NextPageId { get; set; }
	}
}