using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Utility;

namespace Spinit.Wpc.Synologen.Presentation.Code {
	public static class SessionContext {

		private const int DefaultPageSize = 30;

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

		public static class EditOrder{
			private const string NAME = "SynologenAdminEditOrder";
			public static IList<CartOrderItem> EditOrderItemsInCart {
				get {
					try {return (List<CartOrderItem>) Session.GetSessionValue(NAME + "EditOrderItemsInCart");}
					catch {return new List<CartOrderItem>();}
				}
				set { Session.SetSessionValue(NAME + "EditOrderItemsInCart", value); }
			}

			public static IList<int> OrderItemsMarkedForDeletion {
				get { return Session.GetSessionIntList(NAME + "OrderItemsMarkedForDeletion"); }
				set { Session.SetSessionValue(NAME + "OrderItemsMarkedForDeletion", value); }
			}
		}

		public static PageSortSearchSettings Orders
		{
			get { return new PageSortSearchSettings("Orders", "cCreatedDate"); }
		}

		public static PageSortSearchSettings ContractSalesArticles
		{
			get { return new PageSortSearchSettings("ContractSalesArticles", "Id"); }
		}

		public class PageSortSettings
		{
			protected readonly string Name;
			protected readonly string DefaultSortExpression;

			public PageSortSettings(string uniqueSessionName, string defaultSortExpression)
			{
				Name = uniqueSessionName;
				DefaultSortExpression = defaultSortExpression;
			}
			public int PageSize {
				get { return Session.GetSessionValue(Name + "IndexPageSize", DefaultPageSize); }
				set { Session.SetSessionValue(Name + "IndexPageSize", value); }
			}
			public int PageIndex {
				get { return Session.GetSessionValue(Name + "IndexPageIndex", 0); }
				set { Session.SetSessionValue(Name + "IndexPageIndex", value); }
			}
			public bool SortAscending {
				get { return Session.GetSessionValue(Name + "IndexPageSortAscending", true); }
				set { Session.SetSessionValue(Name + "IndexPageSortAscending", value); }
			}
			public string SortExpression {
				get { return Session.GetSessionValue(Name + "IndexSortExpression", DefaultSortExpression); }
				set { Session.SetSessionValue(Name + "IndexSortExpression", value); }
			}
		}

		public class PageSortSearchSettings : PageSortSettings
		{
			public PageSortSearchSettings(string uniqueSessionName, string defaultSortExpression) 
				: base(uniqueSessionName, defaultSortExpression) { }

			public string SearchExpression 
			{
				get { return Session.GetSessionValue(Name + "IndexPageSearch", String.Empty); }
				set { Session.SetSessionValue(Name + "IndexPageSearch", value); }
			}
		}
	}
}