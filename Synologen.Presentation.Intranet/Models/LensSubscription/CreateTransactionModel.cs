using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class CreateTransactionModel
	{

		public CreateTransactionModel()
		{
			Articles = Enumerable.Empty<ListItem>();
		}
		public string Amount { get; set; }
		public TransactionType Type { get; set; }
		public TransactionReason Reason { get; set; }
		public DateTime CreatedDate { get; set; }

		public IEnumerable<ListItem> TypeList { get { return GetList(); } }

		public bool DisplaySaveWithdrawal { get { return Reason == TransactionReason.Withdrawal; } }

		public bool DisplaySaveCorrection { get { return Reason == TransactionReason.Correction; } }

		public bool DisplayChooseReason { 
			get
			{
				return (Reason != TransactionReason.Withdrawal
				&& Reason != TransactionReason.Correction);
			}
		}

		public int SelectedTransactionType { get; set; }
		public int SelectedArticleValue { get; set; }
		public IEnumerable<ListItem> Articles { get; set; }

		private static IEnumerable<ListItem> GetList()
		{
			return new List<ListItem>
			{
				new ListItem { Text = "Välj typ", Value = "0" },
				new ListItem
				{
					Text = TransactionType.Deposit.GetEnumDisplayName(),
					Value = TransactionType.Deposit.ToInteger().ToString()
				},
				new ListItem
				{
					Text = TransactionType.Withdrawal.GetEnumDisplayName(),
					Value = TransactionType.Withdrawal.ToInteger().ToString()
				}
			};
		}
		
	}
}
