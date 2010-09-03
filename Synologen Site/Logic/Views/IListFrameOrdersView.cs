using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views
{
	public interface IListFrameOrdersView<T> : IView<T> where T : class, new()
	{
		int ViewPageId { get; set; }
	}
}