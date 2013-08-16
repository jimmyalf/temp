using System;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

	public class Censors {

		public static ArrayList GetCensors() {
			ArrayList censors;

			ForumsDataProvider dp = ForumsDataProvider.Instance();

			censors = dp.GetCensors();

			return censors;
		}

		public static Censor GetCensor( string censorId ) {
			return null;
		}

		public static int CreateCensor( Censor censor ) {
			return CreateUpdateDeleteCensor( censor, DataProviderAction.Create);
		}
		public static int DeleteCensor( Censor censor ) {
			return CreateUpdateDeleteCensor( censor, DataProviderAction.Delete);
		}
		public static int UpdateCensor( Censor censor ) {
			return CreateUpdateDeleteCensor( censor, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteCensor( Censor censor, DataProviderAction action ) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			return dp.CreateUpdateDeleteCensor( censor, action );
		}
	}
}