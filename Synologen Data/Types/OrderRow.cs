// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: OrderRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/OrderRow.cs $
//
//  VERSION
//	$Revision: 7 $
//
//  DATES
//	Last check in: $Date: 09-04-21 17:35 $
//	Last modified: $Modtime: 09-04-14 9:51 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/OrderRow.cs $
//
//7     09-04-21 17:35 Cber
//
//6     09-01-28 14:25 Cber
//
//5     09-01-27 12:29 Cber
//
//4     09-01-27 10:58 Cber
//
//3     09-01-27 10:35 Cber
//
//2     09-01-16 18:15 Cber
//
//1     08-12-22 17:22 Cber
// 
// ==========================================================================

using System;
using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Data.Types {
	public class OrderRow : IOrder {
		//public int Id;
		//public int CompanyId;
		//public int RSTId;
		//public int StatusId;
		//public int SalesPersonShopId;
		//public int SalesPersonMemberId;
		//public string CompanyUnit;
		//public string CustomerFirstName;
		//public string CustomerLastName;
		//public string PersonalIdNumber;
		//public string Phone;
		//public string Email;
		//public System.DateTime CreatedDate = System.DateTime.MinValue;
		//public System.DateTime UpdateDate = System.DateTime.MinValue;
		//public bool MarkedAsPayedByShop;
		//public long InvoiceNumber;
		//public string RstText;
		private int _companyId;
		private string _companyUnit;
		private DateTime _createdDate;
		private string _customerFirstName;
		private string _customerLastNamed;
		private string _email;
		private string _customerLastName;
		private int _id;
		private long _invoiceNumber;
		private bool _markedAsPayedByShop;
		private string _personalIdNumber;
		private string _phone;
		private int _rstId;
		private string _rstText;
		private int _salesPersonShopId;
		private int _statusId;
		private DateTime _updateDate;
		private int _salesPersonMemberId;
		private double _invoiceSumIncludingVAT;
		private double _invoiceSumExcludingVAT;

		public int CompanyId {
			get { return _companyId; }
			set { _companyId = value; }
		}

		public string CompanyUnit {
			get { return _companyUnit; }
			set { _companyUnit = value; }
		}

		public DateTime CreatedDate {
			get { return _createdDate; }
			set { _createdDate = value; }
		}

		public string CustomerFirstName {
			get { return _customerFirstName; }
			set { _customerFirstName = value; }
		}

		public string CustomerLastNamed {
			get { return _customerLastNamed; }
			set { _customerLastNamed = value; }
		}

		public string Email {
			get { return _email; }
			set { _email = value; }
		}

		public string CustomerLastName {
			get { return _customerLastName; }
			set { _customerLastName = value; }
		}

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public long InvoiceNumber {
			get { return _invoiceNumber; }
			set { _invoiceNumber = value; }
		}

		public bool MarkedAsPayedByShop {
			get { return _markedAsPayedByShop; }
			set { _markedAsPayedByShop = value; }
		}

		public string PersonalIdNumber {
			get { return _personalIdNumber; }
			set { _personalIdNumber = value; }
		}

		public string Phone {
			get { return _phone; }
			set { _phone = value; }
		}

		public int RSTId {
			get { return _rstId; }
			set { _rstId = value; }
		}

		public string RstText {
			get { return _rstText; }
			set { _rstText = value; }
		}

		public int SalesPersonShopId {
			get { return _salesPersonShopId; }
			set { _salesPersonShopId = value; }
		}

		public int StatusId {
			get { return _statusId; }
			set { _statusId = value; }
		}

		public DateTime UpdateDate {
			get { return _updateDate; }
			set { _updateDate = value; }
		}

		public int SalesPersonMemberId {
			get { return _salesPersonMemberId; }
			set { _salesPersonMemberId = value; }
		}

		public double InvoiceSumIncludingVAT {
			get { return _invoiceSumIncludingVAT; }
			set { _invoiceSumIncludingVAT = value; }
		}

		public double InvoiceSumExcludingVAT {
			get { return _invoiceSumExcludingVAT; }
			set { _invoiceSumExcludingVAT = value; }
		}
	}
}