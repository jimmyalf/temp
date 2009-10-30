using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class OrderItem : IOrderItem {
		private int _temporaryId;

		public int TemporaryId {
			get { return _temporaryId; }
			set { _temporaryId = value; }
		}

		public bool IsTemporary {
			get { return (_temporaryId > 0); }
		}

		public int Id { get; set; }
		public int ArticleId { get; set; }
		public string ArticleDisplayName { get; set; }
		public float SinglePrice { get; set; }
		public int NumberOfItems { get; set; }
		public string Notes { get; set; }
		public string ArticleDisplayNumber { get; set; }
		public float DisplayTotalPrice { get; set; }
		public int OrderId { get; set; }
		public bool NoVAT { get; set; }
		public string SPCSAccountNumber { get; set; }
	}
}