using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views
{
	public interface IEditFrameOrderView<T> : IView<T> where T : class, new()
	{
		event EventHandler<EditFrameFormEventArgs> FrameSelected;
		event EventHandler<EditFrameFormEventArgs> SubmitForm;
		event EventHandler<EditFrameFormEventArgs> GlassTypeSelected;
		int RedirectPageId { get; set; }
	}
}