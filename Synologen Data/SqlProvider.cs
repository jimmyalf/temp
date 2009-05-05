// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProvider.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProvider.cs $
//
//  VERSION
//	$Revision: 4 $
//
//  DATES
//	Last check in: $Date: 09-02-25 18:00 $
//	Last modified: $Modtime: 09-02-25 15:14 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProvider.cs $
//
//4     09-02-25 18:00 Cber
//Changes to allow for SPCS Account property on ArticleConnection,
//OrderItemRow and IOrderItem
//
//3     09-01-26 11:26 Cber
//
//2     09-01-09 17:44 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
using System.Data;
using DataException=Spinit.GeneralData.DatabaseInterface.DataException;

namespace Spinit.Wpc.Synologen.Data {
	/// <summary>
	/// Base class for SqlProvider
	/// </summary>
	public partial  class SqlProvider: Member.Data.SqlProvider {
		/// <summary>
		/// Base constructor for SqlProvider
		/// </summary>
		public SqlProvider(string connectionString) : base(connectionString) {}

		private static bool DataSetHasRows (DataSet dataset) {
			if (dataset == null || dataset.Tables[0] == null) return false;
			if (dataset.Tables[0].Rows == null) return false;
			return (dataset.Tables[0].Rows.Count > 0);
		}

		private DataException CreateDataException(string message, Exception innerException) {
			DataException exception = new DataException(message, innerException);
			return exception;
		}
	}
}