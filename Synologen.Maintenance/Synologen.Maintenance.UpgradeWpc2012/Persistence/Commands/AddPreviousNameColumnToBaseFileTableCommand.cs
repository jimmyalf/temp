using Spinit.Data.SqlClient.SqlBuilder;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class AddPreviousNameColumnToBaseFileTableCommand : PersistenceBase
	{
		 public void Execute()
		 {
		 	var command = CommandBuilder
				.Build(
					@"IF NOT EXISTS(SELECT * FROM SYS.COLUMNS where Name = N'cPreviousName' AND OBJECT_ID = OBJECT_ID(N'tblBaseFile')) BEGIN
						ALTER TABLE tblBaseFile ADD cPreviousName NTEXT NULL
					END");
			 Execute(command);
		 }
	}
}