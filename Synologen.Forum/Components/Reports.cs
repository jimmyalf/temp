using System;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

	public class Reports {

		public static ArrayList GetReports() {
			return null;
		}

		public static Report GetReport( int reportId ) {
			return null;
		}

		public static int CreateReport( Report report ) {
			return CreateUpdateDeleteReport( report, DataProviderAction.Create);
		}
		public static int DeleteReport( Report report ) {
			return CreateUpdateDeleteReport( report, DataProviderAction.Delete);
		}
		public static int UpdateReport( Report report ) {
			return CreateUpdateDeleteReport( report, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteReport( Report report, DataProviderAction action ) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			return dp.CreateUpdateDeleteReport( report, action );
		}
	}
}