using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views
{
	public interface IFrameOrderView<T> : IView<T> where T : class, new()
	{
		event EventHandler<FrameFormEventArgs> FrameSelected;
		event EventHandler<FrameFormEventArgs> SubmitForm;
		event EventHandler<FrameFormEventArgs> GlassTypeSelected;
	}
}