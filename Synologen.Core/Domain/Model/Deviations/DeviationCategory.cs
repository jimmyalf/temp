using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
	public class DeviationCategory : Entity
	{
	    public DeviationCategory()
	    {
	        Active = true;
	    }

        public virtual string Name { get; set; }
        public virtual IList<Deviation> Deviations { get; set; }
        public virtual IList<DeviationDefect> Defects { get; set; }
        public virtual IList<DeviationSupplier> Suppliers { get; set; }
        public virtual bool Active { get; set; }
	}
}