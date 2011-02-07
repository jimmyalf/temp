using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public int AddLog(LogType action, string message) {
			try {
				int numAffected = 0;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
            		new SqlParameter("@message", SqlDbType.NVarChar, 2000),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};


				int counter = 0;
				parameters[counter++].Value = (int)action;
				parameters[counter++].Value = message ?? SqlString.Null;
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				parameters[parameters.Length - 1].Direction = ParameterDirection.Output;

				RunProcedure("spSynologenAddLog", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					int logId = (int)parameters[parameters.Length - 1].Value;
					return logId;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}
		
	}
}