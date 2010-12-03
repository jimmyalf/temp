using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.FrameOrders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.FrameOrders
{
	public interface IEditFrameOrderView<T> : IView<T> where T : class, new()
	{
		event EventHandler<EditFrameFormEventArgs> FrameSelected;
		event EventHandler<EditFrameFormEventArgs> SubmitForm;
		event EventHandler<EditFrameFormEventArgs> GlassTypeSelected;
		int RedirectPageId { get; set; }
	}
}