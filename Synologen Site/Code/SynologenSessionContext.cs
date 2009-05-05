// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SynologenSessionContext.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Site/Code/SynologenSessionContext.cs $
//
//  VERSION
//	$Revision: 8 $
//
//  DATES
//	Last check in: $Date: 09-04-21 17:35 $
//	Last modified: $Modtime: 09-04-14 12:55 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Site/Code/SynologenSessionContext.cs $
//
//8     09-04-21 17:35 Cber
//
//7     09-04-09 16:39 Cber
//Settlement changes
//
//6     09-04-03 18:06 Cber
//
//5     09-01-16 18:15 Cber
//
//4     09-01-16 12:18 Cber
//
//3     09-01-13 17:53 Cber
//
//2     08-12-23 18:42 Cber
//
//1     08-12-22 17:22 Cber
// 
// ==========================================================================
using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Data.Types;

namespace Spinit.Wpc.Synologen.Presentation.Site.Code {
	public static class SynologenSessionContext {
		private const string NAME = "SynologenSiteSession";
		private const int IntDefaultValue = 0;
		
		public static string SalesListPage {
			get { return Business.Utility.Session.GetSessionValue(NAME + "SalesListPage", "/"); }
			set { Business.Utility.Session.SetSessionValue(NAME + "SalesListPage", value); }			
		}

		public static string SettlementListPage {
			get { return Business.Utility.Session.GetSessionValue(NAME + "SettlementListPage", "/"); }
			set { Business.Utility.Session.SetSessionValue(NAME + "SettlementListPage", value); }
		}

		public static string MemberListPage {
			get { return Business.Utility.Session.GetSessionValue(NAME + "MemberListPage", "/"); }
			set { Business.Utility.Session.SetSessionValue(NAME + "MemberListPage", value); }
		}

		public static int MemberShopId {
			get { return Business.Utility.Session.GetSessionValue(NAME + "MemberShopId", IntDefaultValue); }
			set { Business.Utility.Session.SetSessionValue(NAME + "MemberShopId", value); }
		}

		public static string MemberShopNumber {
			get { return Business.Utility.Session.GetSessionValue(NAME + "MemberShopNumber", string.Empty); }
			set { Business.Utility.Session.SetSessionValue(NAME + "MemberShopNumber", value); }
		}

		public static int MemberId {
			get { return Business.Utility.Session.GetSessionValue(NAME + "MemberId", IntDefaultValue); }
			set { Business.Utility.Session.SetSessionValue(NAME + "MemberId", value); }
		}

		public static DateTime SecurityIsValidUntil {
			get { return Business.Utility.Session.GetSessionValue(NAME + "SecurityIsValidUntil", DateTime.MinValue); }
			set { Business.Utility.Session.SetSessionValue(NAME + "SecurityIsValidUntil", value); }
		}

		public static List<OrderItemRow> OrderItemsInCart {
			get { 
				try {return (List<OrderItemRow>) Business.Utility.Session.GetSessionValue(NAME + "OrderItemsInCart");}
				catch{return new List<OrderItemRow>();}
			}
			set { Business.Utility.Session.SetSessionValue(NAME + "OrderItemsInCart", value); }
		}
		public static List<OrderItemRow> EditOrderItemsInCart{
			get {
				try{return (List<OrderItemRow>)Business.Utility.Session.GetSessionValue(NAME + "EditOrderItemsInCart");}
				catch { return new List<OrderItemRow>(); }
			}
			set { Business.Utility.Session.SetSessionValue(NAME + "EditOrderItemsInCart", value); }
		}
		public static List<int> OrderItemsMarkedForDeletion {
			get { return Business.Utility.Session.GetSessionIntList(NAME + "OrderMarkedForDeletion"); }
			set { Business.Utility.Session.SetSessionValue(NAME + "OrderMarkedForDeletion", value); }
		}

		public static bool UseDetailedSettlementView {
			get { return Business.Utility.Session.GetSessionValue(NAME + "UseDetailedSettlementView", false); }
			set { Business.Utility.Session.SetSessionValue(NAME + "UseDetailedSettlementView", value); }
		}

	}
}