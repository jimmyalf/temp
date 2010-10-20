using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription
{
	public class CustomersForShopMatchingCriteria : IActionCriteria
	{
		public int ShopId { get; set; }
		public string SearchTerm { get; set; }
	}
}
