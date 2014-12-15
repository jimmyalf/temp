using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class ListTransactionModel
	{
		public IEnumerable<SubscriptionTransactionListItemModel> List { get; set; }
		public string CurrentBalance { get; set; }
		public bool HasTransactions { get; set; }
	}

}
