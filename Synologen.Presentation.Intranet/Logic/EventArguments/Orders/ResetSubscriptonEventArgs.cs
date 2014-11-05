using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class ResetSubscriptonEventArgs : EventArgs
	{
		public string BankAccountNumber { get; set; }
		public string ClearingNumber { get; set; }
	}
}