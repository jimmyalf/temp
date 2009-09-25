// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SessionContext.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/SessionContext.cs $
//
//  VERSION
//	$Revision: 5 $
//
//  DATES
//	Last check in: $Date: 09-04-21 17:34 $
//	Last modified: $Modtime: 09-04-14 13:06 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/SessionContext.cs $
//
//5     09-04-21 17:34 Cber
//
//4     09-01-16 18:15 Cber
//
//3     09-01-16 12:18 Cber
//
//2     09-01-12 17:27 Cber
//
//1     08-12-22 17:22 Cber
// 
// ==========================================================================
using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Data.Types;

namespace Spinit.Wpc.Synologen.Presentation.Code {
	public static class SessionContext {

		private const int DefaultPageSize = 40;

		public static class Shops {
			private const string NAME = "Shops";
			public static int PageSize {
				get { return Session.GetSessionValue(NAME + "Size", DefaultPageSize); }
				set { Session.SetSessionValue(NAME + "Size", value); }
			}
			public static int PageIndex {
				get { return Session.GetSessionValue(NAME + "Index", 0); }
				set { Session.SetSessionValue(NAME + "Index", value); }
			}
			public static bool SortAscending {
				get { return Session.GetSessionValue(NAME + "SortAscending", true); }
				set { Session.SetSessionValue(NAME + "SortAscending", value); }
			}
			public static string SortExpression {
				get { return Session.GetSessionValue(NAME + "SortExpression", "cShopName"); }
				set { Session.SetSessionValue(NAME + "SortExpression", value); }
			}
			public static int SelectedCategory {
				get { return Session.GetSessionValue(NAME + "Category", 0); }
				set { Session.SetSessionValue(NAME + "Category", value); }
			}
			public static string SearchExpression {
				get { return Session.GetSessionValue(NAME + "Search", String.Empty); }
				set { Session.SetSessionValue(NAME + "Search", value); }
			}
		}

		public static class ContractCustomers {
			private const string NAME = "Contracts";
			public static int PageSize {
				get { return Session.GetSessionValue(NAME + "IndexPageSize", DefaultPageSize); }
				set { Session.SetSessionValue(NAME + "IndexPageSize", value); }
			}
			public static int PageIndex {
				get { return Session.GetSessionValue(NAME + "IndexPageIndex", 0); }
				set { Session.SetSessionValue(NAME + "IndexPageIndex", value); }
			}
			public static bool SortAscending {
				get { return Session.GetSessionValue(NAME + "IndexPageSortAscending", true); }
				set { Session.SetSessionValue(NAME + "IndexPageSortAscending", value); }
			}
			public static string SortExpression {
				get { return Session.GetSessionValue(NAME + "IndexSortExpression", "cName"); }
				set { Session.SetSessionValue(NAME + "IndexSortExpression", value); }
			}
			public static int SelectedCategory {
				get { return Session.GetSessionValue(NAME + "IndexPageCategory", 0); }
				set { Session.SetSessionValue(NAME + "IndexPageCategory", value); }
			}
			public static string SearchExpression {
				get { return Session.GetSessionValue(NAME + "IndexPageSearch", String.Empty); }
				set { Session.SetSessionValue(NAME + "IndexPageSearch", value); }
			}
		}

		public static class ContractCompanies {
			private const string NAME = "ContractCompanies";
			public static int PageSize {
				get { return Session.GetSessionValue(NAME + "IndexPageSize", DefaultPageSize); }
				set { Session.SetSessionValue(NAME + "IndexPageSize", value); }
			}
			public static int PageIndex {
				get { return Session.GetSessionValue(NAME + "IndexPageIndex", 0); }
				set { Session.SetSessionValue(NAME + "IndexPageIndex", value); }
			}
			public static bool SortAscending {
				get { return Session.GetSessionValue(NAME + "IndexPageSortAscending", true); }
				set { Session.SetSessionValue(NAME + "IndexPageSortAscending", value); }
			}
			public static string SortExpression {
				get { return Session.GetSessionValue(NAME + "IndexSortExpression", "cName"); }
				set { Session.SetSessionValue(NAME + "IndexSortExpression", value); }
			}
			public static int SelectedCategory {
				get { return Session.GetSessionValue(NAME + "IndexPageCategory", 0); }
				set { Session.SetSessionValue(NAME + "IndexPageCategory", value); }
			}
			public static string SearchExpression {
				get { return Session.GetSessionValue(NAME + "IndexPageSearch", String.Empty); }
				set { Session.SetSessionValue(NAME + "IndexPageSearch", value); }
			}
		}


		public static class Orders {
			private const string NAME = "Orders";
			public static int PageSize {
				get { return Session.GetSessionValue(NAME + "IndexPageSize", DefaultPageSize); }
				set { Session.SetSessionValue(NAME + "IndexPageSize", value); }
			}
			public static int PageIndex {
				get { return Session.GetSessionValue(NAME + "IndexPageIndex", 0); }
				set { Session.SetSessionValue(NAME + "IndexPageIndex", value); }
			}
			public static bool SortAscending {
				get { return Session.GetSessionValue(NAME + "IndexPageSortAscending", false); }
				set { Session.SetSessionValue(NAME + "IndexPageSortAscending", value); }
			}
			public static string SortExpression {
				get { return Session.GetSessionValue(NAME + "IndexSortExpression", "cCreatedDate"); }
				set { Session.SetSessionValue(NAME + "IndexSortExpression", value); }
			}
			public static string SearchExpression {
				get { return Session.GetSessionValue(NAME + "IndexPageSearch", String.Empty); }
				set { Session.SetSessionValue(NAME + "IndexPageSearch", value); }
			}
		}

		public static class EditOrder{
			private const string NAME = "SynologenAdminEditOrder";
			public static List<OrderItemRow> EditOrderItemsInCart {
				get {
					try {return (List<OrderItemRow>) Session.GetSessionValue(NAME + "EditOrderItemsInCart");}
					catch {return new List<OrderItemRow>();}
				}
				set { Session.SetSessionValue(NAME + "EditOrderItemsInCart", value); }
			}

			public static List<int> OrderItemsMarkedForDeletion {
				get { return Session.GetSessionIntList(NAME + "OrderItemsMarkedForDeletion"); }
				set { Session.SetSessionValue(NAME + "OrderItemsMarkedForDeletion", value); }
			}
		}



	}
}