using System;
using System.Linq.Expressions;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
	public class AllOrderableFramesCriteria : IActionCriteria
	{
        public AllOrderableFramesCriteria()
        {
            SortExpression = frame => frame.Name;
        }

	    public AllOrderableFramesCriteria(int? supplierId) : this()
	    {
	        SupplierId = supplierId;
	    }

        public int? SupplierId { get; set; }
	    public Expression<Func<Frame, object>> SortExpression { get; set; } 
	}
}