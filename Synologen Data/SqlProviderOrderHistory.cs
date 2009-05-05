// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProviderOrderHistory.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderOrderHistory.cs $
//
//  VERSION
//	$Revision: 2 $
//
//  DATES
//	Last check in: $Date: 09-03-04 14:18 $
//	Last modified: $Modtime: 09-03-02 13:15 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2009 Spinit AB --- ALL RIGHTS RESERVED
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderOrderHistory.cs $
//
//2     09-03-04 14:18 Cber
//
//1     09-02-05 18:01 Cber
// 
// ==========================================================================
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public int AddOrderHistory(int orderId, string message) {
			return AddOrderHistory(orderId, 0, message);
		}
		public int AddOrderHistory(long invoiceNumber, string message) {
			return AddOrderHistory(0, invoiceNumber, message);
		}

		private int AddOrderHistory(int orderId, long invoiceNumber, string message) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
					new SqlParameter("@orderId", SqlDbType.Int, 4),
					new SqlParameter("@invoiceNumber", SqlDbType.BigInt),
            		new SqlParameter("@message", SqlDbType.NVarChar, 500),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};


				int counter = 0;
				parameters[counter++].Value = orderId;
				parameters[counter++].Value = invoiceNumber;
				parameters[counter++].Value = message ?? SqlString.Null;
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				parameters[parameters.Length - 1].Direction = ParameterDirection.Output;

				RunProcedure("spSynologenAddOrderHistory", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					int orderHistoryId = (int)parameters[parameters.Length - 1].Value;
					return orderHistoryId;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public DataSet GetOrderHistory(int orderId) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@orderId", SqlDbType.Int, 4),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = orderId;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetOrderHistory", parameters, "tblSynologenOrderHistory");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}
	}
}