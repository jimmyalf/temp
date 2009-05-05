// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProviderArticle.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderArticle.cs $
//
//  VERSION
//	$Revision: 8 $
//
//  DATES
//	Last check in: $Date: 09-02-25 18:00 $
//	Last modified: $Modtime: 09-02-25 15:03 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderArticle.cs $
//
//8     09-02-25 18:00 Cber
//Changes to allow for SPCS Account property on ArticleConnection,
//OrderItemRow and IOrderItem
//
//7     09-02-19 17:03 Cber
//
//6     09-01-26 11:26 Cber
//
//5     09-01-09 17:44 Cber
//
//4     09-01-08 18:08 Cber
//
//3     08-12-23 18:42 Cber
//
//2     08-12-19 17:22 Cber
//
//1     08-12-18 19:07 Cber
// 
// ==========================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public ArticleRow GetArticle(int articleId) {
			try {
				DataSet articleDataSet = GetArticles(articleId,0,"cId");
				DataRow articleDataRow = articleDataSet.Tables[0].Rows[0];
				ArticleRow articleRow = new ArticleRow();
				articleRow.Id = Util.CheckNullInt(articleDataRow, "cId");
				articleRow.Description = Util.CheckNullString(articleDataRow, "cDescription");
				articleRow.Name = Util.CheckNullString(articleDataRow, "cName");
				articleRow.Number = Util.CheckNullString(articleDataRow, "cArticleNumber");
				return articleRow;
			}
			catch (Exception ex) {
				throw new Exception("Exception while parsing a ArticleRow object: " + ex.Message);
			}
		}

		public DataSet GetArticles(int articleId,int contractId, string orderBy) {
			try {
				int counter = 0;
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
				DataSet retSet = RunProcedure("spSynologenGetArticles", parameters, "tblSynologenArticles");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public DataSet GetAllArticles(string orderBy) {
			return GetArticles(0, 0, orderBy);
		}

		public DataSet GetContractArticles(int contractCustomerId,string orderBy) {
			return GetArticles(0, contractCustomerId, orderBy);
		}

		public bool AddUpdateDeleteArticle(Enumerations.Action action, ref ArticleRow article) {
			try {
				int numAffected = 0;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
            		new SqlParameter("@number", SqlDbType.NVarChar, 50),
            		new SqlParameter("@name", SqlDbType.NVarChar, 50),
            		new SqlParameter("@description", SqlDbType.NVarChar, 255),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};


				int counter = 0;
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
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public bool ArticleHasConnectedContracts(int articleId) {
			DataSet articleDataSet = GetArticles(articleId, 0, null);
			return DataSetHasRows(articleDataSet);
		}

		public bool ArticleHasConnectedOrders(int articleId) {
			DataSet orderItemsDataSet = GetOrderItems(0, articleId, null);
			return DataSetHasRows(orderItemsDataSet);
			
		}


	}
}