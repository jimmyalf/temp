using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
	public class Deviation : Entity
	{
        public Deviation()
        {
            CreatedDate = DateTime.Now;
            Defects = new List<DeviationDefect>();
            Comments = new List<DeviationComment>();
        }

        public virtual int ShopId { get; set; }
        public virtual DeviationType Type { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DeviationCategory Category { get; set; }
        public virtual string DefectDescription { get; set; }
        public virtual IList<DeviationDefect> Defects { get; set; }
        public virtual IList<DeviationComment> Comments { get; set; }
        public virtual DeviationSupplier Supplier { get; set; }
	}

}