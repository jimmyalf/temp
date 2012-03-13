using System;
using Synologen.Migration.AutoGiro2.Migrators;
using NewTransactionType = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.TransactionType;
using OldTransactionType = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.TransactionType;

namespace Synologen.Migration.AutoGiro2.Commands
{
	public class TransactionTypeMigrator : IMigrator<OldTransactionType, NewTransactionType> 
	{
		public NewTransactionType GetNewEntity(OldTransactionType oldEntity)
		{
			switch (oldEntity)
			{
				case OldTransactionType.Deposit: return NewTransactionType.Deposit;
				case OldTransactionType.Withdrawal: return NewTransactionType.Withdrawal;
				default: throw new ArgumentOutOfRangeException("oldEntity");
			}
		}
	}
}