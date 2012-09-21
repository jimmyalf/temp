using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class AutogiroDetailsEventArgs : EventArgs
    {
		public decimal ProductPrice { get; set; }
		public decimal FeePrice { get; set; }
    	public int? NumberOfPayments { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
		public decimal MonthlyFee { get; set; }
		public decimal MonthlyProduct { get; set; }
		public bool IsOngoing { get { return !NumberOfPayments.HasValue; } }
    }
}