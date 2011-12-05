namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class Article : Entity
	{
		public virtual string Name { get; set; }
		public virtual ArticleSupplier Supplier { get; set; }
		public virtual ArticleCategory Category { get; set; }
	}
}