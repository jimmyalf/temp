using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class CreateTransactionModel
	{

		public CreateTransactionModel()
		{
			TypeList = CreateTypeList();
		}

		private static List<TransactionListItemModel> CreateTypeList()
		{
			return new List<TransactionListItemModel>
					{
			       		new TransactionListItemModel { Text = "Välj typ", Value = "0" },
						new TransactionListItemModel { Text = TransactionType.Deposit.GetEnumDisplayName(), Value = ((int) TransactionType.Deposit).ToString() },
						new TransactionListItemModel { Text = TransactionType.Withdrawal.GetEnumDisplayName(), Value = ((int) TransactionType.Withdrawal).ToString() }
			       	};

		}

		public  decimal Amount { get; set; }
		public TransactionType Type { get; set; }
		public TransactionReason Reason { get; set; }
		public DateTime CreatedDate { get; set; }

		public IEnumerable<TransactionListItemModel> TypeList { get; set; }

		public bool DisplaySaveWithdrawal
		{
			get
			{
				return Reason == TransactionReason.Withdrawal;
			}
		}

		public bool DisplaySaveCorrection
		{
			get
			{
				return Reason == TransactionReason.Correction;
			}
		}

		public bool DisplayChooseReason
		{
			get
			{
				return (Reason != TransactionReason.Withdrawal
				&& Reason != TransactionReason.Correction);
			}
		}
	}

	public class TransactionListItemModel
	{
		public string Value { get; set; }
		public string Text { get; set; }
	}
}
