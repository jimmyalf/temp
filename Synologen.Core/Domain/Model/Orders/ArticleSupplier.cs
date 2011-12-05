namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class ArticleSupplier : Entity
	{
		public string Name { get; set; }

		public OrderShippingOption ShippingOptions { get; set; }
	}
}