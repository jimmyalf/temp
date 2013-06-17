using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
    public class DeviationSupplier : Entity
    {
        public DeviationSupplier()
	    {
	        Active = true;
	    }

        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual bool Active { get; set; }
        public virtual IList<DeviationCategory> Categories { get; set; }
    }
}
