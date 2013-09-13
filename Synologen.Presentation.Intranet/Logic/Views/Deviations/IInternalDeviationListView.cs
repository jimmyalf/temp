using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations
{
	public interface IInternalDeviationListView : IView<InternalDeviationListModel>
	{
        int? ViewPageId { get; set; }
	}
}