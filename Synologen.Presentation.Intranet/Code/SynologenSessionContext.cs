using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code {
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

		public static IList<CartOrderItem> OrderItemsInCart {
			get { 
				try {return (List<CartOrderItem>) Business.Utility.Session.GetSessionValue(NAME + "OrderItemsInCart");}
				catch{return new List<CartOrderItem>();}
			}
			set { Business.Utility.Session.SetSessionValue(NAME + "OrderItemsInCart", value); }
		}
		public static IList<CartOrderItem> EditOrderItemsInCart{
			get {
				try{return (List<CartOrderItem>)Business.Utility.Session.GetSessionValue(NAME + "EditOrderItemsInCart");}
				catch { return new List<CartOrderItem>(); }
			}
			set { Business.Utility.Session.SetSessionValue(NAME + "EditOrderItemsInCart", value); }
		}
		public static IList<int> OrderItemsMarkedForDeletion {
			get { return Business.Utility.Session.GetSessionIntList(NAME + "OrderMarkedForDeletion"); }
			set { Business.Utility.Session.SetSessionValue(NAME + "OrderMarkedForDeletion", value); }
		}

		public static bool UseDetailedSettlementView {
			get { return Business.Utility.Session.GetSessionValue(NAME + "UseDetailedSettlementView", false); }
			set { Business.Utility.Session.SetSessionValue(NAME + "UseDetailedSettlementView", value); }
		}

		public static ShopAccess MemberShopAccessOptions
		{
			get { return Business.Utility.Session.GetSessionValue(NAME + "MemberShopAccessOptions", ShopAccess.None); }
			set { Business.Utility.Session.SetSessionValue(NAME + "MemberShopAccessOptions", value); }
		}
	}
}