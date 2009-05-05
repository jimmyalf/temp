// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: ContractRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ContractRow.cs $
//
//  VERSION
//	$Revision: 1 $
//
//  DATES
//	Last check in: $Date: 08-12-19 17:22 $
//	Last modified: $Modtime: 08-12-19 13:37 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ContractRow.cs $
//
//1     08-12-19 17:22 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
namespace Spinit.Wpc.Synologen.Data.Types {
	public class ContractRow {
		private int _id;
		public int Id {
			get { return _id; }
			set { _id = value; }
		} 
		private string _code;
		public string Code {
			get { return _code; }
			set { _code = value; }
		}
		private string _name;
		public string Name {
			get { return _name; }
			set { _name = value; }
		} 
		private string _description;
		public string Description {
			get { return _description; }
			set { _description = value; }
		} 
		private string _address;
		public string Address {
			get { return _address; }
			set { _address = value; }
		} 
		private string _address2;
		public string Address2 {
			get { return _address2; }
			set { _address2 = value; }
		} 
		private string _zip;
		public string Zip {
			get { return _zip; }
			set { _zip = value; }
		} 
		private string _city;
		public string City {
			get { return _city; }
			set { _city = value; }
		} 
		private string _phone;
		public string Phone {
			get { return _phone; }
			set { _phone = value; }
		} 
		private string _phone2;
		public string Phone2 {
			get { return _phone2; }
			set { _phone2 = value; }
		} 
		private string _fax;
		public string Fax {
			get { return _fax; }
			set { _fax = value; }
		} 
		private bool _active;
		public bool Active {
			get { return _active; }
			set { _active = value; }
		}
		private string _email;
		public string Email {
			get { return _email; }
			set { _email = value; }
		} 
	}
}