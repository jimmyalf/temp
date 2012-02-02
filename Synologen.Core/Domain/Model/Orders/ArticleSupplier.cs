using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class ArticleSupplier : Entity
	{
		public ArticleSupplier()
		{
			Active = true;
		}
		public virtual string Name { get; set; }
		public virtual string OrderEmailAddress { get; set; }
		public virtual OrderShippingOption ShippingOptions { get; set; }
	    public virtual IEnumerable<Article> Articles { get; set; }
		public virtual bool Active { get; set; }
	}
}