using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations
{
	public class InternalDeviationListModel
	{
        public IEnumerable<Deviation> Deviations { get; set; }
        public string ViewDeviationUrl { get; set; }

	}
}