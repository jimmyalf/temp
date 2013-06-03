using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations
{
    public interface ICreateDeviationView : IView<CreateDeviationModel>
    {
        event EventHandler<CreateDeviationEventArgs> Submit;
        event EventHandler<CreateDeviationEventArgs> CategorySelected;
        event EventHandler<CreateDeviationEventArgs> TypeSelected;
    }
}