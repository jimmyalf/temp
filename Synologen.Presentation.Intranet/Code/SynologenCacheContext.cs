using System.Data;
using Spinit.Wpc.Synologen.Business;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code {
	public static class SynologenCacheContext {
		private const string NAME = "SynologenSiteCache";
		

		public static void SetDetailedSettlementOrderListCache(string uniqueId, DataSet value) {
			Business.Utility.Cache.SetCacheValue(
				NAME +uniqueId+ "DetailedSettlementOrderListCache", 
				value,
				Globals.CacheTimeout);
		}


		public static DataSet GetDetailedSettlementOrderListCache(string uniqueId) {
			return Business.Utility.Cache.GetCacheValue(
				NAME +uniqueId+ "DetailedSettlementOrderListCache", 
				new DataSet());
		}



		public static void SetSimpleSettlementOrderListCache(string uniqueId, DataSet value) {
			Business.Utility.Cache.SetCacheValue(
				NAME + uniqueId + "SimpleSettlementOrderListCache",
				value,
				Globals.CacheTimeout);
		}

		public static DataSet GetSimpleSettlementOrderListCache(string uniqueId) {
			return Business.Utility.Cache.GetCacheValue(
				NAME + uniqueId + "SimpleSettlementOrderListCache", 
				new DataSet()
			);
		}

	}
}