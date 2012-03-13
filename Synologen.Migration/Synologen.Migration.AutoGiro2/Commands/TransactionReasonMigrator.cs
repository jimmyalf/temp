using System;
using Synologen.Migration.AutoGiro2.Migrators;
using NewTransactionReason = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.TransactionReason;
using OldTransactionReason = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.TransactionReason;

namespace Synologen.Migration.AutoGiro2.Commands
{
	public class TransactionReasonMigrator : IMigrator<OldTransactionReason, NewTransactionReason> 
	{
		public NewTransactionReason GetNewEntity(OldTransactionReason oldEntity)
		{
			switch (oldEntity)
			{
				case OldTransactionReason.Payment: return NewTransactionReason.Payment;
				case OldTransactionReason.Withdrawal: return NewTransactionReason.Withdrawal;
				case OldTransactionReason.Correction: return NewTransactionReason.Correction;
				case OldTransactionReason.PaymentFailed: return NewTransactionReason.PaymentFailed;
				default: throw new ArgumentOutOfRangeException("oldEntity");
			}
		}
	}
}