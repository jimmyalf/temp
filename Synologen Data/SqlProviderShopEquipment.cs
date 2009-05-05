using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetShopEquipment(int equipmentId, int shopId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@equipmentId", SqlDbType.Int, 4),
						new SqlParameter ("@shopId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 50),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = equipmentId;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetShopEquipment", parameters, "tblSynologenShopEquipment");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public ShopEquipmentRow GetShopEquipmentRow(int equipmentId) {
			try {
				DataSet equipmentDataSet = GetShopEquipment(equipmentId, 0, null);
				DataRow equipmentDatRow = equipmentDataSet.Tables[0].Rows[0];
				ShopEquipmentRow equipmentRow = new ShopEquipmentRow();
				equipmentRow.Id = Util.CheckNullInt(equipmentDatRow, "cId");
				equipmentRow.Name = Util.CheckNullString(equipmentDatRow, "cName");
				equipmentRow.Description = Util.CheckNullString(equipmentDatRow, "cDescription");
				return equipmentRow;
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a ShopEquipmentRow object: " + ex.Message);
			}
		}

		public List<int> GetAllEquipmentIdsPerShop(int shopId) {
			List<int> returnList = new List<int>();
			DataSet equipmentDatSet = GetShopEquipment(0, shopId, null);
			if (equipmentDatSet == null || equipmentDatSet.Tables[0] == null) return returnList;
			foreach (DataRow row in equipmentDatSet.Tables[0].Rows) {
				returnList.Add(Util.CheckNullInt(row, "cId"));
			}
			return returnList;
		}

		public bool EquipmentHasConnectedShops(int equipmentId) {
			DataSet shopDataSet = GetShops(0, 0, 0, 0, equipmentId, true, null);
			return DataSetHasRows(shopDataSet);
		}

		public bool AddUpdateDeleteShopEquipment(Enumerations.Action action, ref ShopEquipmentRow equipment) {
			try {
				int numAffected = 0;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@name", SqlDbType.NVarChar, 50),
					new SqlParameter("@description", SqlDbType.NVarChar, 500),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};

				int counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {

					parameters[counter++].Value = equipment.Name;
					parameters[counter++].Value = equipment.Description ?? SqlString.Null;

				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = equipment.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteShopEquipment", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					equipment.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		private bool UpdateShopEquipmentConnection(ConnectionAction action, int equipmentId, int shopId, string notes) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@equipmentId", SqlDbType.Int, 4),
					new SqlParameter("@shopId", SqlDbType.Int, 4),
					new SqlParameter("@notes", SqlDbType.NVarChar, 255),
            		new SqlParameter("@status", SqlDbType.Int, 4)
				};

				int counter = 0;
				parameters[counter++].Value = (int)action;
				parameters[counter++].Value = equipmentId;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = notes ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				RunProcedure("spSynologenUpdateShopEquipmentConnection", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 1].Value) == 0) {
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("UpdateShopEquipmentConnection failed. Error: " + (int)parameters[parameters.Length - 1].Value, (int)parameters[parameters.Length - 1].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public void DisconnectShopFromAllEquipment(int shopId) {
			UpdateShopEquipmentConnection(ConnectionAction.Delete, 0, shopId, null);
		}

		public void ConnectShopToEquipment(int shopId, int equipmentId) {
			UpdateShopEquipmentConnection(ConnectionAction.Connect, equipmentId, shopId, null);
		}



	}
}
