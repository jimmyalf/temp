using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SubmitCorrectionEventArgs : EventArgs
	{
		public SubscriptionAmount Amount { get; set; }
		public decimal TaxFreeAmount { get; set; }
		public TransactionType Type { get; set; }

		public SubmitCorrectionEventArgs() { }
		public SubmitCorrectionEventArgs(decimal taxedAmount, decimal taxFreeAmount, TransactionType type)
		{
			Amount = new SubscriptionAmount(taxedAmount, taxFreeAmount);
			Type = type;
		}
	}


}