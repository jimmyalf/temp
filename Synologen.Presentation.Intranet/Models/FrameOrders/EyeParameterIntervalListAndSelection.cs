using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders
{
	public class EyeParameterIntervalListAndSelection
	{
		public IEnumerable<IntervalListItem> List { get; set; }
		public EyeParameter Selection { get; set; }

        public EyeParameterIntervalListAndSelection Set(EyeParameter selection, IEnumerable<IntervalListItem> list, string entityName)
        {
            Selection = selection;
            var items = list.ToList();
            items.Insert(0, new IntervalListItem { Name = string.Format("-- Välj {0} --", entityName), Value = int.MinValue.ToString("N2") });
            List = items;
            return this;
        }
	}
}