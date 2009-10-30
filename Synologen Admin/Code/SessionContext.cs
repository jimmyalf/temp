using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Utility;

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
			public static List<OrderItem> EditOrderItemsInCart {
				get {
					try {return (List<OrderItem>) Session.GetSessionValue(NAME + "EditOrderItemsInCart");}
					catch {return new List<OrderItem>();}
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