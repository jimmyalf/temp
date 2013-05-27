using System;
using System.Data;
using System.Data.SqlClient;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data 
{
	public partial class SqlProvider 
	{

		public bool AddUpdateDeleteContractArticleConnection(Enumerations.Action action, ref ContractArticleConnection connection) {
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
					new SqlParameter("@enableManualPriceOverride", SqlDbType.Bit),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};


				var counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = connection.ContractCustomerId;
					parameters[counter++].Value = connection.ArticleId;
					parameters[counter++].Value = connection.Price;
					parameters[counter++].Value = connection.NoVAT;
					parameters[counter++].Value = connection.Active;
					parameters[counter++].Value = connection.SPCSAccountNumber;
					parameters[counter++].Value = connection.EnableManualPriceOverride;

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

		public ContractArticleConnection GetContractCustomerArticleRow(int connectionId) {
			try {
				var articleDataSet = GetContractArticleConnections(connectionId, 0, null, null);
				var articleDataRow = articleDataSet.Tables[0].Rows[0];
				var articleRow = new ContractArticleConnection {
					Id = Util.CheckNullInt(articleDataRow, "cId"), 
					ArticleId = Util.CheckNullInt(articleDataRow, "cArticleId"), 
					ContractCustomerId = Util.CheckNullInt(articleDataRow, "cContractCustomerId"), 
					Price = Util.CheckNullFloat(articleDataRow, "cPrice"), 
					Active = (bool) articleDataRow["cActive"], 
					ArticleName = Util.CheckNullString(articleDataRow, "cName"), 
					ArticleNumber = Util.CheckNullString(articleDataRow, "cArticleNumber"), 
					ArticleDescription = Util.CheckNullString(articleDataRow, "cDescription"), 
					NoVAT = (bool) articleDataRow["cNoVAT"], 
					SPCSAccountNumber = Util.CheckNullString(articleDataRow, "cSPCSAccountNumber"),
					EnableManualPriceOverride = (bool) articleDataRow["cEnableManualPriceOverride"],
				};
				return articleRow;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while parsing a ContractArticleConnection object", ex);
			}
		}

		public DataSet GetContractArticleConnections(int? connectionId, int? contractId, bool? active, string orderBy) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@connectionId", SqlDbType.Int, 4),
						new SqlParameter ("@contractCustomerId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@active", SqlDbType.Bit),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = GetNullableSqlType(connectionId);
				parameters[counter++].Value = GetNullableSqlType(contractId);
				parameters[counter++].Value = GetNullableSqlType(orderBy);
				parameters[counter++].Value = GetNullableSqlType(active);
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetContractArticleConnections", parameters, "tblSynologenContractCustomerArticles");
				return retSet;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while getting GetContractArticleConnections", ex);
			}
		}
		
	}
}