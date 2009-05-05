// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: ShopRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ShopRow.cs $
//
//  VERSION
//	$Revision: 3 $
//
//  DATES
//	Last check in: $Date: 09-03-04 14:18 $
//	Last modified: $Modtime: 09-02-26 11:02 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ShopRow.cs $
//
//3     09-03-04 14:18 Cber
//
//2     09-01-28 14:25 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Data.Types {
	public class ShopRow : IShop{
		private int _shopId;
		public int ShopId {
			get { return _shopId; }
			set { _shopId = value; }
		} 
		private string _name;
		public string Name {
			get { return _name; }
			set { _name = value; }
		} 
		private string _number;
		public string Number {
			get { return _number; }
			set { _number = value; }
		} 
		private string _description;
		public string Description {
			get { return _description; }
			set { _description = value; }
		} 
		private bool _active;
		public bool Active {
			get { return _active; }
			set { _active = value; }
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
		private string _email;
		public string Email {
			get { return _email; }
			set { _email = value; }
		} 
		private string _contactFirstName;
		public string ContactFirstName {
			get { return _contactFirstName; }
			set { _contactFirstName = value; }
		} 
		private string _contactLastName;
		public string ContactLastName {
			get { return _contactLastName; }
			set { _contactLastName = value; }
		}
		private int _categoryId;
		public int CategoryId {
			get { return _categoryId; }
			set { _categoryId = value; }
		}
		private string _url;
		public string Url {
			get { return _url; }
			set { _url = value; }
		}
		private string _mapUrl;
		public string MapUrl {
			get { return _mapUrl; }
			set { _mapUrl = value; }
		} 
		private int _giroId;
		public int GiroId {
			get { return _giroId; }
			set { _giroId = value; }
		}
		private string _giroNumber;
		public string GiroNumber {
			get { return _giroNumber; }
			set { _giroNumber = value; }
		}
		private string _giroSupplier;
		public string GiroSupplier {
			get { return _giroSupplier; }
			set { _giroSupplier = value; }
		}
	}
}