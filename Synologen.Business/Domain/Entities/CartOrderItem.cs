using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class CartOrderItem : OrderItem {

		public CartOrderItem(IOrderItem orderItem) : base(orderItem){}
		public CartOrderItem() {}

		private int _temporaryId;
		public int TemporaryId {
			get { return _temporaryId; }
			set { _temporaryId = value; }
		}
		public bool IsTemporary {
			get { return (_temporaryId > 0); }
		}
	}
}