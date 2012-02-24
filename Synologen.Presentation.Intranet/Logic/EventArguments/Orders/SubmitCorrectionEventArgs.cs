using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SubmitCorrectionEventArgs : EventArgs
	{
		public decimal Amount { get; set; }
		public TransactionType Type { get; set; }

		public SubmitCorrectionEventArgs() { }
		public SubmitCorrectionEventArgs(decimal amount, TransactionType type)
		{
			Amount = amount;
			Type = type;
		}
	}


}