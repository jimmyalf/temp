using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class AutogiroDetailsInvalidFormEventArgs : EventArgs
    {
        public decimal ProductPrice { get; set; }
        public decimal FeePrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal MonthlyFee { get; set; }
        public string CustomNumberOfPayments { get; set; }
        public int? NumberOfPaymentsSelectedValue { get; set; }
        public string BankAccountNumber { get; set; }
        public string ClearingNumber { get; set; }
    }
}