using System.Data;
using System.Data.SqlClient;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetInvoicingMethods(int? invoicingMethodId, string orderBy) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@invoicingMethodId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = GetNullableSqlType(invoicingMethodId);
				parameters[counter++].Value = GetNullableSqlType(orderBy);
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetInvoicingMethods", parameters, "tblSynologenInvoicingMethod");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}


	}
}