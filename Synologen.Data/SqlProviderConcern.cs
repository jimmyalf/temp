using System;
using System.Data;
using System.Data.SqlClient;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data{
	public partial class SqlProvider {
		private DataSet GetConcernDataSet(int? concernId, string orderBy) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@concernId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = GetNullableSqlType(concernId);
				parameters[counter++].Value = GetNullableSqlType(orderBy);
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetConcerns", parameters, "tblSynologenConcern");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}
		private static Concern ParseConcernRow(DataRow dataRow){
			var concernRow = new Concern
			{
				Id = Util.CheckNullInt(dataRow, "cId"),
				Name = Util.CheckNullString(dataRow, "cName"),
				CommonOPQ = (dataRow["cCommonOpq"] == DBNull.Value) ? (bool?)null : (bool)dataRow["cCommonOpq"]
			};
			return concernRow;
		}
		public Concern GetConcern(int concernId) {
			try {
				var shopDataSet = GetConcernDataSet(concernId,"cId");
				var shopDataRow = shopDataSet.Tables[0].Rows[0];
				return ParseConcernRow(shopDataRow);
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a Concern object.", ex);
			}
		}
	}
}