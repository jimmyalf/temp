using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class TransactionArticleListItem : IDeleConfigurableGridItem
	{
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public int NumberOfConnectedTransactions { get; set; }
		public bool Active { get; set; }
		public bool AllowDelete { get { return NumberOfConnectedTransactions == 0; } }
	}
}