using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public bool AddUpdateDeleteContractArticleConnection(Enumerations.Action action, ref ContractArticleRow connection) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
            		new SqlParameter("@contractCustomerId", SqlDbType.Int, 4),
            		new SqlParameter("@articleId", SqlDbType.Int, 4),
            		new SqlParameter("@price", SqlDbType.Float),
					new SqlParameter("@noVAT", SqlDbType.Bit),
					new SqlParameter("@active", SqlDbType.Bit),
					new SqlParameter("@SPCSAccountNumber", SqlDbType.NVarChar, 50),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};


				int counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = connection.ContractCustomerId;
					parameters[counter++].Value = connection.ArticleId;
					parameters[counter++].Value = connection.Price;
					parameters[counter++].Value = connection.NoVAT;
					parameters[counter++].Value = connection.Active;
					parameters[counter++].Value = connection.SPCSAccountNumber;
				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = connection.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteContractArticleConnection", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					connection.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (Exception ex) {
				throw CreateDataException("Exception during AddUpdateDeleteContractArticleConnection", ex);
			}
		}

		public ContractArticleRow GetContractCustomerArticleRow(int connectionId) {
			try {
				DataSet articleDataSet = GetContractArticleConnections(connectionId, 0, null);
				DataRow articleDataRow = articleDataSet.Tables[0].Rows[0];
				ContractArticleRow articleRow = new ContractArticleRow();
				articleRow.Id = Util.CheckNullInt(articleDataRow, "cId");
				articleRow.ArticleId = Util.CheckNullInt(articleDataRow, "cArticleId");
				articleRow.ContractCustomerId = Util.CheckNullInt(articleDataRow, "cContractCustomerId");
				articleRow.Price = Util.CheckNullFloat(articleDataRow, "cPrice");
				articleRow.Active = (bool)articleDataRow["cActive"];
				articleRow.ArticleName = Util.CheckNullString(articleDataRow, "cName");
				articleRow.ArticleNumber = Util.CheckNullString(articleDataRow, "cArticleNumber");
				articleRow.ArticleDescription = Util.CheckNullString(articleDataRow, "cDescription");
				articleRow.NoVAT = (bool)articleDataRow["cNoVAT"];
				articleRow.SPCSAccountNumber = Util.CheckNullString(articleDataRow, "cSPCSAccountNumber");
				return articleRow;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while parsing a ContractArticleRow object", ex);
			}
		}

		public DataSet GetContractArticleConnections(int connectionId, int contractId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@connectionId", SqlDbType.Int, 4),
						new SqlParameter ("@contractCustomerId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = connectionId;
				parameters[counter++].Value = contractId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetContractArticleConnections", parameters, "tblSynologenContractCustomerArticles");
				return retSet;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while getting GetContractArticleConnections", ex);
			}
		}
		
	}
}