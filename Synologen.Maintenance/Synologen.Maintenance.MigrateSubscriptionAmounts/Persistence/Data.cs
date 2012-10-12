using System;
using System.Data;
using Spinit.Data.FluentParameters;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence
{
	public static class Data
	{
		public static T Parse<T>(IDataRecord record, Action<FluentDataParser<T>> builder) where T : class
		{
			return record.Parse(builder, string.Empty);
		}
	}
}