using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class ListTransactionModel
	{
		public IEnumerable<SubscriptionTransactionListItemModel> List { get; set; }
	}

}
