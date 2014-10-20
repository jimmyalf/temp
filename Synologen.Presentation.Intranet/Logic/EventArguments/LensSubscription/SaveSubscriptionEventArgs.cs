using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription
{
	public class SaveSubscriptionEventArgs : EventArgs
	{
		public string AccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public string MonthlyAmount { get; set; }
		public string Notes { get; set; }
	}
}