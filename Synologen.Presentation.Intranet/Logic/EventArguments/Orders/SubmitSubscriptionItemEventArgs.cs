using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SubmitSubscriptionItemEventArgs : EventArgs
	{
		public decimal FeeAmount { get; set; }
		public decimal ProductAmount { get; set; }
		public int WithdrawalsLimit { get; set; }
		public decimal CustomMonthlyProductAmount { get; set; }
		public decimal CustomMonthlyFeeAmount { get; set; }
	}
}