using System;
using System.Data;
using System.Data.SqlTypes;
using Spinit.Data.SqlClient;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using DataException=Spinit.GeneralData.DatabaseInterface.DataException;

namespace Spinit.Wpc.Synologen.Data {
	/// <summary>
	/// Base class for SqlProvider
	/// </summary>
	public partial  class SqlProvider: Member.Data.SqlProvider, ISqlProvider 
	{
		private PersistenceBase Persistence { get; set; }

		/// <summary>
		/// Base constructor for SqlProvider
		/// </summary>
		public SqlProvider(string connectionString) : base(connectionString)
		{
			Persistence = new PersistenceBase(connectionString);
		}

		private static bool DataSetHasRows (DataSet dataset) {
			if (dataset == null || dataset.Tables[0] == null) return false;
			if (dataset.Tables[0].Rows == null) return false;
			return (dataset.Tables[0].Rows.Count > 0);
		}

		private static DataException CreateDataException(string message, Exception innerException) {
			var exception = new DataException(message, innerException);
			return exception;
		}

		private static SqlInt32 GetNullableSqlType(int? value) {
			return value.HasValue ? new SqlInt32(value.Value) : SqlInt32.Null;
		}
		private static SqlString GetNullableSqlType(string value) {
			return String.IsNullOrEmpty(value) ? SqlString.Null : new SqlString(value);
		}
		private static SqlDateTime GetNullableSqlType(DateTime? value) {
			return value.HasValue ? new SqlDateTime(value.Value) : SqlDateTime.Null;
		}

		private static SqlDecimal GetNullableSqlType(decimal? value) {
			return value.HasValue ? new SqlDecimal(value.Value) : SqlDecimal.Null;
		}
		private static SqlBoolean GetNullableSqlType(bool? value) {
			return value.HasValue ? new SqlBoolean(value.Value) : SqlBoolean.Null;
		}
	}
}