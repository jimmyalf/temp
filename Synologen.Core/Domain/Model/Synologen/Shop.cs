namespace Spinit.Wpc.Synologen.Core.Domain.Model.Synologen
{
	public class Shop : Entity
	{
        public virtual string Name { get; set; }
		public virtual string Number { get; set; }
		public virtual ShopGroup ShopGroup { get; set; }
	}
}