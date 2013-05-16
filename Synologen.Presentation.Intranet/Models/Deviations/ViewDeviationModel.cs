using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations
{
    public class ViewDeviationModel
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public DeviationType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DeviationCategory Category { get; set; }
        public string DefectDescription { get; set; }
        public IList<DeviationDefect> Defects { get; set; }
        public DeviationSupplier Supplier { get; set; }
        public IList<DeviationComment> Comments { get; set; }
    }

}