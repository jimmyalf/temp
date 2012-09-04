using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Extensions;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public abstract class RenameNTextCommand : PersistenceBase
	{
		protected void Execute(int id, string search, string replace, string table, string column, string idColumn = "cId")
		{
			var sql = GetSqlCommand(table, column, idColumn);
			var command = CommandBuilder
				.Build(sql)
				.AddParameters(new { FindString = search, ReplaceString = replace, Id = id });
			Execute(command);
		}

		protected virtual string GetSqlCommand(string table, string column, string idColumn)
		{
			return @"
				DECLARE @TextPointer VARBINARY(16), @DeleteLength INT, @OffSet INT 
				SET @DeleteLength = LEN(@FindString) 
				SET @OffSet = 0
				SET @FindString = '%' + @FindString + '%'

				WHILE (EXISTS(
					SELECT {IdColumn} 
					FROM {Table} 
					WHERE (PATINDEX(@FindString, {ContentColumn}) <> 0) AND ({IdColumn} = @Id))) BEGIN 
						SELECT 
							@TextPointer = TEXTPTR({ContentColumn}), 
							@OffSet = PATINDEX(@FindString, {ContentColumn}) - 1
						FROM {Table}
						WHERE {IdColumn} = @Id

						UPDATETEXT tblContPage.cContent 
							@TextPointer
							@OffSet
							@DeleteLength
							@ReplaceString
				END"
				.ReplaceWith(new{Table = table, ContentColumn = column, IdColumn = idColumn});
		}
	}
}