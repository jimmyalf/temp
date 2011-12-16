using System;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
	public interface IOrderView<TEventArgs> : ICommonOrderView<TEventArgs>, IView where TEventArgs : EventArgs
	{ }

	public interface IOrderView<TModel,TEventArgs> : ICommonOrderView<TEventArgs>, IView<TModel> where TModel : class, new() where TEventArgs : EventArgs { }

	public interface ICommonOrderView<TEventArgs> where TEventArgs : EventArgs
	{
		event EventHandler<EventArgs> Previous;
		event EventHandler<EventArgs> Abort;
		event EventHandler<TEventArgs> Submit;
		int AbortPageId { get; set; }
		int PreviousPageId { get; set; }
		int NextPageId { get; set; }
	}
}