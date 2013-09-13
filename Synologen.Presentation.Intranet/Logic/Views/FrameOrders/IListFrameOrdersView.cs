using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders
{
	public interface IListFrameOrdersView<T> : IView<T> where T : class, new()
	{
		int ViewPageId { get; set; }
	}
}