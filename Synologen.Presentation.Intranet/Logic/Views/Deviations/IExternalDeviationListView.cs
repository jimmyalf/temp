using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations
{
	public interface IExternalDeviationListView : IView<ExternalDeviationListModel>
	{
        event EventHandler<ExternalDeviationListEventArgs> SupplierSelected;
        int? ViewPageId { get; set; }
	}
}