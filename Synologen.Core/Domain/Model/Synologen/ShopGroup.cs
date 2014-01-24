using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Synologen
{
	public class ShopGroup : Entity
	{
		public virtual string Name { get; set; }
		public virtual IList<Shop> Shops { get; set; }
	}
}