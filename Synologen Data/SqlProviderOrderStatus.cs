// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProviderOrderStatus.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderOrderStatus.cs $
//
//  VERSION
//	$Revision: 3 $
//
//  DATES
//	Last check in: $Date: 09-03-04 14:18 $
//	Last modified: $Modtime: 09-03-02 12:32 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderOrderStatus.cs $
//
//3     09-03-04 14:18 Cber
//
//2     09-01-09 17:44 Cber
//
//1     09-01-08 18:08 Cber
// 
// ==========================================================================
using System.Data;
using System.Data.SqlClient;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetOrderStatuses(int orderStatusId) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@orderStatusId", SqlDbType.Int, 4),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = orderStatusId;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetOrderStatuses", parameters, "tblSynologenOrderStatus");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public OrderStatusRow GetOrderStatusRow(int orderStatudId) {
			DataSet orderStatusDataSet = GetOrderStatuses(orderStatudId);
			DataRow orderStatusDataRow = orderStatusDataSet.Tables[0].Rows[0];
			OrderStatusRow orderStatus = new OrderStatusRow();
			orderStatus.Id = Util.CheckNullInt(orderStatusDataRow, "cId");
			orderStatus.Name = Util.CheckNullString(orderStatusDataRow, "cName");
			orderStatus.OrderNumber = Util.CheckNullInt(orderStatusDataRow, "cOrder");
			return orderStatus;
		}

		public bool AddUpdateDeleteOrderStatus(Enumerations.Action action, ref OrderStatusRow orderStatus) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@name", SqlDbType.NVarChar, 50),
					new SqlParameter("@orderNumber", SqlDbType.Int, 4),
					new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};

				int counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = orderStatus.Name;
					parameters[counter++].Value = orderStatus.OrderNumber;
				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = orderStatus.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteOrderStatus", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					orderStatus.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}


		}

		public bool OrderStatusHasConnectedOrders(int orderStatusId) {
			DataSet orderStatusDataSet = GetOrders(0, 0, 0, 0, 0, 0, orderStatusId, null);
			return DataSetHasRows(orderStatusDataSet);
		}
	}
}