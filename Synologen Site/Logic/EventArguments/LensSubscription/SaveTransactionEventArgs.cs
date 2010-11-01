using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription
{
	public class SaveTransactionEventArgs : EventArgs
	{
		public decimal Amount { get; set; }
		public string TransactionType  { get; set; }
		public string TransactionReason { get; set; }
	}
}
