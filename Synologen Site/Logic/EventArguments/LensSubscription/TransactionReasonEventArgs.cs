using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription
{
	public class TransactionReasonEventArgs : EventArgs
	{
		public TransactionReason Reason { get; set; }
	}
}
