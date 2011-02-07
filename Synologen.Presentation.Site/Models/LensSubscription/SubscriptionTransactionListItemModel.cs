using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class SubscriptionTransactionListItemModel
	{
		public decimal Amount { get; set; }
		public string Type { get; set; }
		public string Reason { get; set; }
		public string CreatedDate { get; set; }
		public string HasSettlement { get; set; }
	}
}
