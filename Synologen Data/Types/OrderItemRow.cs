// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: OrderItemRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/OrderItemRow.cs $
//
//  VERSION
//	$Revision: 5 $
//
//  DATES
//	Last check in: $Date: 09-02-25 18:00 $
//	Last modified: $Modtime: 09-02-25 14:55 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2008 Spinit AB --- ALL RIGHTS RESERVED
// 	Spinit AB, Datavägen 2, 436 32 Askim, SWEDEN
//
// ==========================================================================
// 
//  DESCRIPTION
//  
//
// ==========================================================================
//
//	History
//
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/OrderItemRow.cs $
//
//5     09-02-25 18:00 Cber
//Changes to allow for SPCS Account property on ArticleConnection,
//OrderItemRow and IOrderItem
//
//4     09-02-19 17:03 Cber
//
//3     09-01-28 14:25 Cber
//
//2     09-01-13 17:53 Cber
//
//1     08-12-23 18:42 Cber
// 
// ==========================================================================
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