using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription
{
	public class SaveSubscriptionEventArgs : EventArgs
	{
		public string AccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public decimal MonthlyAmount { get; set; }
		public string Notes { get; set; }
	}
}