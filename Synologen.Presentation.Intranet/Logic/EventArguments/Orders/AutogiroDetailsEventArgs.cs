using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class AutogiroDetailsEventArgs : EventArgs
    {
		//public decimal? OrderTotalWithdrawalAmount { get; set; }
		public decimal ProductPrice { get; set; }
		public decimal FeePrice { get; set; }
    	public int NumberOfPayments { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
    }
}