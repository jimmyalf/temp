using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class ArticleSupplier : Entity
	{
		public virtual string Name { get; set; }
		public virtual OrderShippingOption ShippingOptions { get; set; }
	    public virtual IEnumerable<Article> Articles { get; set; }
	}
}