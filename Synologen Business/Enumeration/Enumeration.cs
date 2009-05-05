// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: Enumeration.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Business/Enumeration/Enumeration.cs $
//
//  VERSION
//	$Revision: 5 $
//
//  DATES
//	Last check in: $Date: 09-04-21 17:34 $
//	Last modified: $Modtime: 09-04-14 12:30 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Business/Enumeration/Enumeration.cs $
//
//5     09-04-21 17:34 Cber
//Utility changes (from class to namespace)
//
//4     09-02-25 18:00 Cber
//Changes to allow for SPCS Account property on ArticleConnection,
//OrderItemRow and IOrderItem
//
//3     09-02-05 18:01 Cber
//
//2     09-01-07 17:35 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
namespace Spinit.Wpc.Synologen.Business.Enumeration {
	public enum FetchShop {
		All = 1,
		AllPerContractCustomer = 2,
		Specific = 3,
		AllPerShopCategory = 4,
		AllPerMember = 5
	}

	public enum FetchCustomerContract {
		All = 1,
		AllPerShop = 2,
		Specific = 3
	}
	public enum ConnectionAction {
		Connect = 1,
		Delete = 2
	}
	public enum ShopMemberConnectionAction {
		ConnectShopAndMember = 1,
		DeleteAllPerMember = 2
	}

	public enum FileCategoryGetAction {
		Specific = 1,
		All = 2
	}

	public enum FileCategoryType {
		Member,
		Synologen
	}
	public enum LogType {
		Error = 1,
		Information = 2,
		Other = 3
	}
	public enum ActiveFilter {
		Active = 1,
		Inactive = 2,
		Both = 3
	}

	public enum RoundDecimals {
		DoNotRound=1,
		RoundUp=2,
		RoundDown=3,
		RoundWithTwoDecimals=4
	}
}