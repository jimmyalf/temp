using System;
using System.Linq;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Extensions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Extensions;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public abstract class RenameTextCommand : PersistenceBase
	{
		protected void Execute(int id, string search, string replace, string table, string column, string idColumn = "cId")
		{
			var originalContent = GetContent(table, column, idColumn, id);
			var updatedContent = originalContent.ReplaceString(search, replace, StringComparison.InvariantCultureIgnoreCase);
			var sql = GetUpdateCommand(table, column, idColumn);
			var command = CommandBuilder.Build(sql).AddParameters(new {UpdatedContent = updatedContent, Id = id});
			Execute(command);
		}

		protected virtual string GetContent(string table, string column, string idColumn, int id)
		{
			var sql = GetQueryCommand(table, column, idColumn);
			var query = QueryBuilder.Build(sql).AddParameters(new {Id = id});
			return (string) Query(query, record => record[column]).Single();
		}

		protected virtual string GetUpdateCommand(string table, string column, string idColumn)
		{
			return @"UPDATE {Table} SET {ContentColumn} = @UpdatedContent WHERE {IdColumn} = @Id"
				.ReplaceWith(new {Table = table, ContentColumn = column, IdColumn = idColumn});
		}

		protected virtual string GetQueryCommand(string table, string column, string idColumn)
		{
			return "SELECT {Column} FROM {Table} WHERE {IdColumn} = @Id"
				.ReplaceWith(new {Column = column, Table = table, IdColumn = idColumn});
		}
	}
}