using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	public abstract class OrderUserControl<TModel, TEventArgs> : MvpUserControl<TModel>, IOrderView<TModel,TEventArgs> where TModel : class, new() where TEventArgs : EventArgs
	{
		public event EventHandler<EventArgs> Previous;
		public event EventHandler<EventArgs> Abort;
		public event EventHandler<TEventArgs> Submit;
		public int AbortPageId { get; set; }
		public int PreviousPageId { get; set; }
		public int NextPageId { get; set; }


		protected void TryFirePrevious(object sender, EventArgs args)
		{
			TryFireEvent(Previous, sender, args);
		}
		protected void TryFireAbort(object sender, EventArgs args)
		{
			TryFireEvent(Abort, sender, args);
		}
		protected void TryFireSubmit(object sender, TEventArgs args)
		{
			TryFireEvent(Submit, sender, args);
		}
		private static void TryFireEvent<TEArgs>(EventHandler<TEArgs> selectedEvent, object sender, TEArgs args) where TEArgs : EventArgs
		{
			if(selectedEvent == null) return;
			selectedEvent(sender, args);
		}

	}

	public abstract class OrderUserControl<TEventArgs> : MvpUserControl, IOrderView<TEventArgs> where TEventArgs : EventArgs
	{
		public event EventHandler<EventArgs> Previous;
		public event EventHandler<EventArgs> Abort;
		public event EventHandler<TEventArgs> Submit;
		public int AbortPageId { get; set; }
		public int PreviousPageId { get; set; }
		public int NextPageId { get; set; }

		protected void TryFirePrevious(object sender, EventArgs args)
		{
			TryFireEvent(Previous, sender, args);
		}
		protected void TryFireAbort(object sender, EventArgs args)
		{
			TryFireEvent(Abort, sender, args);
		}
		protected void TryFireSubmit(object sender, TEventArgs args)
		{
			TryFireEvent(Submit, sender, args);
		}
		private static void TryFireEvent<TEArgs>(EventHandler<TEArgs> selectedEvent, object sender, TEArgs args) where TEArgs : EventArgs
		{
			if(selectedEvent == null) return;
			selectedEvent(sender, args);
		}
	}
}