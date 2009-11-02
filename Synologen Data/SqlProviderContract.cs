using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetContractsByPage(string searchString, string orderBy, int currentPage, int pageSize, ref int totalSize) {

			try {
				var counter = 0;

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
				var retSet = RunProcedure("spSynologenGetContractsByPage", parameters, "tblSynologenContract");
				if (retSet.Tables.Count > 1) totalSize = Convert.ToInt32(retSet.Tables[1].Rows[0][0]);
				return retSet;

			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException while getting contracts.", e);
			}

		}

		public Contract GetContract(int contractCustomerId) {
			try {
				var contractCustomerDataSet = GetContracts(FetchCustomerContract.Specific, contractCustomerId, 0, null);
				var contractCustomerDataRow = contractCustomerDataSet.Tables[0].Rows[0];
				var contract = new Contract {Address = Util.CheckNullString(contractCustomerDataRow, "cAddress"), Address2 = Util.CheckNullString(contractCustomerDataRow, "cAddress2"), City = Util.CheckNullString(contractCustomerDataRow, "cCity"), Code = Util.CheckNullString(contractCustomerDataRow, "cCode"), Description = Util.CheckNullString(contractCustomerDataRow, "cDescription"), Email = Util.CheckNullString(contractCustomerDataRow, "cEmail"), Fax = Util.CheckNullString(contractCustomerDataRow, "cFax"), Id = Util.CheckNullInt(contractCustomerDataRow, "cId"), Name = Util.CheckNullString(contractCustomerDataRow, "cName"), Phone = Util.CheckNullString(contractCustomerDataRow, "cPhone"), Phone2 = Util.CheckNullString(contractCustomerDataRow, "cPhone2"), Zip = Util.CheckNullString(contractCustomerDataRow, "cZip"), Active = (bool) contractCustomerDataRow["cActive"]};
				return contract;
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a Shop object.", ex);
			}
		}

		public DataSet GetContracts(FetchCustomerContract type, int contractCustomer, int shopId, bool? active) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@type", SqlDbType.Int, 4),
					new SqlParameter ("@contractCustomerId", SqlDbType.Int, 4),
					new SqlParameter ("@shopId", SqlDbType.Int, 4),
					new SqlParameter ("@active", SqlDbType.Bit),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = (int)type;
				parameters[counter++].Value = contractCustomer;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = GetNullableSqlType(active);
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetContracts", parameters, "tblSynologenContract");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException while getting contracts.", e);
			}
		}

		public List<int> GetContractIdsPerShop(int shopId, bool? active) {
			//TODO: Improve by querying just the connection table.
			var returnList = new List<int>();
			var contractCustomersDataSet = GetContracts(FetchCustomerContract.AllPerShop, 0, shopId, active);
			var contractCustomersDataRowCollection = contractCustomersDataSet.Tables[0].Rows;
			foreach(DataRow contractCustomerDataRow in contractCustomersDataRowCollection) {
				returnList.Add((int)contractCustomerDataRow["cId"]);
			}
			return returnList;
		}

		public bool ContractHasConnectedOrders(int contractId) {
			var contractDataSet = GetOrders(0, 0, contractId, 0, 0, 0, 0, null);
			return DataSetHasRows(contractDataSet);
		}

		public bool AddUpdateDeleteContract(Enumerations.Action action, ref Contract contract) {
		    try {
		        int numAffected;
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

		        var counter = 0;
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
		        throw new GeneralData.DatabaseInterface.DataException("SqlException while adding/updating/deleting contract.", e);
		    }
		}




	}
}