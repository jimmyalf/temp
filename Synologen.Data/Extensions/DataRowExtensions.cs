using System;
using System.Data;

namespace Spinit.Wpc.Synologen.Data.Extensions
{
	public static class DataRowExtensions
	{

		public static TType TryGetValue<TType>(this DataRow row, string columnName, TType defaultValue)
		{
			var value = row[columnName];
			return value == DBNull.Value 
				? defaultValue 
				: row.Field<TType>(columnName);
		}

		public static TType TryGetValue<TType>(this DataRow row, string columnName)
		{
			return TryGetValue(row, columnName, default(TType));
		}
	}
}
