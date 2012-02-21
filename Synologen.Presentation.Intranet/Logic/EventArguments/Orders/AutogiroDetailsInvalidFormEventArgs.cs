using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class AutogiroDetailsInvalidFormEventArgs : EventArgs
    {
        public decimal? AutoWithdrawalAmount { get; set; }
        public decimal TaxFreeAmount { get; set; }
        public decimal TaxedAmount { get; set; }
        public string CustomNumberOfPayments { get; set; }
        public int NumberOfPaymentsSelectedValue { get; set; }
        public string BankAccountNumber { get; set; }
        public string ClearingNumber { get; set; }
    }
}