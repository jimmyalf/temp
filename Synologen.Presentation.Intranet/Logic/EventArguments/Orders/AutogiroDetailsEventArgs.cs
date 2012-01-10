using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class AutogiroDetailsEventArgs : EventArgs
    {
		public decimal? AutoWithdrawalAmount { get; set; }
    	public decimal TaxFreeAmount { get; set; }
    	public decimal TaxedAmount { get; set; }
    	public string Description { get; set; }
    	public string Notes { get; set; }
    	public int? NumberOfPayments { get; set; }
    	public string BankAccountNumber { get; set; }
    	public string ClearingNumber { get; set; }
    }
}