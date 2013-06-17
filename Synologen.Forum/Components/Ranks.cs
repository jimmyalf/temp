using System;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;


namespace Spinit.Wpc.Forum {

	public class Ranks {

		public static ArrayList GetRanks() {
			ArrayList ranks;

			ForumsDataProvider dp = ForumsDataProvider.Instance();

			ranks = dp.GetRanks();
			
			return ranks;
		}

		public static Rank GetRank( int rankId ) {
			return null;
		}

		public static int CreateRank( Rank rank ) {
			return CreateUpdateDeleteRank( rank, DataProviderAction.Create);
		}
		public static int DeleteRank( Rank rank ) {
			return CreateUpdateDeleteRank( rank, DataProviderAction.Delete);
		}
		public static int UpdateRank( Rank rank ) {
			return CreateUpdateDeleteRank( rank, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteRank( Rank rank, DataProviderAction action ) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			return dp.CreateUpdateDeleteRank( rank, action );
		}
	}
}