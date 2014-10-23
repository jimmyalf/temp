using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription
{
	public class SaveTransactionEventArgs : EventArgs
	{
		public decimal Amount { get; set; }
		public string TransactionType  { get; set; }
		public string TransactionReason { get; set; }
		public int SelectedArticleValue { get; set; }
	}

	public class UpdateTransactionModelEventArgs : EventArgs
	{
		public string Amount { get; set; }
		public int TransactionType  { get; set; }
		public int SelectedArticleValue { get; set; }
	}
}
