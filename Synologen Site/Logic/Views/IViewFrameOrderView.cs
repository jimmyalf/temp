using System;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views
{
	public interface IViewFrameOrderView<T> : IView<T> where T : class, new()
	{
		event EventHandler SendOrder;
		int RedirectAfterSentOrderPageId { get; set; }
		int EditPageId { get; set; }
	}
}