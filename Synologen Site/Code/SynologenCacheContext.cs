// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SynologenCacheContext.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Site/Code/SynologenCacheContext.cs $
//
//  VERSION
//	$Revision: 3 $
//
//  DATES
//	Last check in: $Date: 09-04-21 17:35 $
//	Last modified: $Modtime: 09-04-14 13:02 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Site/Code/SynologenCacheContext.cs $
//
//3     09-04-21 17:35 Cber
//
//2     09-04-09 16:39 Cber
//Settlement changes
//
//1     09-04-03 18:06 Cber
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
using System.Data;
using Spinit.Wpc.Synologen.Business;

namespace Spinit.Wpc.Synologen.Presentation.Site.Code {
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