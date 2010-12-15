using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class CreateTransactionModel
	{

		public string Amount { get; set; }
		public TransactionType Type { get; set; }
		public TransactionReason Reason { get; set; }
		public DateTime CreatedDate { get; set; }

		public IEnumerable<TransactionListItemModel> TypeList {
			get
			{
				return new List<TransactionListItemModel>
				{
					new TransactionListItemModel { Text = "Välj typ", Value = "0" },
					new TransactionListItemModel
					{
						Text = TransactionType.Deposit.GetEnumDisplayName(),
						Value = TransactionType.Deposit.ToInteger().ToString()
					},
					new TransactionListItemModel
					{
						Text = TransactionType.Withdrawal.GetEnumDisplayName(),
						Value = TransactionType.Withdrawal.ToInteger().ToString()
					}
				};
			}
		}

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
	}
}
