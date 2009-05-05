// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SynologenUtility.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/SynologenUtility.cs $
//
//  VERSION
//	$Revision: 3 $
//
//  DATES
//	Last check in: $Date: 08-12-22 17:22 $
//	Last modified: $Modtime: 08-12-22 14:37 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/SynologenUtility.cs $
//
//3     08-12-22 17:22 Cber
//
//2     08-12-19 17:22 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Code {
	public static class Utility{
		public static bool FindMatchInLists(List<int> firstList,List<int> secondList) {
				foreach(int number in firstList) {
					if (secondList.Contains(number)) return true;
				}
				return false;
		}
	}
}