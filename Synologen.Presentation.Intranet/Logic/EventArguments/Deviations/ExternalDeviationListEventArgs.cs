using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations
{
    public class ExternalDeviationListEventArgs : EventArgs
	{
        public int SelectedSupplier { get; set; }
        
	}
}