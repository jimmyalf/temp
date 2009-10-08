using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Data.Types {
	public class OrderItemRow : IOrderItem {
		private int _id;
		private int _articleId;
		private int _orderId;
		private string _articleDisplayName;
		private float _singlePrice;
		private int _numberOfItems;
		private string _notes;
		private int _temporaryId;
		private string _articleDisplayNumber;
		private float _displayTotalPrice;
		private bool _noVAT;
		private string _SPCSAccountNumber;

		public int TemporaryId {
			get { return _temporaryId; }
			set { _temporaryId = value; }
		}

		public bool IsTemporary {
			get { return (_temporaryId > 0); }
		}

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public int ArticleId {
			get { return _articleId; }
			set { _articleId = value; }
		}

		public string ArticleDisplayName {
			get { return _articleDisplayName; }
			set { _articleDisplayName = value; }
		}

		public float SinglePrice {
			get { return _singlePrice; }
			set { _singlePrice = value; }
		}

		public int NumberOfItems {
			get { return _numberOfItems; }
			set { _numberOfItems = value; }
		}

		public string Notes {
			get { return _notes; }
			set { _notes = value; }
		}

		public string ArticleDisplayNumber {
			get { return _articleDisplayNumber; }
			set { _articleDisplayNumber = value; }
		}

		public float DisplayTotalPrice {
			get { return _displayTotalPrice; }
			set { _displayTotalPrice = value; }
		}

		public int OrderId {
			get { return _orderId; }
			set { _orderId = value; }
		}

		public bool NoVAT {
			get { return _noVAT; }
			set { _noVAT = value; }
		}

		public string SPCSAccountNumber {
			get { return _SPCSAccountNumber; }
			set { _SPCSAccountNumber = value; }
		}
	}
}