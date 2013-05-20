using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription
{
	public class TransactionReasonEventArgs : EventArgs
	{
		public TransactionReason Reason { get; set; }
	}
}
