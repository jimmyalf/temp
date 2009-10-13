using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public bool AddUpdateDeleteOrderItem(Enumerations.Action action, ref OrderItemRow orderItem) {
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

				int counter = 0;
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

		public DataSet GetOrderItems(int orderId, int articleId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@orderId", SqlDbType.Int, 4),
						new SqlParameter ("@articleId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = orderId;
				parameters[counter++].Value = articleId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetOrderItems", parameters, "tblSynologenOrderItem");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public List<OrderItemRow> GetOrderItemsList(int orderId, int articleId, string orderBy) {
			List<OrderItemRow> returnList = new List<OrderItemRow>();
			DataSet orderItems = GetOrderItems(orderId, articleId,orderBy);
			if (orderItems == null || orderItems.Tables[0] == null) return returnList;
			foreach (DataRow row in orderItems.Tables[0].Rows) {
				returnList.Add(ParseOrderItemRow(row));
			}
			return returnList;
		}

		public List<IOrderItem> GetIOrderItemsList(int orderId, int articleId, string orderBy) {
			List<IOrderItem> returnList = new List<IOrderItem>();
			DataSet orderItems = GetOrderItems(orderId, articleId, orderBy);
			if (orderItems == null || orderItems.Tables[0] == null) return returnList;
			foreach (DataRow row in orderItems.Tables[0].Rows) {
				returnList.Add(ParseOrderItemRow(row));
			}
			return returnList;
		}
		
		private OrderItemRow ParseOrderItemRow(DataRow row) {
			OrderItemRow returnItem = new OrderItemRow();
			returnItem.Id = Util.CheckNullInt(row, "cId");
			returnItem.Notes = Util.CheckNullString(row, "cNotes");
			returnItem.NumberOfItems = Util.CheckNullInt(row, "cNumberOfItems");
			returnItem.OrderId = Util.CheckNullInt(row, "cOrderId");
			returnItem.SinglePrice = Util.CheckNullFloat(row, "cSinglePrice");
			returnItem.DisplayTotalPrice = returnItem.SinglePrice * returnItem.NumberOfItems;
			returnItem.ArticleId = Util.CheckNullInt(row, "cArticleId");
			returnItem.NoVAT = (bool)row["cNoVAT"];
			ArticleRow article = GetArticle(returnItem.ArticleId);
			returnItem.ArticleDisplayName = article.Name;
			returnItem.ArticleDisplayNumber = article.Number;
			returnItem.SPCSAccountNumber = Util.CheckNullString(row, "cSPCSAccountNumber");
			return returnItem;
		}

	}
}