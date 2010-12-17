namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class TransactionArticleListItem
	{
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public int NumberOfConnectedTransactions { get; set; }
		public bool Active { get; set; }
		public bool Deletable { get { return NumberOfConnectedTransactions == 0; } }
	}
}