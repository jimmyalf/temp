// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: ContractArticleRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ContractArticleRow.cs $
//
//  VERSION
//	$Revision: 4 $
//
//  DATES
//	Last check in: $Date: 09-02-25 18:00 $
//	Last modified: $Modtime: 09-02-25 15:01 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ContractArticleRow.cs $
//
//4     09-02-25 18:00 Cber
//Changes to allow for SPCS Account property on ArticleConnection,
//OrderItemRow and IOrderItem
//
//3     09-02-19 17:03 Cber
//
//2     08-12-23 18:42 Cber
//
//1     08-12-19 17:22 Cber
// 
// ==========================================================================
namespace Spinit.Wpc.Synologen.Data.Types {
	public class ContractArticleRow {
		public int Id;
		public int ContractCustomerId;
		public int ArticleId;
		public string ArticleName;
		public float Price;
		public bool Active;
		public string ArticleNumber;
		public string ArticleDescription;
		public bool NoVAT;
		public string SPCSAccountNumber;
	}
}