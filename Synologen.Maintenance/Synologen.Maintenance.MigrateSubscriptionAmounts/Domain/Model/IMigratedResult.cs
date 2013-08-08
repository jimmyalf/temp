namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public interface IMigratedResult
	{
		string ToString();
		bool Success { get; }
	}
}