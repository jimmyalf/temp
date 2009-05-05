// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProviderContract.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderContract.cs $
//
//  VERSION
//	$Revision: 5 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderContract.cs $
//
//5     09-03-04 14:18 Cber
//
//4     09-01-09 17:44 Cber
//
//3     09-01-08 18:08 Cber
//
//2     08-12-23 18:42 Cber
//
//1     08-12-19 17:22 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider{

		public DataSet GetContractsByPage(string searchString, string orderBy, int currentPage, int pageSize, ref int totalSize) {

			try {
				int counter = 0;

				SqlParameter[] parameters = {
						new SqlParameter ("@SearchString", SqlDbType.NVarChar, 255),
						new SqlParameter ("@OrderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@CurrentPage", SqlDbType.Int, 4),
						new SqlParameter ("@PageSize", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = searchString ?? SqlString.Null;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter++].Value = currentPage;
				parameters[counter].Value = pageSize;
				DataSet retSet = RunProcedure("spSynologenGetContractsByPage", parameters, "tblSynologenContract");
				if (retSet.Tables.Count > 1) totalSize = Convert.ToInt32(retSet.Tables[1].Rows[0][0]);
				return retSet;

			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: "+ e);
			}

		}

		public ContractRow GetContract(int contractCustomerId) {
			try {
				DataSet contractCustomerDataSet = GetContracts(FetchCustomerContract.Specific, contractCustomerId, 0);
				DataRow contractCustomerDataRow = contractCustomerDataSet.Tables[0].Rows[0];
				ContractRow contractRow = new ContractRow();
				contractRow.Address = Util.CheckNullString(contractCustomerDataRow, "cAddress");
				contractRow.Address2 = Util.CheckNullString(contractCustomerDataRow, "cAddress2");
				contractRow.City = Util.CheckNullString(contractCustomerDataRow, "cCity");
				contractRow.Code = Util.CheckNullString(contractCustomerDataRow, "cCode");
				contractRow.Description = Util.CheckNullString(contractCustomerDataRow, "cDescription");
				contractRow.Email = Util.CheckNullString(contractCustomerDataRow, "cEmail");
				contractRow.Fax = Util.CheckNullString(contractCustomerDataRow, "cFax");
				contractRow.Id = Util.CheckNullInt(contractCustomerDataRow, "cId");
				contractRow.Name = Util.CheckNullString(contractCustomerDataRow, "cName");
				contractRow.Phone = Util.CheckNullString(contractCustomerDataRow, "cPhone");
				contractRow.Phone2 = Util.CheckNullString(contractCustomerDataRow, "cPhone2");
				contractRow.Zip = Util.CheckNullString(contractCustomerDataRow, "cZip");
				contractRow.Active = (bool)contractCustomerDataRow["cActive"];
				return contractRow;
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a ShopRow object: "+ex.Message);
			}
		}

		public DataSet GetContracts(FetchCustomerContract type, int contractCustomer, int shopId) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@type", SqlDbType.Int, 4),
					new SqlParameter ("@contractCustomerId", SqlDbType.Int, 4),
					new SqlParameter ("@shopId", SqlDbType.Int, 4),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = (int)type;
				parameters[counter++].Value = contractCustomer;
				parameters[counter++].Value = shopId;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetContracts", parameters, "tblSynologenContract");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public List<int> GetContractIdsPerShop(int shopId) {
			//TODO: Improve by querying just the connection table.
			List<int> returnList = new List<int>();
			DataSet contractCustomersDataSet = GetContracts(FetchCustomerContract.AllPerShop, 0, shopId);
			DataRowCollection contractCustomersDataRowCollection = contractCustomersDataSet.Tables[0].Rows;
			foreach(DataRow contractCustomerDataRow in contractCustomersDataRowCollection) {
				returnList.Add((int)contractCustomerDataRow["cId"]);
			}
			return returnList;
		}

		public bool ContractHasConnectedOrders(int contractId) {
			DataSet contractDataSet = GetOrders(0, 0, contractId, 0, 0, 0, 0, null);
			return DataSetHasRows(contractDataSet);
		}

		public bool AddUpdateDeleteContract(Enumerations.Action action, ref ContractRow contract) {
		    try {
		        int numAffected = 0;
		        SqlParameter[] parameters = {
		            new SqlParameter("@type", SqlDbType.Int, 4),
		            new SqlParameter("@code", SqlDbType.NVarChar, 50),
		            new SqlParameter("@name", SqlDbType.NVarChar, 50),
		            new SqlParameter("@description", SqlDbType.NVarChar, 500),
		            new SqlParameter("@address", SqlDbType.NVarChar, 50),
		            new SqlParameter("@address2", SqlDbType.NVarChar, 50),
		            new SqlParameter("@zip", SqlDbType.NVarChar, 50),
		            new SqlParameter("@city", SqlDbType.NVarChar, 50),
		            new SqlParameter("@phone", SqlDbType.NVarChar, 50),
		            new SqlParameter("@phone2", SqlDbType.NVarChar, 50),
		            new SqlParameter("@fax", SqlDbType.NVarChar, 50),
		            new SqlParameter("@email", SqlDbType.NVarChar, 50),
					new SqlParameter("@active", SqlDbType.Bit),
		            new SqlParameter("@status", SqlDbType.Int, 4),
		            new SqlParameter("@id", SqlDbType.Int, 4)
		        };

		        int counter = 0;
		        parameters[counter++].Value = (int)action;
		        if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = contract.Code ?? SqlString.Null;
					parameters[counter++].Value = contract.Name ?? SqlString.Null;
					parameters[counter++].Value = contract.Description ?? SqlString.Null;
					parameters[counter++].Value = contract.Address ?? SqlString.Null;
					parameters[counter++].Value = contract.Address2 ?? SqlString.Null;
					parameters[counter++].Value = contract.Zip ?? SqlString.Null;
					parameters[counter++].Value = contract.City ?? SqlString.Null;
					parameters[counter++].Value = contract.Phone ?? SqlString.Null;
					parameters[counter++].Value = contract.Phone2 ?? SqlString.Null;
					parameters[counter++].Value = contract.Fax ?? SqlString.Null;
					parameters[counter++].Value = contract.Email ?? SqlString.Null;
					parameters[counter++].Value = contract.Active;
				}
		        parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
		        if (action == Enumerations.Action.Create){
		            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
		        }
		        else{
		            parameters[parameters.Length - 1].Value = contract.Id;
		        }
				RunProcedure("spSynologenAddUpdateDeleteContract", parameters, out numAffected);

		        if (((int)parameters[parameters.Length - 2].Value) == 0){
		            contract.Id= (int)parameters[parameters.Length - 1].Value;
		            return true;
		        }
		        throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: "+ (int)parameters[parameters.Length - 2].Value,(int)parameters[parameters.Length - 2].Value);
		    }
		    catch (SqlException e){
		        throw new GeneralData.DatabaseInterface.DataException("SqlException: "+ e);
		    }
		}




	}
}