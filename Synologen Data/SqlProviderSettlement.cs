using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public int AddSettlement(int filterStatusId, int statusIdAfterSettlement) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@filterStatusId", SqlDbType.Int, 4),
            		new SqlParameter("@statusIdAfterSettlement", SqlDbType.Int, 4),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@settlementId", SqlDbType.Int, 4),
					new SqlParameter("@numberOfOrdersAdded", SqlDbType.Int, 4)
				};


				int counter = 0;
				parameters[counter++].Value = filterStatusId;
				parameters[counter++].Value = statusIdAfterSettlement;
				parameters[parameters.Length - 3].Direction = ParameterDirection.Output;
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				parameters[parameters.Length - 1].Direction = ParameterDirection.Output;

				RunProcedure("spSynologenAddSettlement", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 3].Value) == 0) {
					int newSettlementId = (int)parameters[parameters.Length - 2].Value;
					//int numberOfOrdersAdded = (int)parameters[parameters.Length - 1].Value; NOT USED
					return newSettlementId;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (Exception ex) {
				throw CreateDataException("Exception during AddSettlement", ex);
			}
		}

		public DataSet GetSettlementsDataSet(int settlementId, int shopId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@settlementId", SqlDbType.Int,4),
						new SqlParameter ("@shopId", SqlDbType.Int,4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = settlementId;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetSettlements", parameters, "tblSynologenSettlement");
				return retSet;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while getting GetSettlementsDataSet", ex);
			}
		}

		public DataSet GetSettlementDetailsDataSet(int settlementId, out float settlementValueIncludingVAT, out float settlementValueExcludingVAT, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@settlementId", SqlDbType.Int,4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@settlementValueIncludingVAT", SqlDbType.Float),
						new SqlParameter ("@settlementValueExcludingVAT", SqlDbType.Float)
					};
				parameters[counter++].Value = settlementId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter++].Direction = ParameterDirection.Output;
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetSettlementDetails", parameters, "tblSynologenSettlement");
				settlementValueIncludingVAT = (parameters[counter - 1].Value == DBNull.Value) ? 0 : Convert.ToSingle(parameters[counter - 1].Value);	
				settlementValueExcludingVAT = (parameters[counter].Value == DBNull.Value) ? 0 : Convert.ToSingle(parameters[counter].Value);
				return retSet;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while getting GetSettlementDetailsDataSet", ex);
			}
		}

		public DataSet GetSettlementsOrderItemsDataSetSimple(int shopId, int settlementId, string orderBy, out bool allOrdersMarkedAsPayed, out float orderValueIncludingVAT, out float orderValueExcludingVAT) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@shopId", SqlDbType.Int,4),
						new SqlParameter ("@settlementId", SqlDbType.Int,4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@allOrdersMarkedAsPayed", SqlDbType.Bit),					
						new SqlParameter ("@orderValueIncludingVAT", SqlDbType.Float),
						new SqlParameter ("@orderValueExcludingVAT", SqlDbType.Float)
					};
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = settlementId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter++].Direction = ParameterDirection.Output;
				parameters[counter++].Direction = ParameterDirection.Output;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetSettlementOrderItemsSimple", parameters, "tblSynologenOrderItems");
				orderValueExcludingVAT = Convert.ToSingle(parameters[counter].Value);
				orderValueIncludingVAT = Convert.ToSingle(parameters[counter - 1].Value);
				allOrdersMarkedAsPayed = (bool)parameters[counter - 2].Value;
				return retSet;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while getting GetSettlementsDataSet", ex);
			}
		}

		public DataSet GetSettlementsOrderItemsDataSetDetailed(int shopId, int settlementId, string orderBy, out bool allOrdersMarkedAsPayed, out float orderValueIncludingVAT, out float orderValueExcludingVAT) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@shopId", SqlDbType.Int,4),
						new SqlParameter ("@settlementId", SqlDbType.Int,4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@allOrdersMarkedAsPayed", SqlDbType.Bit),					
						new SqlParameter ("@orderValueIncludingVAT", SqlDbType.Float),
						new SqlParameter ("@orderValueExcludingVAT", SqlDbType.Float)
					};
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = settlementId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter++].Direction = ParameterDirection.Output;
				parameters[counter++].Direction = ParameterDirection.Output;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetSettlementOrderItemsDetailed", parameters, "tblSynologenOrderItems");
				orderValueExcludingVAT = Convert.ToSingle(parameters[counter].Value);
				orderValueIncludingVAT = Convert.ToSingle(parameters[counter - 1].Value);
				allOrdersMarkedAsPayed = (bool)parameters[counter - 2].Value;
				return retSet;
			}
			catch (Exception ex) {
				throw CreateDataException("Exception while getting GetSettlementsDataSet", ex);
			}
		}

		public int MarkOrdersInSettlementAsPayedPerShop(int settlementId, int shopId) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@settlementId", SqlDbType.Int, 4),
					new SqlParameter("@shopId", SqlDbType.Int, 4),
            		new SqlParameter("@status", SqlDbType.Int, 4)
				};


				int counter = 0;
				parameters[counter++].Value = settlementId;
				parameters[counter++].Value = shopId;
				parameters[parameters.Length - 1].Direction = ParameterDirection.Output;

				RunProcedure("spSynologenMarkOrdersInSettlementAsPayedPerShop", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 1].Value) == 0) return numAffected;
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (Exception ex) {
				throw CreateDataException("Exception during MarkOrdersInSettlementAsPayedPerShop", ex);
			}
		}

		public Settlement GetSettlement(int settlementId) {
			DataSet settlementDataSet = GetSettlementsDataSet(settlementId, 0, null);
			if (settlementDataSet.Tables[0] == null) return new Settlement();
			if (settlementDataSet.Tables[0].Rows[0] == null) return new Settlement();
			DataRow settlementDataRow = settlementDataSet.Tables[0].Rows[0];
			return ParseSettlementRow(settlementDataRow);
		}

		private static Settlement ParseSettlementRow(DataRow settlementDataRow) {
			Settlement settlement = new Settlement();
			settlement.Id = Util.CheckNullInt(settlementDataRow, "cId");
			if(Util.CheckDateTimeInput(settlementDataRow["cCreatedDate"].ToString())){
				settlement.CreatedDate = Convert.ToDateTime(settlementDataRow["cCreatedDate"]);
			}
			settlement.NumberOfConnectedOrders = Util.CheckNullInt(settlementDataRow, "cNumberOfOrders");
			return settlement;
		}
	}
}