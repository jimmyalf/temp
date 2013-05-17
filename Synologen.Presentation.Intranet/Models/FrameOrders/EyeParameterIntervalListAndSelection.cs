using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders
{
	public class EyeParameterIntervalListAndSelection
	{
		public IEnumerable<IntervalListItem> List { get; set; }
		public EyeParameter Selection { get; set; }
	}
}