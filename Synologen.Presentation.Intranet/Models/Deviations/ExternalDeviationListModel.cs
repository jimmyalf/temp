using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations
{
	public class ExternalDeviationListModel
	{
        public IEnumerable<Deviation> Deviations { get; set; }
        public IEnumerable<DeviationSupplierListItem> Suppliers { get; set; }
        public int SelectedSupplierId { get; set; }

	}
}