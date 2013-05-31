using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations
{
	public class CreateDeviationEventArgs : EventArgs
	{
        public DeviationType SelectedType { get; set; }
        public int SelectedCategory { get; set; }
        public List<DeviationDefectListItem> SelectedDefects { get; set; }
        public string DefectDescription { get; set; }
        public string Title { get; set; }
        public int SelectedSupplier { get; set; }
        
	}
}