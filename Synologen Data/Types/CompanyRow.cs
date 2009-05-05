// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: CompanyRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/CompanyRow.cs $
//
//  VERSION
//	$Revision: 3 $
//
//  DATES
//	Last check in: $Date: 09-02-25 18:00 $
//	Last modified: $Modtime: 09-02-24 10:56 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/CompanyRow.cs $
//
//3     09-02-25 18:00 Cber
//Changes to allow for SPCS Account property on ArticleConnection,
//OrderItemRow and IOrderItem
//
//2     09-02-10 17:41 Cber
//Name change (ContractCustomerRow -> CustomerRow, IContractCustomer ->
//ICustomer)
//
//1     09-02-05 18:01 Cber
//
//3     09-01-27 10:58 Cber
//
//2     08-12-23 18:42 Cber
//
//1     08-12-19 17:22 Cber
// 
// ==========================================================================
using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Data.Types {
	public class CompanyRow : ICompany {
		private int _id;
		private int _contractId;
		private string _name;
		private string _address1;
		private string _address2;
		private string _zip;
		private string _city;
		private string _companyCode;
		private string _bankCode;
		private bool _active;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public int ContractId {
			get { return _contractId; }
			set { _contractId = value; }
		}

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public string Address1 {
			get { return _address1; }
			set { _address1 = value; }
		}

		public string Address2 {
			get { return _address2; }
			set { _address2 = value; }
		}

		public string Zip {
			get { return _zip; }
			set { _zip = value; }
		}

		public string City {
			get { return _city; }
			set { _city = value; }
		}

		public string CompanyCode {
			get { return _companyCode; }
			set { _companyCode = value; }
		}

		public string BankCode {
			get { return _bankCode; }
			set { _bankCode = value; }
		}
		public bool Active {
			get { return _active; }
			set { _active = value; }
		}
	}
}