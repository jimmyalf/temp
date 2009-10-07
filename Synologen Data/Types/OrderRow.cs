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

		public int CompanyId { get; set; }

		public string CompanyUnit { get; set; }

		public DateTime CreatedDate { get; set; }

		public string CustomerFirstName { get; set; }

		public string CustomerOrderNumber { get; set; }

		public string Email { get; set; }

		public string CustomerLastName { get; set; }

		public int Id { get; set; }

		public long InvoiceNumber { get; set; }

		public bool MarkedAsPayedByShop { get; set; }

		public string PersonalIdNumber { get; set; }

		public string Phone { get; set; }

		public int RSTId { get; set; }

		public string RstText { get; set; }

		public int SalesPersonShopId { get; set; }

		public int StatusId { get; set; }

		public DateTime UpdateDate { get; set; }

		public int SalesPersonMemberId { get; set; }

		public double InvoiceSumIncludingVAT { get; set; }

		public double InvoiceSumExcludingVAT { get; set; }

		public string CustomerCombinedName {
			get { return String.Concat( CustomerFirstName ?? String.Empty,  " ",  CustomerLastName ?? String.Empty ).Trim(); }
		}
	}
}