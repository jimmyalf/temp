namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model.Enums
{
	public enum TransactionReason
	{
		Deposit = 1,
		Withdrawal = 2,
		Correction = 3,
		FailedWithdrawal = 4
	}
}