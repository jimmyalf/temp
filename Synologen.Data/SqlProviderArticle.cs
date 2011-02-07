using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider{

		public Article GetArticle(int articleId) {
			try {
				var articleDataSet = GetArticles(articleId,0,"cId");
				var articleDataRow = articleDataSet.Tables[0].Rows[0];
				var article = new Article {Id = Util.CheckNullInt(articleDataRow, "cId"), Description = Util.CheckNullString(articleDataRow, "cDescription"), Name = Util.CheckNullString(articleDataRow, "cName"), Number = Util.CheckNullString(articleDataRow, "cArticleNumber")};
				return article;
			}
			catch (Exception ex) {
				throw new Exception("Exception while parsing a Article object: " + ex.Message);
			}
		}

		public DataSet GetArticles(int articleId,int contractId, string orderBy) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@articleId", SqlDbType.Int, 4),
						new SqlParameter ("@contractCustomerId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = articleId;
				parameters[counter++].Value = contractId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetArticles", parameters, "tblSynologenArticles");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException while getting articles.", e);
			}
		}

		public DataSet GetAllArticles(string orderBy) {
			return GetArticles(0, 0, orderBy);
		}

		public DataSet GetContractArticles(int contractCustomerId,string orderBy) {
			return GetArticles(0, contractCustomerId, orderBy);
		}

		public bool AddUpdateDeleteArticle(Enumerations.Action action, ref Article article) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
            		new SqlParameter("@number", SqlDbType.NVarChar, 50),
            		new SqlParameter("@name", SqlDbType.NVarChar, 50),
            		new SqlParameter("@description", SqlDbType.NVarChar, 255),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};


				var counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = article.Number ?? SqlString.Null;
					parameters[counter++].Value = article.Name ?? SqlString.Null;
					parameters[counter].Value = article.Description ?? SqlString.Null;
				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = article.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteArticle", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					article.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException while Add/Update/Delete Article.", e);
			}
		}

		public bool ArticleHasConnectedContracts(int articleId) {
			var articleDataSet = GetArticles(articleId, 0, null);
			return DataSetHasRows(articleDataSet);
		}

		public bool ArticleHasConnectedOrders(int articleId) {
			var orderItemsDataSet = GetOrderItemsDataSet(0, articleId, null);
			return DataSetHasRows(orderItemsDataSet);
			
		}


	}
}