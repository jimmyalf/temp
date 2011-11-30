namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionTransactionArticle : Entity
	{
		public virtual string Name { get; set; }
		public virtual bool Active { get; set; }
		public virtual int NumberOfConnectedTransactions { get; private set; }
	}
}