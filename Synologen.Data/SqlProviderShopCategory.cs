using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public bool AddUpdateDeleteShopCategory(Enumerations.Action action, ref ShopCategory category) {
			try {
				var numAffected = 0;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@name", SqlDbType.NVarChar, 50),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};

				var counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {

					parameters[counter++].Value = category.Name ?? SqlString.Null;

				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = category.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteShopCategory", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					category.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public DataSet GetShopCategories(int categoryId) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@categoryId", SqlDbType.Int, 4),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = categoryId;
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetShopCategories", parameters, "tblSynologenShopCategory");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public ShopCategory GetShopCategoryRow(int categoryId) {
			var returnValue = new ShopCategory();
			var dataSet = GetShopCategories(categoryId);
			var dataRow = dataSet.Tables[0].Rows[0];
			returnValue.Id = Util.CheckNullInt(dataRow, "cId");
			returnValue.Name = Util.CheckNullString(dataRow, "cName");
			return returnValue;
		}

		public bool ShopCategoryHasConnectedShops(int categoryId) {
			var shopDataSet = GetShops(null, categoryId, null, null, null, null, null, null);
			return DataSetHasRows(shopDataSet);
		}

		#region Shop Category - Member Category connection

		private DataSet GetShopCategoryMemberCategoryConnections(int shopCategoryId, int memberCategoryId) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@shopCategoryId", SqlDbType.Int, 4),
						new SqlParameter ("@memberCategoryId", SqlDbType.Int, 4),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = shopCategoryId;
				parameters[counter++].Value = memberCategoryId;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetShopCategoryMemberCategoryConnections", parameters, "tblSynologenShopCategoryMemberCategoryConnection");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		private bool UpdateShopCategoryMemberCategoryConnection(ConnectionAction action, int shopCategoryId, int memberCategoryId) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@shopCategoryId", SqlDbType.Int, 4),
            		new SqlParameter("@memberCategoryId", SqlDbType.Int, 4),
            		new SqlParameter("@status", SqlDbType.Int, 4)
				};

				int counter = 0;
				parameters[counter++].Value = (int)action;
				parameters[counter++].Value = shopCategoryId;
				parameters[counter++].Value = memberCategoryId;
				parameters[counter].Direction = ParameterDirection.Output;
				RunProcedure("spSynologenUpdateShopCategoryMemberCategoryConnection", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 1].Value) == 0) {
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("UpdateShopContractConnection failed. Error: " + (int)parameters[parameters.Length - 1].Value, (int)parameters[parameters.Length - 1].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public List<int> GetShopCategoriesPerMemberCategoryId(int memberCategoryId) {
			List<int> returnList = new List<int>();
			DataSet connectionDataSet = GetShopCategoryMemberCategoryConnections(0, memberCategoryId);
			foreach (DataRow connectionRow in connectionDataSet.Tables[0].Rows) {
				returnList.Add(Util.CheckNullInt(connectionRow, "cShopCategoryId"));
			}
			return returnList;
		}

		public List<int> GetMemberCategoriesPerShopCategoryId(int shopCategoryId) {
			List<int> returnList = new List<int>();
			DataSet connectionDataSet = GetShopCategoryMemberCategoryConnections(shopCategoryId, 0);
			foreach (DataRow connectionRow in connectionDataSet.Tables[0].Rows) {
				returnList.Add(Util.CheckNullInt(connectionRow, "cMemberCategoryId"));
			}
			return returnList;
		}

		/// <summary>
		/// Disconnects a Member Category from any shop categories
		/// </summary>
		/// <param name="memberCategoryId">Member Id</param>
		public bool DisconnectMemberCategoryFromShopCategories(int memberCategoryId) {
			const ConnectionAction action = ConnectionAction.Delete;
			return UpdateShopCategoryMemberCategoryConnection(action, 0, memberCategoryId);
		}

		/// <summary>
		/// Connects a Member Category to a Shop Category
		/// </summary>
		/// <param name="memberCategoryId">Member Id</param>
		/// <param name="shopCategoryId">Shop Category Id</param>
		public bool ConnectMemberCategoryToShopCategory(int memberCategoryId, int shopCategoryId) {
			const ConnectionAction action = ConnectionAction.Connect;
			return UpdateShopCategoryMemberCategoryConnection(action, shopCategoryId, memberCategoryId);
		}

		#endregion

	}
}
