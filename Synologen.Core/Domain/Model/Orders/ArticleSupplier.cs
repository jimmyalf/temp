namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class ArticleSupplier : Entity
	{
		public virtual string Name { get; set; }

		public virtual OrderShippingOption ShippingOptions { get; set; }
	    public virtual ArticleCategory NAME { get; set; }
	}
}