using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SubmitSubscriptionItemEventArgs : EventArgs
	{
		public decimal TaxedAmount { get; set; }
		public decimal TaxFreeAmount { get; set; }
		public int WithdrawalsLimit { get; set; }
	}
}