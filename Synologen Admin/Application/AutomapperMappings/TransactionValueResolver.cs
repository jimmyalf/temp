using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class TransactionValueResolver : ValueResolver<IEnumerable<SubscriptionTransaction>, IEnumerable<TransactionListItemView>>
	{
		protected override IEnumerable<TransactionListItemView> ResolveCore(IEnumerable<SubscriptionTransaction> source) 
		{
			Func<SubscriptionTransaction,TransactionListItemView> converter = transaction => new TransactionListItemView
			{
				DepositAmount =  (transaction.Type.Equals(TransactionType.Deposit)) ? transaction.Amount.ToString("C2", new CultureInfo("sv-SE")) : String.Empty,
				WithdrawalAmount =  (transaction.Type.Equals(TransactionType.Withdrawal)) ? transaction.Amount.Invert().ToString("C2", new CultureInfo("sv-SE")) : String.Empty,
				Date = transaction.CreatedDate.ToString("yyyy-MM-dd"),
				Reason = transaction.Reason.GetEnumDisplayName(),
			};
			return source.Select(converter);
		}
	}
}