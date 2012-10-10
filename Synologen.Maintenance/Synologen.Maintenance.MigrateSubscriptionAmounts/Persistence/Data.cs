using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence
{
	public static class Data
	{
		 public static FluentDataParser<TType> CreateParser<TType>(IDataRecord record) where TType : class
		 {
		 	return new FluentDataParser<TType>(record) {ColumnPrefix = string.Empty};
		 }
	}
}