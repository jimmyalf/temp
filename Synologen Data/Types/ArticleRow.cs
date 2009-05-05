// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: ArticleRow.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ArticleRow.cs $
//
//  VERSION
//	$Revision: 2 $
//
//  DATES
//	Last check in: $Date: 09-02-19 17:03 $
//	Last modified: $Modtime: 09-02-17 12:11 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/Types/ArticleRow.cs $
//
//2     09-02-19 17:03 Cber
//
//1     08-12-18 19:07 Cber
// 
// ==========================================================================
namespace Spinit.Wpc.Synologen.Data.Types {
	public class ArticleRow {
		public int Id;
		public string Name;
		public string Number;
		public string Description;
		public bool NoVAT;
	}
}