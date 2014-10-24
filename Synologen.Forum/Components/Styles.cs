using System;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

	public class Styles {

		public static ArrayList GetStyles() {
			return null;
		}

		public static Style GetStyle( int styleId ) {
			return null;
		}

		public static int CreateStyle( Style style ) {
			return CreateUpdateDeleteStyle( style, DataProviderAction.Create);
		}
		public static int DeleteStyle( Style style ) {
			return CreateUpdateDeleteStyle( style, DataProviderAction.Delete);
		}
		public static int UpdateStyle( Style style ) {
			return CreateUpdateDeleteStyle( style, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteStyle( Style style, DataProviderAction action ) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			return dp.CreateUpdateDeleteStyle( style, action );
		}
	}
}