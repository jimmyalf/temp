using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public bool AddUpdateDeleteOrderItem(Enumerations.Action action, ref IOrderItem orderItem) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@orderId", SqlDbType.Int, 4),
            		new SqlParameter("@articleId", SqlDbType.Int, 4),
					new SqlParameter("@singlePrice", SqlDbType.Float),
					new SqlParameter("@numberOfItems", SqlDbType.Int, 4),
					new SqlParameter("@notes", SqlDbType.NVarChar, 255),
					new SqlParameter("@noVAT", SqlDbType.Bit),
					new SqlParameter("@SPCSAccountNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};

				var counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = orderItem.OrderId;
					parameters[counter++].Value = orderItem.ArticleId;
					parameters[counter++].Value = orderItem.SinglePrice;
					parameters[counter++].Value = orderItem.NumberOfItems;
					parameters[counter++].Value = orderItem.Notes ?? SqlString.Null;
					parameters[counter++].Value = orderItem.NoVAT;
					parameters[counter++].Value = orderItem.SPCSAccountNumber;

				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = orderItem.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteOrderItem", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					orderItem.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}

		}

		public DataSet GetOrderItemsDataSet(int? orderId, int? articleId, string orderBy) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@orderId", SqlDbType.Int, 4),
						new SqlParameter ("@articleId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = GetNullableSqlType(orderId);
				parameters[counter++].Value = GetNullableSqlType(articleId);
				parameters[counter++].Value = GetNullableSqlType(orderBy);
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetOrderItems", parameters, "tblSynologenOrderItem");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public IList<IOrderItem> GetOrderItemsList(int? orderId, int? articleId, string orderBy) {
			var returnList = new List<IOrderItem>();
			var orderItems = GetOrderItemsDataSet(orderId, articleId, orderBy);
			if (orderItems == null || orderItems.Tables[0] == null) return returnList;
			foreach (DataRow row in orderItems.Tables[0].Rows) {
				returnList.Add(ParseOrderItemRow(row));
			}
			return returnList;
		}
		
		private IOrderItem ParseOrderItemRow(DataRow row) {
			var returnItem = new OrderItem
			{
				Id = Util.CheckNullInt(row, "cId"), 
				Notes = Util.CheckNullString(row, "cNotes"), 
				NumberOfItems = Util.CheckNullInt(row, "cNumberOfItems"), 
				OrderId = Util.CheckNullInt(row, "cOrderId"), 
				SinglePrice = Util.CheckNullFloat(row, "cSinglePrice"),
				ArticleId = Util.CheckNullInt(row, "cArticleId"),
				NoVAT = (bool)row["cNoVAT"],
				SPCSAccountNumber = Util.CheckNullString(row, "cSPCSAccountNumber"),
			};
			returnItem.DisplayTotalPrice = returnItem.SinglePrice * returnItem.NumberOfItems;
			var article = GetArticle(returnItem.ArticleId);
			returnItem.ArticleDisplayName = article.Name;
			returnItem.ArticleDisplayNumber = article.Number;
			
			return returnItem;
		}

	}
}