namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class Shop
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual ShopAddress Address { get; set; }
	}
}