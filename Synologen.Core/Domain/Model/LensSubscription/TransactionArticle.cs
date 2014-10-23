namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class TransactionArticle : Entity
	{
		public virtual string Name { get; set; }
		public virtual bool Active { get; set; }
		public virtual int NumberOfConnectedTransactions { get; private set; }
	}
}