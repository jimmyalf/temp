using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations
{
    public interface IViewDeviationView : IView<ViewDeviationModel>
	{
        event EventHandler<ViewDeviationEventArgs> Submit;
	}
}